using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.CustomerImages
{
    public partial class CustomerImageController
    {
        [HttpGet]
        [Route("GetListDevextremes")]
        public virtual Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _customerImagesAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpPost]
        [Route("update-from-excel")]
        public virtual Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
        {
            return _customerImagesAppService.UpdateFromExcelAsync(file);
        }

        [HttpPost]
        [Route("insert-from-excel")]
        public virtual Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
        {
            return _customerImagesAppService.InsertFromExcelAsync(file);
        }

        [HttpGet]
        [Route("get-excel-template")]
        public virtual Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
        {
            return _customerImagesAppService.GenerateExcelTemplatesAsync();
        }
    }
}
