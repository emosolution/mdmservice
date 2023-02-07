using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using System.Reflection;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using System.Text.Json;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.Shared.Protos.Shared.Import;

namespace DMSpro.OMS.MdmService
{
    public class PartialAppService<T, TDto, TRepository> : ApplicationService, IPartialAppservice
        where T : class, IEntity, new()
        where TDto : class
        where TRepository : class, IRepository<T>
    {
        protected readonly IRepository<T> _repository;

        private readonly ICurrentTenant _currentTenant;
        private readonly IConfiguration _settingProvider;
        private static readonly List<Type> _knownNumberTypes = new()
        {
            typeof(uint),
            typeof(int),
            typeof(decimal),
        };

        private Dictionary<string, Type> _entityProperties = new();
        private readonly Dictionary<string, string> _getIdByCodeFromDBAndSheet = new();
        private readonly Dictionary<string, string> _getIdByCodeFromDBOnly = new();
        private readonly Dictionary<string, string> _getIdFromGRPC = new();
        private readonly Dictionary<string, Guid> _entityCodeAndIdFromSheet = new();
        private readonly Dictionary<string, List<string>> _codeToGetFromDB = new();
        private readonly Dictionary<string, List<string>> _codeToGetFromGRPC = new();
        private readonly Dictionary<Guid, Dictionary<string, string>> _entityCodeValue = new();
        private readonly Dictionary<string, bool> _codePropertyAllowNull = new();
        private readonly Dictionary<string, string> _grpcInfo = new();

        protected readonly Dictionary<string, object> _repositories = new();

        public PartialAppService(ICurrentTenant currentTenant,
            TRepository repository, IConfiguration settingProvider)
        {
            _repository = repository;
            _currentTenant = currentTenant;
            _settingProvider = settingProvider;
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _repository.GetQueryableAsync();
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            results.data = ObjectMapper.Map<IEnumerable<T>, IEnumerable<TDto>>(results.data.Cast<T>());
            return results;
        }

        public Task<int> UpdateFromExcelAsync(IFormFile file)
        {
            return null;
        }

        public async Task<int> InsertFromExcelAsync(IFormFile file)
        {
            if (file == null || file.Length <= 0) //file empty
            {
                throw new BusinessException(message: "Error:ImportHandler:550", code: "0");
            }

            if (!(Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
                || Path.GetExtension(file.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase)))
            //not support file extention
            {
                throw new BusinessException(message: "Error:ImportHandler:551", code: "0");
            }

            List<T> entities = new();
            _entityProperties = GetEntityProperties();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    var worksheets = package.Workbook.Worksheets;

                    if (worksheets.Count % 2 != 0)
                    {
                        throw new BusinessException(message: "Error:ImportHandler:552", code: "0");
                    }

                    int tableCount = worksheets.Count / 2;
                    for (int i = 0; i < tableCount; i++)
                    {
                        var sheetTable = worksheets[i * 2];
                        var sheetData = worksheets[i * 2 + 1];

                        var data = ReadExcelToDatatable(sheetTable, sheetData);

                        entities = await CreateEntityList(data);
                    }
                }
            }

            await _repository.InsertManyAsync(entities);

