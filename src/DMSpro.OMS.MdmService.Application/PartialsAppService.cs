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
using Org.BouncyCastle.Asn1.Cms;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService
{
    public class PartialsAppService<T, TDto, TRepository> : ApplicationService, IPartialsAppservice
        where T : class, IEntity<Guid>, new()
        where TDto : class, IEntityDto
        where TRepository : class, IRepository<T, Guid>
    {
        protected readonly IRepository<T, Guid> _repository;
        
        private readonly Dictionary<string, Type> _entityProperties = new();
        private static readonly Dictionary<string, bool> _entityGetIdByCode = new();
        private static readonly List<Type> _knownNumberTypes = new()
        {
            typeof(uint),
            typeof(int),
            typeof(decimal),
        };

        public PartialsAppService(TRepository repository)
        {
            _repository = repository;
            _entityProperties = GetEntityProperties();
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

            var entities = new List<T>();

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

            return entities.Count;
        }

        private async Task<List<T>> CreateEntityList(DataTable data)
        {
            List<T> result = new();
            foreach (DataRow row in data.AsEnumerable())
            {
                T entity = new();
                foreach (string propertyName in _entityProperties.Keys)
                {
                    Type type = _entityProperties[propertyName];
                    var value = row[propertyName];
                    var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                    if (type == typeof(string))
                    {
                        property.SetValue(entity, value);
                    }
                    else if (type == typeof(bool))
                    {
                        property.SetValue(entity, (bool)(value ?? true));
                    }
                    else if (type == typeof(DateTime))
                    {
                        property.SetValue(entity, (DateTime) value);
                    }
                    else if (_knownNumberTypes.Contains(type))
                    {
                        property.SetValue(entity, value);
                    }
                    else if (type == typeof(Enum))
                    {
                        property.SetValue(entity, (Enum) value);
                    }else if (type == typeof(Guid))
                    {
                        Guid? id = null;
                        if (_entityGetIdByCode[propertyName])
                        {
                            id = await GetIdByCodeForImport(propertyName, value.ToString());
                        }
                        else
                        {
                            id = (Guid?)value;
                        }
                        property.SetValue(entity, id);
                    }
                }
                result.Add(entity);
            }

            return result;
            
            //    {
            //        Code = (string)row["Code"],
            //        Name = (string)row["Name"],
            //        Street = (string)row["Street"],
            //        Address = (string)row["Address"],
            //        Active = (bool)(row["Active"] ?? true),
            //        EffectiveDate = (DateTime)(row["EffectiveDate"] ?? DateTime.Now),
            //        GeoLevel0Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel0Code"]).Result,
            //        GeoLevel1Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel1Code"]).Result,
            //        GeoLevel2Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel2Code"]).Result,
            //        GeoLevel3Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel3Code"]).Result,
            //        GeoLevel4Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel4Code"]).Result,
            //    };
        }

        private static DataTable ReadExcelToDatatable(ExcelWorksheet sheetTable, ExcelWorksheet sheetData)
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

                var getIdByCodeSheetValue = sheetTable.Cells[i, 4].Value;
                if (getIdByCodeSheetValue != null)
                {
                    bool getIdByCode = bool.Parse(getIdByCodeSheetValue.ToString().Trim());
                    _entityGetIdByCode.Add(col.ColumnName, getIdByCode);
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

        protected virtual Task<Guid?> GetIdByCodeForImport(string propertyName, string code)
        {
            throw new NotImplementedException();
        }
    }
}
