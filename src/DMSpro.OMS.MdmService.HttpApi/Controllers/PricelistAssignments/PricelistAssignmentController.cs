using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.PricelistAssignments;

namespace DMSpro.OMS.MdmService.Controllers.PricelistAssignments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("PricelistAssignment")]
    [Route("api/mdm-service/pricelist-assignments")]
    public partial class PricelistAssignmentController : AbpController, IPricelistAssignmentsAppService
    {
        private readonly IPricelistAssignmentsAppService _pricelistAssignmentsAppService;

        public PricelistAssignmentController(IPricelistAssignmentsAppService pricelistAssignmentsAppService)
        {
            _pricelistAssignmentsAppService = pricelistAssignmentsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PricelistAssignmentDto> GetAsync(Guid id)
        {
            return _pricelistAssignmentsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<PricelistAssignmentDto> CreateAsync(PricelistAssignmentCreateDto input)
        {
            return _pricelistAssignmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PricelistAssignmentDto> UpdateAsync(Guid id, PricelistAssignmentUpdateDto input)
        {
            return _pricelistAssignmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _pricelistAssignmentsAppService.DeleteAsync(id);
        }
    }
}