            return entities.Count;
        }

        private async Task<List<T>> CreateEntityList(DataTable data)
        {
            List<T> result = new();
            Guid? tenantId = _currentTenant.Id;

            foreach (DataRow row in data.AsEnumerable())
            {
                T entity = new();
                Guid id = GuidGenerator.Create();
                foreach (DataColumn col in data.Columns)
                {
                    string propertyName = col.ColumnName;
                    if (!_entityProperties.ContainsKey(propertyName))
                    {
                        var detailDict = new Dictionary<string, string> { ["propertyName"] = propertyName };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: "Error:ImportHandler:553",
                            code: "0", details: detailString);
                    }
                    Type type = _entityProperties[propertyName];
                    var value = row[propertyName];
                    var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                    if (type == typeof(string))
                    {
                        string valueString = value == null || value == DBNull.Value ? "" : value.ToString();
                        property.SetValue(entity, valueString);
                    }
                    else if (type == typeof(bool))
                    {
                        property.SetValue(entity, (bool)(value ?? true));
                    }
                    else if (type == typeof(DateTime))
                    {
                        property.SetValue(entity, (DateTime)value);
                    }
                    else if (_knownNumberTypes.Contains(type))
                    {
                        property.SetValue(entity, value);
                    }
                    else if (type == typeof(Enum))
                    {
                        property.SetValue(entity, (Enum)value);
                    }
                    else if (type == typeof(Guid))
                    {
                        HandleGuidType(entity, property, propertyName, value, id);
                    }
                }
                SetTenantId(entity, tenantId);
                SetId(entity, row, id);
                result.Add(entity);
            }

            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromDB = await FindIdByCodeFromDB(result);
            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromGRPC = await FindIdByCodeFromGRPC(result, tenantId);
            FillIdByCodeProperties(result, idAndCodeFromDB, idAndCodeFromGRPC);

            return result;

        }

        private void FillIdByCodeProperties(List<T> entities, 
            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromDB,
            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromGRPC)
        {
            foreach (T entity in entities)
            {
                Type entityType = entity.GetType();
                PropertyInfo idProperty = entityType.GetProperty("Id");
                Guid entityId = (Guid)idProperty.GetValue(entity, null);
                Guid? id = null;
                foreach (string propertyName in _getIdByCodeFromDBOnly.Keys)
                {
                    string repoName = _getIdByCodeFromDBOnly[propertyName];
                    PropertyInfo property = entityType.GetProperty(propertyName);
                    string code = GetCodeForProperty(entityId, property, repoName);
                    id = GetIdByCodeFromDB(repoName, code, idAndCodeFromDB);
                    if (id == null && !_codePropertyAllowNull[propertyName])
                    {
                        var detailDict = new Dictionary<string, string> { ["code"] = code };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: "Error:ImportHandler:554",
                            code: "1", details: detailString);
                    }
                    property.SetValue(entity, id);
                }
                foreach (string propertyName in _getIdByCodeFromDBAndSheet.Keys)
                {
                    string repoName = _getIdByCodeFromDBAndSheet[propertyName];
                    PropertyInfo property = entityType.GetProperty(propertyName);
                    string code = GetCodeForProperty(entityId, property, repoName);
                    id = GetIdByCodeFromDB(repoName, code, idAndCodeFromDB);
                    if (id == null)
                    {
                        id = GetIdByCodeFromSheet(code);
                    }
                    if (id == null && !_codePropertyAllowNull[propertyName])
                    {
                        var detailDict = new Dictionary<string, string> { ["code"] = code };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: "Error:ImportHandler:555",
                            code: "1", details: detailString);
                    }
                    property.SetValue(entity, id);
                }
                foreach (string propertyName in _getIdFromGRPC.Keys)
                {
                    string repoName = _getIdFromGRPC[propertyName];
                    PropertyInfo property = entityType.GetProperty(propertyName);
                    string code = GetCodeForProperty(entityId, property, repoName);
                    id = GetIdByCodeFromGRPC(repoName, code, idAndCodeFromGRPC);
                    if (id == null && !_codePropertyAllowNull[propertyName])
                    {
                        var detailDict = new Dictionary<string, string> { ["code"] = code };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: "Error:ImportHandler:566",
                            code: "1", details: detailString);
                    }
                    property.SetValue(entity, id);
                }
            }
        }

        private Guid? GetIdByCodeFromGRPC(string repoName, string code,
            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromGRPC)
        {
            if (code == null)
            {
                return null;
            }
            Dictionary<string, Guid> idAndCode = idAndCodeFromGRPC[repoName];
            if (!idAndCode.ContainsKey(code))
            {
                return null;
            }
            return idAndCode[code];
        }

        private Guid? GetIdByCodeFromSheet(string code)
        {
            if (code == null)
            {
                return null;
            }
            if (!_entityCodeAndIdFromSheet.ContainsKey(code))
            {
                return null;
            }
            return _entityCodeAndIdFromSheet[code];
        }

        private string GetCodeForProperty(Guid entityId, PropertyInfo property, string repoName)
        {
            if (!_entityCodeValue.Keys.Contains(entityId))
            {
                throw new BusinessException(message: "Error:ImportHandler:556", code: "1");
            }
            Dictionary<string, string> codePropertyAndValue = _entityCodeValue[entityId];
            if (!codePropertyAndValue.ContainsKey(property.Name))
            {
                var detailDict = new Dictionary<string, string> { ["propertyName"] = property.Name };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: "Error:ImportHandler:557",
                    code: "1", details: detailString);
            }
            return codePropertyAndValue[property.Name];
        }

        private static Guid? GetIdByCodeFromDB(string repoName, string code,
            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromDB)
        {
            if (code == null)
            {
                return null;
            }
            Dictionary<string, Guid> idAndCode = idAndCodeFromDB[repoName];
            if (!idAndCode.ContainsKey(code))
            {
                return null;
            }
            return idAndCode[code];
        }

        private async Task<Dictionary<string, Dictionary<string, Guid>>> 
            FindIdByCodeFromGRPC(List<T> entities, Guid? tenantId)
        {
            Dictionary<string, Dictionary<string, Guid>> result = new();
            foreach (string repoName in _codeToGetFromGRPC.Keys)
            {
                List<string> codes = _codeToGetFromGRPC[repoName];
                if (codes.Count < 1)
                {
                    continue;
                }
                string connectionString = _grpcInfo[repoName];
                using (GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider[connectionString]))
                {
                    string typeName = $"{repoName}ProtoAppService.{repoName}ProtoAppServiceClient";
                    Type repoType = Type.GetType(typeName);
                    object client = Activator.CreateInstance(repoType, args: channel);
                    MethodInfo method = client.GetType().GetMethod("GetCodeAndIdWithCode");
                    if (method == null)
                    {
                        var detailDict = new Dictionary<string, string> { ["repoName"] = repoName };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: "Error:ImportHandler:564",
                            code: "1", details: detailString);
                    }

                    ListCodeAndIdRequest request = new();
                    request.TenantId = tenantId == null ? "" : tenantId.ToString();
                    request.Codes.Add(codes);
                    object resultTask = method.Invoke(client, new object[] { request });
                    if (resultTask is Task<ListCodeAndIdResponse> task)
                    {
                        ListCodeAndIdResponse response = await task;
                        List<CodeAndId> items = response.CodeAndIds.ToList();
                        if (items.Count != codes.Count)
                        {
                            throw new BusinessException(message: "Error:ImportHandler:565", code: "1");
                        }
                        Dictionary<string, Guid> foundCodeAndIds = new();
                        foreach (CodeAndId item in items)
                        {
                            string code = item.Code;
                            Guid id = Guid.Parse(item.Id);
                            foundCodeAndIds.Add(code, id);
                        }
                        result.Add(repoName, foundCodeAndIds);
                    }
                }
            }
            return result;
        }

        private async Task<Dictionary<string, Dictionary<string, Guid>>> FindIdByCodeFromDB(List<T> entities)
        {
            Dictionary<string, Dictionary<string, Guid>> result = new();
            foreach (string repoName in _codeToGetFromDB.Keys)
            {
                List<string> codes = _codeToGetFromDB[repoName];
                if (codes.Count < 1)
                {
                    continue;
                }
                if (!_repositories.ContainsKey(repoName))
                {
                    var detailDict = new Dictionary<string, string> { ["repoName"] = repoName };
                    string detailString = JsonSerializer.Serialize(detailDict).ToString();
                    throw new BusinessException(message: "Error:ImportHandler:558",
                        code: "1", details: detailString);
                }
                object repo = _repositories[repoName];
                Type repoType = repo.GetType();
                MethodInfo method = repoType.GetMethod("GetListIdByCodeAsync");
                if (method == null)
                {
                    var detailDict = new Dictionary<string, string> { ["repoName"] = repoName };
                    string detailString = JsonSerializer.Serialize(detailDict).ToString();
                    throw new BusinessException(message: "Error:ImportHandler:559",
                        code: "1", details: detailString);
                }

                object resultTask = method.Invoke(repo, new object[] { codes });
                if (resultTask is Task<Dictionary<string, Guid>> task)
                {
                    Dictionary<string, Guid> idAndCode = await task;
                    if (idAndCode.Count != codes.Count)
                    {
                        throw new BusinessException(message: "Error:ImportHandler:560", code: "1");
                    }
                    result.Add(repoName, idAndCode);
                }
            }
            return result;
        }

        private static void SetTenantId(T entity, Guid? tenantId)
        {
            var property = typeof(T).GetProperty("TenantId", BindingFlags.Public | BindingFlags.Instance);
            property.SetValue(entity, tenantId);
        }

        private void SetId(T entity, DataRow row, Guid id)
        {
            var property = typeof(T).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            property.SetValue(entity, id);
            if (_entityProperties.ContainsKey("Code"))
            {
                _entityCodeAndIdFromSheet.Add(row["Code"].ToString(), id);
            }
        }

        private void HandleGuidType(T entity, PropertyInfo property, string propertyName, Object value, Guid entityId)
        {
            if (!_getIdByCodeFromDBOnly.ContainsKey(propertyName) &&
                !_getIdByCodeFromDBAndSheet.ContainsKey(propertyName) &&
                !_getIdFromGRPC.ContainsKey(propertyName))
            {
                Guid? id = value == null || string.IsNullOrEmpty(value.ToString()) ?
                    null : Guid.Parse(value.ToString());
                property.SetValue(entity, id);
                return;
            }
            if (!_entityCodeValue.Keys.Contains(entityId))
            {
                _entityCodeValue.Add(entityId, new Dictionary<string, string>());
            }
            Dictionary<string, string> codePropertyNameAndValue = _entityCodeValue[entityId];
            if (value == null || value == DBNull.Value || string.IsNullOrEmpty(value.ToString()))
            {
                codePropertyNameAndValue.Add(propertyName, null);
                return;
            }

            string code = value.ToString();
            codePropertyNameAndValue.Add(propertyName, code);
            string repoName = "";
            Dictionary<string, List<string>> codeList = null;
            if (_getIdByCodeFromDBOnly.ContainsKey(propertyName))
            {
                repoName = _getIdByCodeFromDBOnly[propertyName];
                codeList = _codeToGetFromDB;
            }
            else if (_getIdByCodeFromDBAndSheet.ContainsKey(propertyName))
            {
                repoName = _getIdByCodeFromDBAndSheet[propertyName];
                codeList = _codeToGetFromDB;
            }
            else if (_getIdFromGRPC.ContainsKey(propertyName))
            {
                repoName = _getIdFromGRPC[propertyName];
                codeList = _codeToGetFromGRPC;
            }
            if (string.IsNullOrEmpty(repoName))
            {
                var detailDict = new Dictionary<string, string> { ["propertyName"] = propertyName };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: "Error:ImportHandler:561",
                    code: "1", details: detailString);
            }
            if (!codeList.ContainsKey(repoName))
            {
                codeList.Add(repoName, new List<string>());
            }
            List<string> codes = codeList[repoName];
            if (codes.Contains(code))
            {
                return;
            }
            codes.Add(value.ToString());
        }

        private DataTable ReadExcelToDatatable(ExcelWorksheet sheetTable, ExcelWorksheet sheetData)
        {
            DataTable dt = new();
            int colCount = sheetTable.Dimension.End.Column;
            int rowCount = sheetTable.Dimension.End.Row;

            for (int i = 2; i <= rowCount; i++)
            {
                var col = new DataColumn();
                col.ColumnName = sheetTable.Cells[i, 1].Value.ToString().Trim();

                var allowNull = bool.Parse(sheetTable.Cells[i, 3].Value.ToString().Trim());
                col.AllowDBNull = allowNull;

                var getIdByCodeFromDBAndSheetValue = sheetTable.Cells[i, 4].Value;
                if (getIdByCodeFromDBAndSheetValue != null)
                {
                    _getIdByCodeFromDBAndSheet.Add(col.ColumnName, getIdByCodeFromDBAndSheetValue.ToString().Trim());
                    _codePropertyAllowNull.Add(col.ColumnName, allowNull);
                }

                var getIdByCodeFromDBOnlyValue = sheetTable.Cells[i, 5].Value;
                if (getIdByCodeFromDBOnlyValue != null)
                {
                    _getIdByCodeFromDBOnly.Add(col.ColumnName, getIdByCodeFromDBOnlyValue.ToString().Trim());
                    _codePropertyAllowNull.Add(col.ColumnName, allowNull);
                }

                var getIdFromGRPCValue = sheetTable.Cells[i, 6].Value;
                if (getIdFromGRPCValue != null)
                {
                    string protoName = getIdFromGRPCValue.ToString().Trim();
                    var gRPCConnectionString = sheetTable.Cells[i, 7].Value;
                    if (gRPCConnectionString != null)
                    {
                        string connectionString = gRPCConnectionString.ToString().Trim();
                        _getIdFromGRPC.Add(col.ColumnName, protoName);
                        _grpcInfo.Add(protoName, connectionString);
                        _codePropertyAllowNull.Add(col.ColumnName, allowNull);
                    }
                    else
                    {
                        var detailDict = new Dictionary<string, string> { ["columnName"] = col.ColumnName };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: "Error:ImportHandler:563",
                            code: "1", details: detailString);
                    }
                }

                switch (sheetTable.Cells[i, 2].Value.ToString().Trim())
                {
                    case "string":
                        col.DataType = typeof(string);
                        col.DefaultValue = string.Empty;
                        break;
                    case "int":
                        col.DataType = typeof(int);
                        col.DefaultValue = 0;
                        break;
                    case "decimal":
                        col.DataType = typeof(decimal);
                        col.DefaultValue = 0;
                        break;
                    case "date":
                    case "datetime":
                        col.DataType = typeof(DateTime);
                        col.DefaultValue = DateTime.Now;
                        break;
                    case "bit":
                    case "bool":
                        col.DataType = typeof(bool);
                        col.DefaultValue = true;
                        break;
                    case "guid": col.DataType = typeof(Guid); break;
                }

                dt.Columns.Add(col);
            }


            colCount = sheetData.Dimension.End.Column;
            rowCount = sheetData.Dimension.End.Row;

            //start adding the contents of the excel file to the companies
            for (int i = 2; i <= rowCount; i++)
            {
                var row = sheetData.Cells[i, 1, i, colCount];
                int count = row.Columns;
                var newRow = dt.NewRow();

                //loop all cells in the row
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Value;
                }
                dt.Rows.Add(newRow);
            }

            return dt;
        }

        private static Dictionary<string, Type> GetEntityProperties()
        {
            Dictionary<string, Type> result = new();

            List<Type> knownTypes = new()
            {
                typeof(string),
                typeof(bool),
                typeof(DateTime),
                typeof(Guid),
                typeof(Enum),
                typeof(ExtraPropertyDictionary),
            };
            knownTypes.AddRange(_knownNumberTypes);
            foreach (PropertyInfo prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (!knownTypes.Contains(type))
                {
                    var detailDict = new Dictionary<string, string>
                    {
                        ["propertyName"] = prop.Name,
                        ["typeName"] = type.Name,
                    };
                    string detailString = JsonSerializer.Serialize(detailDict).ToString();
                    throw new BusinessException(message: "Error:ImportHandler:562",
                        code: "1", details: detailString);
                }
                result.Add(prop.Name, type);
            }
            return result;
        }
    }
}
