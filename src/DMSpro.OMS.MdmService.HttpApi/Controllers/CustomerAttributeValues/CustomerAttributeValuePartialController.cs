using System.Threading.Tasks;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.CustomerAttributeValues
{
    public partial class CustomerAttributeValueController
    {
        [HttpGet]
        [Route("GetListDevextremes")]
        public virtual Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _customerAttributeValuesAppService.GetListDevextremesAsync(inputDev);
            
        }

        [HttpPost]
        [Route("update-from-excel")]
        public virtual Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
        {
            return _customerAttributeValuesAppService.UpdateFromExcelAsync(file);
        }

        [HttpPost]
        [Route("insert-from-excel")]
        public virtual Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
        {
            return _customerAttributeValuesAppService.InsertFromExcelAsync(file);
        }

        [HttpGet]
        [Route("get-excel-template")]
        public virtual Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
        {
            return _customerAttributeValuesAppService.GenerateExcelTemplatesAsync();
        }
    }
}