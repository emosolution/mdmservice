using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.UOMGroupDetails
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("UOMGroupDetail")]
    [Route("api/mdm-service/u-oMGroup-details")]
    public partial class UOMGroupDetailController : AbpController, IUOMGroupDetailsAppService
    {
        private readonly IUOMGroupDetailsAppService _uOMGroupDetailsAppService;

        public UOMGroupDetailController(IUOMGroupDetailsAppService uOMGroupDetailsAppService)
        {
            _uOMGroupDetailsAppService = uOMGroupDetailsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<UOMGroupDetailWithNavigationPropertiesDto>> GetListAsync(GetUOMGroupDetailsInput input)
        {
            return _uOMGroupDetailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<UOMGroupDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _uOMGroupDetailsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<UOMGroupDetailDto> GetAsync(Guid id)
        {
            return _uOMGroupDetailsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("u-oMGroup-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupLookupAsync(LookupRequestDto input)
        {
            return _uOMGroupDetailsAppService.GetUOMGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("u-oM-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input)
        {
            return _uOMGroupDetailsAppService.GetUOMLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<UOMGroupDetailDto> CreateAsync(UOMGroupDetailCreateDto input)
        {
            return _uOMGroupDetailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<UOMGroupDetailDto> UpdateAsync(Guid id, UOMGroupDetailUpdateDto input)
        {
            return _uOMGroupDetailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _uOMGroupDetailsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMGroupDetailExcelDownloadDto input)
        {
            return _uOMGroupDetailsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _uOMGroupDetailsAppService.GetDownloadTokenAsync();
        }
    }
}