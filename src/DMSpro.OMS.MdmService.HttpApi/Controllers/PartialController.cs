using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace DMSpro.OMS.MdmService.Controllers
{
    public class PartialController<TAppService> : AbpController, IPartialsAppservice
        where TAppService : class, IPartialsAppservice
    {
        private readonly IPartialsAppservice _appService;

        public PartialController(IPartialsAppservice appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _appService.GetListDevextremesAsync(inputDev);
        }

        [HttpPost]
        [Route("update-from-excel")]
        public Task<int> UpdateFromExcelAsync(IFormFile file)
        {
            return _appService.UpdateFromExcelAsync(file);
        }

        [HttpPost]
        [Route("insert-from-excel")]
        public Task<int> InsertFromExcelAsync(IFormFile file)
        {
            return _appService.InsertFromExcelAsync(file);
        }
    }
}
