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
using Volo.Abp.Application.Dtos;
using System.Reflection;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService
{
    public class PartialsAppService<T, TDto, TRepository> : ApplicationService, IPartialsAppservice
        where T : class, IEntity, new()
        where TDto : class
        where TRepository : class, IRepository<T>
    {
        protected readonly IRepository<T> _repository;

        private readonly ICurrentTenant _currentTenant;
        private static readonly List<Type> _knownNumberTypes = new()
        {
            typeof(uint),
            typeof(int),
            typeof(decimal),
        };

        private Dictionary<string, Type> _entityProperties = new();
        private readonly Dictionary<string, string> _getIdByCodeFromDBAndSheet = new();
        private readonly Dictionary<string, string> _getIdByCodeFromDBOnly = new();
        private readonly Dictionary<string, Guid> _entityCodeAndIdFromSheet = new();
        private readonly Dictionary<string, List<string>> _codeToGetFromDB = new();
        private readonly Dictionary<Guid, Dictionary<string, string>> _entityCodeValue = new();
        private readonly Dictionary<string, bool> _codePropertyAllowNull = new();

        protected readonly Dictionary<string, object> _repositories = new();

        public PartialsAppService(ICurrentTenant currentTenant, TRepository repository)
        {
            _repository = repository;
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
                throw new BusinessException(message: L["Error:EmptyFormFile"], code: "0");
            }

            if (!(Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
                || Path.GetExtension(file.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase)))
            //not support file extention
            {
                throw new BusinessException(message: L["Error:ImportFileNotSupported"], code: "0");
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
                        throw new BusinessException(message: L["Error:ImportFileNotSupported"], code: "0");
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
            await UnitOfWorkManager.Current.SaveChangesAsync();

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
                        throw new BusinessException(message: $"Entity does not have a property named {propertyName}.", code: "1");
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
            FillIdByCodeProperties(result, idAndCodeFromDB);

            return result;

        }

        private void FillIdByCodeProperties(List<T> entities, Dictionary<string, Dictionary<string, Guid>> idAndCodeFromDB)
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
                        throw new BusinessException(
                            message: $"There is no Id value can be found in database for code {code}", code: "1");
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
                        throw new BusinessException(
                           message: $"There is no Id value can be found in database or sheet for code {code}", code: "1");
                    }
                    property.SetValue(entity, id);
                }
            }
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
                throw new BusinessException(message: $"There is no Code value can be found for an entity", code: "1");
            }
            Dictionary<string, string> codePropertyAndValue = _entityCodeValue[entityId];
            if (!codePropertyAndValue.ContainsKey(property.Name))
            {
                throw new BusinessException(message: $"There is code value can be found for property {property.Name}",
                    code: "1");
            }
            return codePropertyAndValue[property.Name];
        }

        private Guid? GetIdByCodeFromDB(string repoName, string code,
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
                    throw new BusinessException(message: $"Cannot find repository with name {repoName} to get Id by code", code: "1");
                }
                object repo = _repositories[repoName];
                Type repoType = repo.GetType();
                MethodInfo method = repoType.GetMethod("GetListIdByCodeAsync");
                if (method == null)
                {
                    throw new BusinessException(message: $"Repository {repoName} does not have the required methods", code: "1");
                }

                object resultTask = method.Invoke(repo, new object[] { codes });
                if (resultTask is Task<Dictionary<string, Guid>> task)
                {
                    Dictionary<string, Guid> idAndCode = await task;
                    if (idAndCode.Count != codes.Count)
                    {
                        throw new BusinessException(message: "Not all Code can be found in database", code: "1");
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
                !_getIdByCodeFromDBAndSheet.ContainsKey(propertyName))
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
            if (_getIdByCodeFromDBOnly.ContainsKey(propertyName))
            {
                repoName = _getIdByCodeFromDBOnly[propertyName];
            }
            else if (_getIdByCodeFromDBAndSheet.ContainsKey(propertyName))
            {
                repoName = _getIdByCodeFromDBAndSheet[propertyName];
            }
            if (string.IsNullOrEmpty(repoName))
            {
                throw new BusinessException(message: $"Cannot find a repository to check for property {propertyName}", code: "1");
            }
            if (!_codeToGetFromDB.ContainsKey(repoName))
            {
                _codeToGetFromDB.Add(repoName, new List<string>());
            }
            List<string> codes = _codeToGetFromDB[repoName];
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
                    throw new BusinessException(message: $"Unknown entity's property ({prop.Name}) of unknown type ({type.Name}) encountered during import");
                }
                result.Add(prop.Name, type);
            }
            return result;
        }
    }
}
