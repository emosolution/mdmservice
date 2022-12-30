using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public int? PriceBeforeUpdateMin { get; set; }
        public int? PriceBeforeUpdateMax { get; set; }
        public int? NewPriceMin { get; set; }
        public int? NewPriceMax { get; set; }
        public DateTime? UpdatedDateMin { get; set; }
        public DateTime? UpdatedDateMax { get; set; }
        public Guid? PriceUpdateId { get; set; }
        public Guid? PriceListDetailId { get; set; }

        public PriceUpdateDetailExcelDownloadDto()
        {

        }
    }
}