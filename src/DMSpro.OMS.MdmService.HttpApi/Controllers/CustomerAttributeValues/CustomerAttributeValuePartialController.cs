using System;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Content;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public partial class CustomerAttributeValueController
    {
        [HttpGet]
        [Route("GetListDevextremes")]
        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            try
            {
                return await _customerAttributeValuesAppService.GetListDevextremesAsync(inputDev);
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
        [Route("update-from-excel")]
        public async Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
        {
            try
            {
                return await _customerAttributeValuesAppService.UpdateFromExcelAsync(file);
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
                return await _customerAttributeValuesAppService.InsertFromExcelAsync(file);
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
                return await _customerAttributeValuesAppService.GenerateExcelTemplatesAsync();
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