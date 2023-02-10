using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Protos.Shared.Import;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Grpc.Core;

namespace DMSpro.OMS.MdmService.Partial
{
    public class PartialAppService<T, TDto, TRepository> : ApplicationService, IPartialAppService
        where T : class, IEntity, new()
        where TDto : class
        where TRepository : class, IRepository<T>
    {
        protected readonly IRepository<T> _repository;
        protected readonly ICurrentTenant _currentTenant;
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
        private readonly Dictionary<string, string> _grpcConnectionString= new();
        private readonly Dictionary<string, string> _grpcNamespace = new();
        private readonly List<string> _codeFromDBAndSheetRepo = new();
        
        protected readonly Dictionary<string, object> _repositories = new();

        public PartialAppService(ICurrentTenant currentTenant,
            TRepository repository,
            IConfiguration settingProvider)
        {
            _repository = repository;
            _settingProvider = settingProvider;
            _currentTenant = currentTenant;
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
                SetId(entity, row, id);
                result.Add(entity);
            }
            if (_entityProperties.ContainsKey("Code"))
            {
                await CheckForCodeUniqueness();
            }

            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromDB = await FindIdByCodeFromDB();
            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromGRPC = await FindIdByCodeFromGRPC();
            FillIdByCodeProperties(result, idAndCodeFromDB, idAndCodeFromGRPC);

            return result;

        }

        private async Task CheckForCodeUniqueness()
        {
            List<string> codes = _entityCodeAndIdFromSheet.Keys.ToList();
            Type repoType = _repository.GetType();
            MethodInfo method = repoType.GetMethod("GetCountByCodeAsync");
            if (method == null)
            {
                throw new BusinessException(message: "Error:ImportHandler:567", code: "1");
            }

            object resultTask = method.Invoke(_repository, new object[] { codes });
            if (resultTask is Task<int> task)
            {
                int idCount = await task;
                if (idCount > 0)
                {
                    throw new BusinessException(message: "Error:ImportHandler:568", code: "0");
                }
            }
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
                foreach (string propertyName in _getIdByCodeFromDBOnly.Keys)
                {
                    SetIdProperty(propertyName, _getIdByCodeFromDBOnly, CheckTypes.DB_ONLY,
                        entityType, entityId, entity, idAndCodeFromDB, idAndCodeFromGRPC,
                        "Error:ImportHandler:554");
                }
                foreach (string propertyName in _getIdByCodeFromDBAndSheet.Keys)
                {
                    SetIdProperty(propertyName, _getIdByCodeFromDBAndSheet, CheckTypes.DB_AND_SHEET,
                        entityType, entityId, entity, idAndCodeFromDB, idAndCodeFromGRPC,
                        "Error:ImportHandler:555");
                }
                foreach (string propertyName in _getIdFromGRPC.Keys)
                {
                    SetIdProperty(propertyName, _getIdFromGRPC, CheckTypes.GRPC,
                        entityType, entityId, entity, idAndCodeFromDB, idAndCodeFromGRPC,
                        "Error:ImportHandler:556");
                }
            }
        }

        private void SetIdProperty(string propertyName, Dictionary<string, string> repoDictionary,
            CheckTypes type, Type entityType, Guid entityId, T entity,
            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromDB,
            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromGRPC,
            string errorString)
        {
            Guid? id;
            string repoName = repoDictionary[propertyName];
            PropertyInfo property = entityType.GetProperty(propertyName);
            string code = GetCodeForProperty(entityId, property);
            if (code == null && _codePropertyAllowNull[propertyName])
            {
                property.SetValue(entity, null);
                return;
            }
            if (type != CheckTypes.GRPC)
            {
                id = GetIdByCodeFromDB(repoName, code, idAndCodeFromDB);
            }
            else
            {
                id = GetIdByCodeFromGRPC(repoName, code, idAndCodeFromGRPC);
            }
            if (id == null && type == CheckTypes.DB_AND_SHEET)
            {
                id = GetIdByCodeFromSheet(code);
            }
            if (id == null && !_codePropertyAllowNull[propertyName])
            {
                var detailDict = new Dictionary<string, string> { ["code"] = code };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: errorString,
                    code: "1", details: detailString);
            }
            property.SetValue(entity, id);
        }

        private static Guid? GetIdByCodeFromGRPC(string repoName, string code,
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

        private string GetCodeForProperty(Guid entityId, PropertyInfo property)
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

        private async Task<Dictionary<string, Dictionary<string, Guid>>> FindIdByCodeFromGRPC()
        {
            Guid? tenantId = _currentTenant.Id;
            Dictionary<string, Dictionary<string, Guid>> result = new();
            foreach (string repoName in _codeToGetFromGRPC.Keys)
            {
                List<string> codes = _codeToGetFromGRPC[repoName];
                if (codes.Count < 1)
                {
                    continue;
                }
                string connectionString = _grpcConnectionString[repoName];
                string namespaceString = _grpcNamespace[repoName];
                using (GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider.GetValue<string>(connectionString)))
                {
                    Type protoType = Type.GetType($"{namespaceString}.{repoName}ProtoAppService");
                    Type protoClientType = (Type) protoType.GetMember($"{repoName}ProtoAppServiceClient")[0];
                    object client = Activator.CreateInstance(protoClientType, args: channel);
                    MethodInfo method = client.GetType().GetMethod("GetCodeAndIdWithCodeAsync",
                        BindingFlags.Instance | BindingFlags.Public,
                        new Type[] { typeof(ListCodeAndIdRequest), typeof(CallOptions) });
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
                    object resultTask = method.Invoke(client, new object[] { request, new CallOptions() });
                    if (resultTask is AsyncUnaryCall<ListCodeAndIdResponse> task)
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

        private async Task<Dictionary<string, Dictionary<string, Guid>>> FindIdByCodeFromDB()
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

                var task = (Task<Dictionary<string, Guid>>)method.Invoke(repo, new object[] { codes });
                Dictionary<string, Guid> idAndCode = await task;
                if (!_codeFromDBAndSheetRepo.Contains(repoName) && idAndCode.Count != codes.Count)
                {
                    throw new BusinessException(message: "Error:ImportHandler:560", code: "1");
                }
                result.Add(repoName, idAndCode);
            }
            return result;
        }

        private void SetId(T entity, DataRow row, Guid id)
        {
            var property = typeof(T).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            property.SetValue(entity, id);
            if (!_entityProperties.ContainsKey("Code"))
            {
                return;
            }
            string code = row["Code"].ToString().Trim();
            if (_entityCodeAndIdFromSheet.ContainsKey(code))
            {
                var detailDict = new Dictionary<string, string> { ["code"] = code };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: "Error:ImportHandler:569", code: "0", details: detailString);
            }
            _entityCodeAndIdFromSheet.Add(code, id);
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
                if (!_codeFromDBAndSheetRepo.Contains(repoName))
                {
                    _codeFromDBAndSheetRepo.Add(repoName);
                }
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
                        var gRPCNamespace = sheetTable.Cells[i, 8].Value;
                        if (gRPCNamespace != null)
                        {
                            string namespaceString = gRPCNamespace.ToString().Trim();
                            _getIdFromGRPC.Add(col.ColumnName, protoName);
                            _grpcConnectionString.Add(protoName, connectionString);
                            _grpcNamespace.Add(protoName, namespaceString);
                            _codePropertyAllowNull.Add(col.ColumnName, allowNull);
                        }
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

        private enum CheckTypes
        {
            DB_ONLY,
            DB_AND_SHEET,
            GRPC,
        }
    }
}
