using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public class EmployeeInZoneExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public Guid? SalesOrgHierarchyId { get; set; }
        public Guid? EmployeeId { get; set; }

        public EmployeeInZoneExcelDownloadDto()
        {

        }
    }
}