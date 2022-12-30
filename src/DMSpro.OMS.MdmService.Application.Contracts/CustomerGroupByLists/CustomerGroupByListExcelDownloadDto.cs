using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByListExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public bool? Active { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public Guid? CustomerId { get; set; }

        public CustomerGroupByListExcelDownloadDto()
        {

        }
    }
}