using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Content;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Controllers.CustomerImages
{
    public partial class CustomerImageController
    {
        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _customerImagesAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpPost]
        [Route("update-from-excel")]
        public async Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
        {
            try
            {
                return await _customerImagesAppService.UpdateFromExcelAsync(file);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPost]
        [Route("insert-from-excel")]
        public async Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
        {
            try
            {
                return await _customerImagesAppService.InsertFromExcelAsync(file);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpGet]
        [Route("get-excel-template")]
        public virtual async Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
        {
            try
            {
                return await _customerImagesAppService.GenerateExcelTemplatesAsync();
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }
    }
}
