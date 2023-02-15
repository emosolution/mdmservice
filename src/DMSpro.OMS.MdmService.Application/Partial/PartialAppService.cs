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
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Localization;

namespace DMSpro.OMS.MdmService.Partial
{
    public class PartialAppService<T, TDto, TRepository> : ApplicationService
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

        private Dictionary<string, Type> _entityPropertyTypes = new();
        private Dictionary<string, PropertyInfo> _entityPropertyInfos = new();
        private readonly Dictionary<string, string> _getIdByCodeFromDBAndSheet = new();
        private readonly Dictionary<string, string> _getIdByCodeFromDBOnly = new();
        private readonly Dictionary<string, string> _getIdFromGRPC = new();
        private readonly Dictionary<string, Guid> _entityCodeAndIdFromSheet = new();
        private readonly Dictionary<string, List<string>> _codeToGetFromDB = new();
        private readonly Dictionary<string, List<string>> _codeToGetFromGRPC = new();
        private readonly Dictionary<Guid, Dictionary<string, string>> _entityCodeValue = new();
        private readonly Dictionary<string, string> _grpcConnectionString = new();
        private readonly Dictionary<string, string> _grpcNamespace = new();
        private readonly List<string> _codeFromDBAndSheetRepo = new();
        private readonly Dictionary<string, bool> _structureAllowNull = new();
        private readonly Dictionary<string, Type> _structureType = new();
        private readonly List<string> _structurePropertyName = new();
        private readonly List<Guid> _guidForUpdate = new();

        protected readonly Dictionary<string, object> _repositories = new();

        public PartialAppService(ICurrentTenant currentTenant,
            TRepository repository,
            IConfiguration settingProvider)
        {
            _repository = repository;
            _settingProvider = settingProvider;
            _currentTenant = currentTenant;

            LocalizationResource = typeof(MdmServiceResource);
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _repository.GetQueryableAsync();
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            if (inputDev.Group == null)
            {
                results.data = ObjectMapper.Map<IEnumerable<T>, IEnumerable<TDto>>(results.data.Cast<T>());
            }
            return results;
        }

        public async Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
        {
            DataTable data = GetDataTableFromFile(file, OperationMode.UPDATE);
            _entityPropertyTypes = GetEntityProperties();
            List<T> databaseEntities = await CheckIdForUpdate();
            List<T> entities = await CreateEntityList(data, OperationMode.UPDATE);
            Dictionary<Guid, T> entitiyDictionary = new();
            foreach (T entity in entities)
            {
                Guid id = (Guid)_entityPropertyInfos["Id"].GetValue(entity);
                entitiyDictionary.Add(id, entity);
            }
            await UpdateDatabaseEntities(databaseEntities, entitiyDictionary, data);
            return databaseEntities.Count;
        }

        public async Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
        {
            DataTable data = GetDataTableFromFile(file, OperationMode.INSERT);
            _entityPropertyTypes = GetEntityProperties();
            List<T> entities = await CreateEntityList(data, OperationMode.INSERT);
            await _repository.InsertManyAsync(entities);
            return entities.Count;
        }

        private async Task<int> UpdateDatabaseEntities(List<T> databaseEntities,
            Dictionary<Guid, T> entitiyDictionary, DataTable data)
        {
            foreach (T entity in databaseEntities)
            {
                Guid id = (Guid)_entityPropertyInfos["Id"].GetValue(entity);
                foreach (DataColumn col in data.Columns)
                {
                    string propertyName = col.ColumnName;
                    if (propertyName == "Id")
                    {
                        continue;
                    }
                    var property = _entityPropertyInfos[propertyName];
                    var newValue = property.GetValue(entitiyDictionary[id]);
                    property.SetValue(entity, newValue);
                }
            }
            Type repoType = _repository.GetType();
            MethodInfo method = repoType.GetMethod("UpdateManyAsync");
            object resultTask = method.Invoke(_repository, new object[] { databaseEntities, Type.Missing, Type.Missing });
            await (Task)resultTask;
            return databaseEntities.Count;
        }

