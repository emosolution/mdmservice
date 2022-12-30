using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMs;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailWithNavigationPropertiesDto
    {
        public UOMGroupDetailDto UOMGroupDetail { get; set; }

        public UOMGroupDto UOMGroup { get; set; }
        public UOMDto AltUOM { get; set; }
        public UOMDto BaseUOM { get; set; }

    }
}