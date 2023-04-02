using System;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.VisitPlans;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace DMSpro.OMS.MdmService.Controllers.VisitPlans
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("VisitPlan")]
    [Route("api/mdm-service/visit-plans")]
    public partial class VisitPlanController : AbpController, IVisitPlansAppService
    {
        private readonly IVisitPlansAppService _visitPlansAppService;

        public VisitPlanController(IVisitPlansAppService visitPlansAppService)
        {
            _visitPlansAppService = visitPlansAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VisitPlanDto> GetAsync(Guid id)
        {
            return _visitPlansAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<VisitPlanDto> CreateAsync(VisitPlanCreateDto input)
        {
            return _visitPlansAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VisitPlanDto> UpdateAsync(Guid id, VisitPlanUpdateDto input)
        {
            return _visitPlansAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _visitPlansAppService.DeleteAsync(id);
        }
    }
}