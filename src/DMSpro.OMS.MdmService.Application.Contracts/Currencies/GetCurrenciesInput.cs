using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Currencies
{
    public class GetCurrenciesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public GetCurrenciesInput()
        {

        }
    }
}