using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;

namespace DMSpro.OMS.MdmService.Controllers.SalesOrgEmpAssignments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SalesOrgEmpAssignment")]
    [Route("api/mdm-service/sales-org-emp-assignments")]
    public partial class SalesOrgEmpAssignmentController : AbpController, ISalesOrgEmpAssignmentsAppService
    {
        private readonly ISalesOrgEmpAssignmentsAppService _salesOrgEmpAssignmentsAppService;

        public SalesOrgEmpAssignmentController(ISalesOrgEmpAssignmentsAppService salesOrgEmpAssignmentsAppService)
        {
            _salesOrgEmpAssignmentsAppService = salesOrgEmpAssignmentsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SalesOrgEmpAssignmentDto> GetAsync(Guid id)
        {
            return _salesOrgEmpAssignmentsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<SalesOrgEmpAssignmentDto> CreateAsync(SalesOrgEmpAssignmentCreateDto input)
        {
            return _salesOrgEmpAssignmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SalesOrgEmpAssignmentDto> UpdateAsync(Guid id, SalesOrgEmpAssignmentUpdateDto input)
        {
            return _salesOrgEmpAssignmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _salesOrgEmpAssignmentsAppService.DeleteAsync(id);
        }
    }
}