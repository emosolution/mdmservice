using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.WorkingPositions;

namespace DMSpro.OMS.MdmService.Controllers.WorkingPositions
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("WorkingPosition")]
    [Route("api/mdm-service/working-positions")]
    public partial class WorkingPositionController : AbpController, IWorkingPositionsAppService
    {
        private readonly IWorkingPositionsAppService _workingPositionsAppService;

        public WorkingPositionController(IWorkingPositionsAppService workingPositionsAppService)
        {
            _workingPositionsAppService = workingPositionsAppService;
        }


        [HttpGet]
        [Route("{id}")]
        public virtual Task<WorkingPositionDto> GetAsync(Guid id)
        {
            return _workingPositionsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<WorkingPositionDto> CreateAsync(WorkingPositionCreateDto input)
        {
            return _workingPositionsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<WorkingPositionDto> UpdateAsync(Guid id, WorkingPositionUpdateDto input)
        {
            return _workingPositionsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _workingPositionsAppService.DeleteAsync(id);
        }
    }
}