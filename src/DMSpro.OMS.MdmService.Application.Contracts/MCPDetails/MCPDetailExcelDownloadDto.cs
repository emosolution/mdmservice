using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public class MCPDetailExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public int? DistanceMin { get; set; }
        public int? DistanceMax { get; set; }
        public int? VisitOrderMin { get; set; }
        public int? VisitOrderMax { get; set; }
        public bool? Monday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }
        public bool? Saturday { get; set; }
        public bool? Sunday { get; set; }
        public bool? Week1 { get; set; }
        public bool? Week2 { get; set; }
        public bool? Week3 { get; set; }
        public bool? Week4 { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? MCPHeaderId { get; set; }

        public MCPDetailExcelDownloadDto()
        {

        }
    }
}