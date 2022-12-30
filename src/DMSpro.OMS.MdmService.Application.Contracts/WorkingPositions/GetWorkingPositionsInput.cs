using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class GetWorkingPositionsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public GetWorkingPositionsInput()
        {

        }
    }
}