using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Currencies;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.Currencies
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Currency")]
    [Route("api/mdm-service/currencies")]
    public partial class CurrencyController : AbpController, ICurrenciesAppService
    {
        private readonly ICurrenciesAppService _currenciesAppService;

        public CurrencyController(ICurrenciesAppService currenciesAppService)
        {
            _currenciesAppService = currenciesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CurrencyDto>> GetListAsync(GetCurrenciesInput input)
        {
            return _currenciesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CurrencyDto> GetAsync(Guid id)
        {
            return _currenciesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CurrencyDto> CreateAsync(CurrencyCreateDto input)
        {
            return _currenciesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CurrencyDto> UpdateAsync(Guid id, CurrencyUpdateDto input)
        {
            return _currenciesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _currenciesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CurrencyExcelDownloadDto input)
        {
            return _currenciesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _currenciesAppService.GetDownloadTokenAsync();
        }
    }
}