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
                throw new BusinessException(message: L["FormFile is empty"]);
            }

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)) //not support file extention
            {
                throw new BusinessException(message: L["Not support file extention"]);
            }

            //var postedFile = HttpContext.Request.Form.Files[0];

            var companies = new List<Company>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        companies.Add(new Company
                        {
                            Code = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            Street = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Address = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            GeoLevel0Id = await _geoMasterRepository.GetIdByCode(worksheet.Cells[row, 20].Value.ToString().Trim()),
                            GeoLevel1Id = await _geoMasterRepository.GetIdByCode(worksheet.Cells[row, 21].Value.ToString().Trim()),
                            GeoLevel2Id = await _geoMasterRepository.GetIdByCode(worksheet.Cells[row, 22].Value.ToString().Trim()),
                            GeoLevel3Id = await _geoMasterRepository.GetIdByCode(worksheet.Cells[row, 23].Value.ToString().Trim()),
                            GeoLevel4Id = await _geoMasterRepository.GetIdByCode(worksheet.Cells[row, 24].Value.ToString().Trim()),
                        });
                    }
                }
            }

            await _companyRepository.InsertManyAsync(companies);

            return companies.Count;
        }
    }
}