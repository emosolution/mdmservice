using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public class GetSystemDatasInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string ValueCode { get; set; }
        public string ValueName { get; set; }

        public GetSystemDatasInput()
        {

        }
    }
}