using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.SSHistoryInZones;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.SSHistoryInZones
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SSHistoryInZone")]
    [Route("api/mdm-service/s-sHistory-in-zones")]
    public class SSHistoryInZoneController : AbpController, ISSHistoryInZonesAppService
    {
        private readonly ISSHistoryInZonesAppService _sSHistoryInZonesAppService;

        public SSHistoryInZoneController(ISSHistoryInZonesAppService sSHistoryInZonesAppService)
        {
            _sSHistoryInZonesAppService = sSHistoryInZonesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<SSHistoryInZoneWithNavigationPropertiesDto>> GetListAsync(GetSSHistoryInZonesInput input)
        {
            return _sSHistoryInZonesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<SSHistoryInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _sSHistoryInZonesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _sSHistoryInZonesAppService.GetListDevextremesAsync(inputDev);
        }


        [HttpGet]
        [Route("{id}")]
        public virtual Task<SSHistoryInZoneDto> GetAsync(Guid id)
        {
            return _sSHistoryInZonesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _sSHistoryInZonesAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpGet]
        [Route("employee-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input)
        {
            return _sSHistoryInZonesAppService.GetEmployeeProfileLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<SSHistoryInZoneDto> CreateAsync(SSHistoryInZoneCreateDto input)
        {
            return _sSHistoryInZonesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SSHistoryInZoneDto> UpdateAsync(Guid id, SSHistoryInZoneUpdateDto input)
        {
            return _sSHistoryInZonesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _sSHistoryInZonesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(SSHistoryInZoneExcelDownloadDto input)
        {
            return _sSHistoryInZonesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _sSHistoryInZonesAppService.GetDownloadTokenAsync();
        }
    }
}