        private async Task<List<T>> CheckIdForUpdate()
        {
            Type repoType = _repository.GetType();
            MethodInfo method = repoType.GetMethod("GetByIdAsync");
            if (method == null)
            {
                throw new BusinessException(message: L["Error:ImportHandler:577"], code: "1");
            }

            object resultTask = method.Invoke(_repository, new object[] { _guidForUpdate });
            if (resultTask is Task<List<T>> task)
            {
                List<T> result = await task;
                if (result.Count != _guidForUpdate.Count)
                {
                    throw new BusinessException(message: L["Error:ImportHandler:578"], code: "0");
                }
                return result;
            }
            throw new BusinessException(message: L["Error:ImportHandler:582"], code: "1");
        }

        private DataTable GetDataTableFromFile(IRemoteStreamContent file, OperationMode operationMode)
        {
            if (file == null || file.ContentLength <= 0) //file empty
            {
                throw new BusinessException(message: L["Error:ImportHandler:550"], code: "0");
            }

            if (!(Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
                || Path.GetExtension(file.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase)))
            //not support file extention
            {
                throw new BusinessException(message: L["Error:ImportHandler:551"], code: "0");
            }

            DataTable result = null;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(file.GetStream()))
            {
                var worksheets = package.Workbook.Worksheets;

                if (worksheets.Count % 2 != 0)
                {
                    throw new BusinessException(message: L["Error:ImportHandler:552"], code: "0");
                }

                int tableCount = worksheets.Count / 2;
                for (int i = 0; i < tableCount; i++)
                {
                    var sheetStructure = worksheets[i * 2];
                    var sheetData = worksheets[i * 2 + 1];

                    result = ReadExcelToDatatable(sheetStructure, sheetData, operationMode);
                }
            }
            if (result == null)
            {
                throw new BusinessException(message: L["Error:ImportHandler:571"], code: "0");
            }
            return result;
        }

        private async Task<List<T>> CreateEntityList(DataTable data, OperationMode operationMode)
        {
            List<T> result = new();
            foreach (DataRow row in data.AsEnumerable())
            {
                T entity = new();
                Guid id;
                if (operationMode == OperationMode.INSERT)
                {
                    id = GuidGenerator.Create();
                }
                else if (operationMode == OperationMode.UPDATE)
                {
                    id = (Guid)row["Id"];
                }
                else
                {
                    throw new BusinessException(message: L["Error:ImportHandler:581"], code: "1");
                }
                foreach (DataColumn col in data.Columns)
                {
                    string propertyName = col.ColumnName;
                    if (!_entityPropertyTypes.ContainsKey(propertyName))
                    {
                        var detailDict = new Dictionary<string, string> { ["propertyName"] = propertyName };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: L["Error:ImportHandler:553"],
                            code: "0", details: detailString);
                    }
                    var value = row[propertyName];
                    Type type = _entityPropertyTypes[propertyName];
                    var property = _entityPropertyInfos[propertyName];
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
            if (_structurePropertyName.Contains("Code"))
            {
                await CheckCodeForUniqueness(operationMode);
            }

            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromDB = await FindIdByCodeFromDB();
            Dictionary<string, Dictionary<string, Guid>> idAndCodeFromGRPC = await FindIdByCodeFromGRPC();
            FillIdByCodeProperties(result, idAndCodeFromDB, idAndCodeFromGRPC);

            return result;

        }

        private async Task CheckCodeForUniqueness(OperationMode operationMode)
        {
            if (_entityCodeAndIdFromSheet.Count < 1)
            {
                return;
            }
            if (operationMode == OperationMode.INSERT)
            {
                await CheckInsertCodeUniqueness();
            }
            else if (operationMode == OperationMode.UPDATE)
            {
                await CheckUpdateCodeUniqueness();
            }
            else
            {
                throw new BusinessException(message: L["Error:ImportHandler:581"], code: "1");
            }
        }

        private async Task CheckUpdateCodeUniqueness()
        {
            List<string> codes = _entityCodeAndIdFromSheet.Keys.ToList();
            List<Guid> ids = _entityCodeAndIdFromSheet.Values.ToList();
            Type repoType = _repository.GetType();
            MethodInfo method = repoType.GetMethod("CheckUniqueCodeForUpdate");
            if (method == null)
            {
                throw new BusinessException(message: L["Error:ImportHandler:583"], code: "1");
            }

            object resultTask = method.Invoke(_repository, new object[] { codes, ids });
            if (resultTask is Task<bool> task)
            {
                bool noDuplicateCodeInDb = await task;
                if (!noDuplicateCodeInDb)
                {
                    throw new BusinessException(message: L["Error:ImportHandler:568"], code: "0");
                }
            }
        }

        private async Task CheckInsertCodeUniqueness()
        {
            List<string> codes = _entityCodeAndIdFromSheet.Keys.ToList();
            Type repoType = _repository.GetType();
            MethodInfo method = repoType.GetMethod("GetCountByCodeAsync");
            if (method == null)
            {
                throw new BusinessException(message: L["Error:ImportHandler:567"], code: "1");
            }

            object resultTask = method.Invoke(_repository, new object[] { codes });
            if (resultTask is Task<int> task)
            {
                int idCount = await task;
                if (idCount > 0)
                {
                    throw new BusinessException(message: L["Error:ImportHandler:568"], code: "0");
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
            if (code == null && _structureAllowNull[propertyName])
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
            if (id == null && !_structureAllowNull[propertyName])
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
                throw new BusinessException(message: L["Error:ImportHandler:556"], code: "1");
            }
            Dictionary<string, string> codePropertyAndValue = _entityCodeValue[entityId];
            if (!codePropertyAndValue.ContainsKey(property.Name))
            {
                var detailDict = new Dictionary<string, string> { ["propertyName"] = property.Name };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:ImportHandler:557"],
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
                    Type protoClientType = (Type)protoType.GetMember($"{repoName}ProtoAppServiceClient")[0];
                    object client = Activator.CreateInstance(protoClientType, args: channel);
                    MethodInfo method = client.GetType().GetMethod("GetCodeAndIdWithCodeAsync",
                        BindingFlags.Instance | BindingFlags.Public,
                        new Type[] { typeof(ListCodeAndIdRequest), typeof(CallOptions) });
                    if (method == null)
                    {
                        var detailDict = new Dictionary<string, string> { ["repoName"] = repoName };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: L["Error:ImportHandler:564"],
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
                            throw new BusinessException(message: L["Error:ImportHandler:565"], code: "1");
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
                    throw new BusinessException(message: L["Error:ImportHandler:558"],
                        code: "1", details: detailString);
                }
                object repo = _repositories[repoName];
                Type repoType = repo.GetType();
                MethodInfo method = repoType.GetMethod("GetListIdByCodeAsync");
                if (method == null)
                {
                    var detailDict = new Dictionary<string, string> { ["repoName"] = repoName };
                    string detailString = JsonSerializer.Serialize(detailDict).ToString();
                    throw new BusinessException(message: L["Error:ImportHandler:559"],
                        code: "1", details: detailString);
                }

                var task = (Task<Dictionary<string, Guid>>)method.Invoke(repo, new object[] { codes });
                Dictionary<string, Guid> idAndCode = await task;
                if (!_codeFromDBAndSheetRepo.Contains(repoName) && idAndCode.Count != codes.Count)
                {
                    throw new BusinessException(message: L["Error:ImportHandler:560"], code: "1");
                }
                result.Add(repoName, idAndCode);
            }
            return result;
        }

        private void SetId(T entity, DataRow row, Guid id)
        {
            var property = typeof(T).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            property.SetValue(entity, id);
            if (!_entityPropertyTypes.ContainsKey("Code"))
            {
                return;
            }
            string code = row["Code"].ToString().Trim();
            if (_entityCodeAndIdFromSheet.ContainsKey(code))
            {
                var detailDict = new Dictionary<string, string> { ["code"] = code };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:ImportHandler:569"], code: "0", details: detailString);
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
                throw new BusinessException(message: L["Error:ImportHandler:561"],
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

        private DataTable ReadExcelToDatatable(ExcelWorksheet sheetStructure,
            ExcelWorksheet sheetData, OperationMode operationMode)
        {
            GetStructureInfo(sheetStructure);

            int colCount = sheetData.Dimension.End.Column;
            int rowCount = sheetData.Dimension.End.Row;

            if (rowCount < 2 || colCount < 2)
            {
                throw new BusinessException(message: L["Error:ImportHandler:572"], code: "0");
            }
            DataTable dt = new();
            List<string> approvedColumns = new();
            List<string> checkedColumn = new();
            Dictionary<string, string> columnLetters = new();
            int approvedNum = 0;
            for (int i = 1; i <= colCount; i++)
            {
                var cell = sheetData.Cells[1, i];
                string propertyName = sheetData.Cells[1, i].Value.ToString();
                string columnLetter = cell.EntireColumn.Range.Address.Split(":")[0];
                if (propertyName.CompareTo("Id") == 0 && operationMode == OperationMode.UPDATE)
                {
                    DataColumn col = new();
                    col.AllowDBNull = false;
                    col.ColumnName = propertyName;
                    col.DataType = typeof(Guid);
                    dt.Columns.Add(col);
                    approvedColumns.Add(propertyName);
                    columnLetters.Add(columnLetter, propertyName);
                }
                else if (_structurePropertyName.Contains(propertyName))
                {
                    approvedNum++;
                    DataColumn col = new();
                    col.AllowDBNull = _structureAllowNull[propertyName];
                    col.ColumnName = propertyName;
                    col.DataType = _structureType[propertyName];
                    switch (col.DataType.ToString())
                    {
                        case "System.String":
                            col.DefaultValue = string.Empty;
                            break;
                        case "System.Int32":
                            col.DefaultValue = 0;
                            break;
                        case "System.Decimal":
                            col.DefaultValue = 0;
                            break;
                        case "System.DateTime":
                            col.DefaultValue = DateTime.Now;
                            break;
                        case "System.Boolean":
                            col.DefaultValue = true;
                            break;
                    }
                    dt.Columns.Add(col);
                    approvedColumns.Add(propertyName);
                    columnLetters.Add(columnLetter, propertyName);
                }
                if (checkedColumn.Contains(propertyName))
                {
                    throw new BusinessException(message: L["Error:ImportHandler:574"], code: "0");
                }
                checkedColumn.Add(propertyName);
            }
            if (operationMode == OperationMode.INSERT && approvedNum != _structurePropertyName.Count)
            {
                throw new BusinessException(message: L["Error:ImportHandler:573"], code: "0");
            }
            else if (operationMode == OperationMode.UPDATE && !approvedColumns.Contains("Id"))
            {
                throw new BusinessException(message: L["Error:ImportHandler:575"], code: "0");
            }

            PopulateDataTableFromSheetData(sheetData, dt, rowCount, colCount,
                columnLetters, approvedColumns);

            return dt;
        }

        private void PopulateDataTableFromSheetData(ExcelWorksheet sheetData, DataTable dt,
            int rowCount, int colCount,
            Dictionary<string, string> columnLetters,
            List<string> approvedColumns)
        {
            for (int i = 2; i <= rowCount; i++)
            {
                var newRow = dt.NewRow();
                var sheetRow = sheetData.Cells[i, 1, i, colCount];
                foreach (var cell in sheetRow)
                {
                    string columnLetter = cell.EntireColumn.Range.Address.Split(":")[0];
                    if (!columnLetters.ContainsKey(columnLetter))
                    {
                        continue;
                    }
                    string propertyName = columnLetters[columnLetter];
                    if (!approvedColumns.Contains(propertyName))
                    {
                        continue;
                    }
                    newRow[propertyName] = cell.Value;
                    if (propertyName.CompareTo("Id") == 0)
                    {
                        Guid id = ParseGuidForUpdate(cell, i);
                        if (_guidForUpdate.Contains(id))
                        {
                            var detailDict = new Dictionary<string, string> { ["row"] = i.ToString() };
                            string detailString = JsonSerializer.Serialize(detailDict).ToString();
                            throw new BusinessException(message: L["Error:ImportHandler:579"], code: "0", details: detailString);
                        }
                        _guidForUpdate.Add(id);

                    }
                }
                dt.Rows.Add(newRow);
            }
        }

        private Guid ParseGuidForUpdate(ExcelRangeBase cell, int row)
        {
            if (cell.Value == null || cell.Value == DBNull.Value)
            {
                var detailDict = new Dictionary<string, string> { ["row"] = row.ToString() };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:ImportHandler:580"], code: "0", details: detailString);
            }
            try
            {
                Guid id = Guid.Parse(cell.Value.ToString());
                return id;
            }
            catch (Exception)
            {
                var detailDict = new Dictionary<string, string> { ["row"] = row.ToString() };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:ImportHandler:576"], code: "0", details: detailString);
            }
        }

        private void GetStructureInfo(ExcelWorksheet sheetStructure)
        {
            int rowCount = sheetStructure.Dimension.End.Row;
            for (int i = 2; i <= rowCount; i++)
            {
                string propertyName = sheetStructure.Cells[i, 1].Value.ToString().Trim();

                var allowNull = bool.Parse(sheetStructure.Cells[i, 3].Value.ToString().Trim());
                _structureAllowNull.Add(propertyName, allowNull);

                var getIdByCodeFromDBAndSheetValue = sheetStructure.Cells[i, 4].Value;
                if (getIdByCodeFromDBAndSheetValue != null)
                {
                    _getIdByCodeFromDBAndSheet.Add(propertyName, getIdByCodeFromDBAndSheetValue.ToString().Trim());
                }

                var getIdByCodeFromDBOnlyValue = sheetStructure.Cells[i, 5].Value;
                if (getIdByCodeFromDBOnlyValue != null)
                {
                    _getIdByCodeFromDBOnly.Add(propertyName, getIdByCodeFromDBOnlyValue.ToString().Trim());
                }

                var getIdFromGRPCValue = sheetStructure.Cells[i, 6].Value;
                if (getIdFromGRPCValue != null)
                {
                    string protoName = getIdFromGRPCValue.ToString().Trim();
                    var gRPCConnectionString = sheetStructure.Cells[i, 7].Value;
                    if (gRPCConnectionString != null)
                    {
                        string connectionString = gRPCConnectionString.ToString().Trim();
                        var gRPCNamespace = sheetStructure.Cells[i, 8].Value;
                        if (gRPCNamespace != null)
                        {
                            string namespaceString = gRPCNamespace.ToString().Trim();
                            _getIdFromGRPC.Add(propertyName, protoName);
                            _grpcConnectionString.Add(protoName, connectionString);
                            _grpcNamespace.Add(protoName, namespaceString);
                        }
                    }
                    else
                    {
                        var detailDict = new Dictionary<string, string> { ["columnName"] = propertyName };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: L["Error:ImportHandler:563"],
                            code: "1", details: detailString);
                    }
                }

                switch (sheetStructure.Cells[i, 2].Value.ToString().Trim())
                {
                    case "string":
                        _structureType.Add(propertyName, typeof(string));
                        break;
                    case "int":
                        _structureType.Add(propertyName, typeof(int));
                        break;
                    case "decimal":
                        _structureType.Add(propertyName, typeof(decimal));
                        break;
                    case "date":
                    case "datetime":
                        _structureType.Add(propertyName, typeof(DateTime));
                        break;
                    case "bit":
                    case "bool":
                        _structureType.Add(propertyName, typeof(bool));
                        break;
                    case "guid":
                        _structureType.Add(propertyName, typeof(Guid));
                        break;
                }

                _structurePropertyName.Add(propertyName);
            }
        }

        private Dictionary<string, Type> GetEntityProperties()
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
                    throw new BusinessException(message: L["Error:ImportHandler:562"],
                        code: "1", details: detailString);
                }
                result.Add(prop.Name, type);
                _entityPropertyInfos.Add(prop.Name, prop);
            }
            return result;
        }

        private enum CheckTypes
        {
            DB_ONLY,
            DB_AND_SHEET,
            GRPC,
        }

        private enum OperationMode
        {
            INSERT,
            UPDATE,
        }
    }
}
