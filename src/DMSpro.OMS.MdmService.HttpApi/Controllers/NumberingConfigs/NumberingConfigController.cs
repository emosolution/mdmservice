using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.NumberingConfigs;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.NumberingConfigs
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("NumberingConfig")]
    [Route("api/mdm-service/numbering-configs")]
    public class NumberingConfigController : AbpController, INumberingConfigsAppService
    {
        private readonly INumberingConfigsAppService _numberingConfigsAppService;

        public NumberingConfigController(INumberingConfigsAppService numberingConfigsAppService)
        {
            _numberingConfigsAppService = numberingConfigsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<NumberingConfigWithNavigationPropertiesDto>> GetListAsync(GetNumberingConfigsInput input)
        {
            return _numberingConfigsAppService.GetListAsync(input);
        }

        [HttpGet]

        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _numberingConfigsAppService.GetListDevextremesAsync(inputDev);
        }


        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<NumberingConfigWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _numberingConfigsAppService.GetWithNavigationPropertiesAsync(id);
        }


        [HttpGet]
        [Route("{id}")]
        public virtual Task<NumberingConfigDto> GetAsync(Guid id)
        {
            return _numberingConfigsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _numberingConfigsAppService.GetCompanyLookupAsync(input);
        }

        [HttpGet]
        [Route("system-data-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            return _numberingConfigsAppService.GetSystemDataLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<NumberingConfigDto> CreateAsync(NumberingConfigCreateDto input)
        {
            return _numberingConfigsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<NumberingConfigDto> UpdateAsync(Guid id, NumberingConfigUpdateDto input)
        {
            return _numberingConfigsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _numberingConfigsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(NumberingConfigExcelDownloadDto input)
        {
            return _numberingConfigsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _numberingConfigsAppService.GetDownloadTokenAsync();
        }
    }
}