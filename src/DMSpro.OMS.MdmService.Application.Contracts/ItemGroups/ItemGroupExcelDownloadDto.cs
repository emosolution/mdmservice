using DMSpro.OMS.MdmService.ItemGroups;
using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GroupType? Type { get; set; }
        public GroupStatus? Status { get; set; }

        public ItemGroupExcelDownloadDto()
        {

        }
    }
}