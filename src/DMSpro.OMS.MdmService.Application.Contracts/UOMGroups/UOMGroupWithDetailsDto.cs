using DMSpro.OMS.MdmService.UOMGroupDetails;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    public class UOMGroupWithDetailsDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<UOMGroupDetailDto> Details { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
