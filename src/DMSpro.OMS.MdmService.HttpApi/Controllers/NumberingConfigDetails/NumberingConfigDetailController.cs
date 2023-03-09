using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.NumberingConfigDetails
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("NumberingConfigDetail")]
    [Route("api/mdm-service/numbering-config-details")]
    public partial class NumberingConfigDetailController : AbpController, INumberingConfigDetailsAppService
    {
        private readonly INumberingConfigDetailsAppService _numberingConfigDetailsAppService;

        public NumberingConfigDetailController(INumberingConfigDetailsAppService numberingConfigDetailsAppService)
        {
            _numberingConfigDetailsAppService = numberingConfigDetailsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<NumberingConfigDetailWithNavigationPropertiesDto>> GetListAsync(GetNumberingConfigDetailsInput input)
        {
            return _numberingConfigDetailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<NumberingConfigDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _numberingConfigDetailsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<NumberingConfigDetailDto> GetAsync(Guid id)
        {
            return _numberingConfigDetailsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("numbering-config-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetNumberingConfigLookupAsync(LookupRequestDto input)
        {
            return _numberingConfigDetailsAppService.GetNumberingConfigLookupAsync(input);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _numberingConfigDetailsAppService.GetCompanyLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<NumberingConfigDetailDto> CreateAsync(NumberingConfigDetailCreateDto input)
        {
            return _numberingConfigDetailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<NumberingConfigDetailDto> UpdateAsync(Guid id, NumberingConfigDetailUpdateDto input)
        {
            return _numberingConfigDetailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _numberingConfigDetailsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(NumberingConfigDetailExcelDownloadDto input)
        {
            return _numberingConfigDetailsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _numberingConfigDetailsAppService.GetDownloadTokenAsync();
        }
    }
}