using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Routes;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.Routes
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Route")]
    [Route("api/mdm-service/routes")]
    public partial class RouteController : AbpController, IRoutesAppService
    {
        private readonly IRoutesAppService _routesAppService;

        public RouteController(IRoutesAppService routesAppService)
        {
            _routesAppService = routesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<RouteWithNavigationPropertiesDto>> GetListAsync(GetRoutesInput input)
        {
            return _routesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<RouteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _routesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<RouteDto> GetAsync(Guid id)
        {
            return _routesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("system-data-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            return _routesAppService.GetSystemDataLookupAsync(input);
        }

        [HttpGet]
        [Route("item-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            return _routesAppService.GetItemGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _routesAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<RouteDto> CreateAsync(RouteCreateDto input)
        {
            return _routesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<RouteDto> UpdateAsync(Guid id, RouteUpdateDto input)
        {
            return _routesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _routesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(RouteExcelDownloadDto input)
        {
            return _routesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _routesAppService.GetDownloadTokenAsync();
        }
    }
}