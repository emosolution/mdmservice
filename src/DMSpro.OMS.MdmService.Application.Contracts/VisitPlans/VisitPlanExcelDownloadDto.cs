using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public DateTime? DateVisitMin { get; set; }
        public DateTime? DateVisitMax { get; set; }
        public int? DistanceMin { get; set; }
        public int? DistanceMax { get; set; }
        public int? VisitOrderMin { get; set; }
        public int? VisitOrderMax { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public int? WeekMin { get; set; }
        public int? WeekMax { get; set; }
        public int? MonthMin { get; set; }
        public int? MonthMax { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public Guid? MCPDetailId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? RouteId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? ItemGroupId { get; set; }

        public VisitPlanExcelDownloadDto()
        {

        }
    }
}