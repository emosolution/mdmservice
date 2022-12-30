using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Currencies
{
    public class CurrencyExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public CurrencyExcelDownloadDto()
        {

        }
    }
}