using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public decimal? BasedOnPriceMin { get; set; }
        public decimal? BasedOnPriceMax { get; set; }
        public string Description { get; set; }
        public Guid? PriceListId { get; set; }
        public Guid? UOMId { get; set; }
        public Guid? ItemId { get; set; }

        public PriceListDetailExcelDownloadDto()
        {

        }
    }
}