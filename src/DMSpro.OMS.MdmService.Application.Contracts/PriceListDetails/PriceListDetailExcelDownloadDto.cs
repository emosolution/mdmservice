using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public int? PriceMin { get; set; }
        public int? PriceMax { get; set; }
        public int? BasedOnPriceMin { get; set; }
        public int? BasedOnPriceMax { get; set; }
        public string Description { get; set; }
        public Guid? PriceListId { get; set; }
        public Guid? ItemMasterId { get; set; }
        public Guid? UOMId { get; set; }

        public PriceListDetailExcelDownloadDto()
        {

        }
    }
}