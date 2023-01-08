using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.VisitPlans
{
    public partial class VisitPlanController
    {

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _visitPlansAppService.GetListDevextremesAsync(inputDev);
        }
    }
}