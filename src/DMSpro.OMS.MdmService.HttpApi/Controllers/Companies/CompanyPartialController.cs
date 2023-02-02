using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using System.Threading;

namespace DMSpro.OMS.MdmService.Controllers.Companies
{
	public partial class CompanyController
	{

		[HttpGet]
		[Route("GetListDevextremes")]
		public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _companiesAppService.GetListDevextremesAsync(inputDev);
		}

		[HttpPost]
		[Route("update-from-excel")]
		public Task<int> UpdateFromExcelAsync(IFormFile file)
		{
			return _companiesAppService.UpdateFromExcelAsync(file);
		}

		[HttpPost]
		[Route("insert-from-excel")]
		public Task<int> InsertFromExcelAsync(IFormFile file)
        {
            return _companiesAppService.InsertFromExcelAsync(file);
        }
	}
}