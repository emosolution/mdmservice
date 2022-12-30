using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImageExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Description { get; set; }
        public string url { get; set; }
        public bool? Active { get; set; }
        public bool? IsAvatar { get; set; }
        public Guid? EmployeeProfileId { get; set; }

        public EmployeeImageExcelDownloadDto()
        {

        }
    }
}