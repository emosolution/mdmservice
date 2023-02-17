using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using System;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.SalesOrgHeaders
{
	public partial class SalesOrgHeaderController
	{
		[HttpGet]
		[Route("GetListDevextremes")]
		public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _salesOrgHeadersAppService.GetListDevextremesAsync(inputDev);
		}

		[HttpPost]
		[Route("update-from-excel")]
		public async Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
		{
			try
            {
                return await _salesOrgHeadersAppService.UpdateFromExcelAsync(file);
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
		public async Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
        {
            try
            {
                return await _salesOrgHeadersAppService.InsertFromExcelAsync(file);
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
		
		[HttpGet]
        [Route("get-excel-template")]
        public virtual async Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
        {
            try
            {
                return await _salesOrgHeadersAppService.GenerateExcelTemplatesAsync();
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