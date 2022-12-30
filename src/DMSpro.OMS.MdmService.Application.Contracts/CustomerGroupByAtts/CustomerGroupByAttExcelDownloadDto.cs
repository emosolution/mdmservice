using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string ValueCode { get; set; }
        public string ValueName { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public Guid? CusAttributeValueId { get; set; }

        public CustomerGroupByAttExcelDownloadDto()
        {

        }
    }
}