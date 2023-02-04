using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using OfficeOpenXml;
using Volo.Abp;
using System.Data;
using Volo.Abp.Data;
using Microsoft.VisualBasic;

namespace DMSpro.OMS.MdmService.Companies
{
	public partial class CompaniesAppService
	{
		public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			var items = await _companyRepository.GetQueryableAsync();
			var base_dataloadoption = new DataSourceLoadOptionsBase();
			DataLoadParser.Parse(base_dataloadoption,inputDev);
			LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
			results.data = ObjectMapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(results.data.Cast<Company>());
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

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                //|| !Path.GetExtension(file.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase)) //not support file extention
            {
                throw new BusinessException(message: L["Error:ImportFileNotSupported"], code: "0");
            }

            var companies = new List<Company>();

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

                        //var table = new DataTable();
                        //var data = new DataTable();

                        //table = ReadExcelToDataTable(sheetTable, true);

                        //var rowCount = table.Rows.Count;
                        //var columnCount = data.Columns.Count;

                        //if (rowCount != columnCount) throw new BusinessException(message: L["Error:ImportFileNotSupported"], code: "0");

                        var data = ReadExcelToDatatable(sheetTable, sheetData);

                        companies = data.AsEnumerable().Select(row =>
                        {
                            return new Company
                            {
                                Code = (string)row["Code"],
                                Name = (string)row["Name"],
                                Street = (string)row["Street"],
                                Address = (string)row["Address"],
                                Active = (bool)(row["Active"] ?? true),
                                EffectiveDate = (DateTime)(row["EffectiveDate"] ?? DateTime.Now),
                                GeoLevel0Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel0Code"]).Result,
                                GeoLevel1Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel1Code"]).Result,
                                GeoLevel2Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel2Code"]).Result,
                                GeoLevel3Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel3Code"]).Result,
                                //GeoLevel4Id = _geoMasterRepository.GetIdByCodeAsync((string)row["GeoLevel4Code"]).Result,
                            };

                        }).ToList();
                    }
                }
            }

            //await _companyRepository.InsertManyAsync(companies);

            return companies.Count;
        }

        private DataTable ReadExcelAndCreateDataTable(ExcelWorksheet worksheet)
        {
            DataTable dt = new DataTable();

            //int totalColumns = worksheet.Dimension.End.Column;
            //int totalRows = worksheet.Dimension.End.Row;

            //for (int i = 1; i <= totalColumns; i++)
            //{
            //    dataTable.Columns.Add();
            //}

            //for (int i = 1; i <= totalRows; i++)
            //{
            //    DataRow dr = dataTable.NewRow();
            //    for (int j = 1; j <= totalColumns; j++)
            //    {
            //        dr[j - 1] = worksheet.Cells[i, j].Value;
            //    }
            //    dataTable.Rows.Add(dr);
            //}

            //return dataTable;

            var rowCount = worksheet.Dimension.End.Row;
            var colCount = worksheet.Dimension.End.Column;

            //create a list to hold the column names
            var columnNames = new List<string>();

            //needed to keep track of empty column headers
            int currentColumn = 1;

            //loop all columns in the sheet and add them to the datatable
            foreach (var cell in worksheet.Cells[1, 1, 1, colCount])
            {
                string columnName = cell.Text.Trim();

                //check if the previous header was empty and add it if it was
                if (cell.Start.Column != currentColumn)
                {
                    columnNames.Add("Header_" + currentColumn);
                    dt.Columns.Add("Header_" + currentColumn);
                    currentColumn++;
                }

                //add the column name to the list to count the duplicates
                columnNames.Add(columnName);

                //count the duplicate column names and make them unique to avoid the exception
                //A column named 'Name' already belongs to this DataTable
                int occurrences = columnNames.Count(x => x.Equals(columnName));
                if (occurrences > 1)
                {
                    columnName = columnName + "_" + occurrences;
                }

                //add the column to the datatable
                dt.Columns.Add(columnName);

                currentColumn++;
            }

            //start adding the contents of the excel file to the companies
            for (int i = 2; i <= rowCount; i++)
            {
                var row = worksheet.Cells[i, 1, i, colCount];
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

        private DataTable ReadExcelToDatatable(ExcelWorksheet sheetTable, ExcelWorksheet sheetData)
        {
            DataTable dt = new DataTable();
            int colCount = sheetTable.Dimension.End.Column;
            int rowCount = sheetTable.Dimension.End.Row;

            for (int i = 2; i <= rowCount; i++)
            {
                var col = new DataColumn();
                col.ColumnName = sheetTable.Cells[i, 1].Value.ToString().Trim();

                var allowNull = bool.Parse(sheetTable.Cells[i, 3].Value.ToString().Trim());
                col.AllowDBNull = allowNull;

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
    }
}