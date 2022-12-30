using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public class GetGeoMastersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string ERPCode { get; set; }
        public string Name { get; set; }
        public int? LevelMin { get; set; }
        public int? LevelMax { get; set; }
        public Guid? ParentId { get; set; }

        public GetGeoMastersInput()
        {

        }
    }
}