using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using System;

namespace DMSpro.OMS.MdmService.Controllers.CompanyInZones
{
	public partial class CompanyInZoneController
	{

		[HttpGet]
		[Route("GetListDevextremes")]
		public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _companyInZonesAppService.GetListDevextremesAsync(inputDev);
		}

        [HttpGet]
		[Route("GetListDevextremeWithNavigation")]
		public Task<LoadResult> GetListDevextremesWithNavigationAsync(DataLoadOptionDevextreme inputDev)
		{
			return _companyInZonesAppService.GetListDevextremesWithNavigationAsync(inputDev);
		}

		[HttpPost]
		[Route("update-from-excel")]
		public async Task<int> UpdateFromExcelAsync(IFormFile file)
		{
			try
            {
                return await _companyInZonesAppService.UpdateFromExcelAsync(file);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
		}

		[HttpPost]
		[Route("insert-from-excel")]
		public async Task<int> InsertFromExcelAsync(IFormFile file)
        {
            try
            {
                return await _companyInZonesAppService.InsertFromExcelAsync(file);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }
	}
}