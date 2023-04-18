using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Content;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;

namespace DMSpro.OMS.MdmService.Controllers.NumberingConfigDetails
{
    public partial class NumberingConfigDetailController
    {
        [HttpGet]
        [Route("GetListDevextremes")]
        public virtual Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _numberingConfigDetailsAppService.GetListDevextremesAsync(inputDev);
            
        }

        [HttpPost]
        [Route("update-from-excel")]
        public virtual Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
        {
            return _numberingConfigDetailsAppService.UpdateFromExcelAsync(file);
        }

        [HttpPost]
        [Route("insert-from-excel")]
        public virtual Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
        {
            return _numberingConfigDetailsAppService.InsertFromExcelAsync(file);
        }

        [HttpGet]
        [Route("get-excel-template")]
        public virtual Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
        {
            return _numberingConfigDetailsAppService.GenerateExcelTemplatesAsync();
        }
    }
}