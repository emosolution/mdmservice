using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.RouteAssignments;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.RouteAssignments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("RouteAssignment")]
    [Route("api/mdm-service/route-assignments")]
    public class RouteAssignmentController : AbpController, IRouteAssignmentsAppService
    {
        private readonly IRouteAssignmentsAppService _routeAssignmentsAppService;

        public RouteAssignmentController(IRouteAssignmentsAppService routeAssignmentsAppService)
        {
            _routeAssignmentsAppService = routeAssignmentsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<RouteAssignmentWithNavigationPropertiesDto>> GetListAsync(GetRouteAssignmentsInput input)
        {
            return _routeAssignmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _routeAssignmentsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<RouteAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _routeAssignmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<RouteAssignmentDto> GetAsync(Guid id)
        {
            return _routeAssignmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _routeAssignmentsAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpGet]
        [Route("employee-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input)
        {
            return _routeAssignmentsAppService.GetEmployeeProfileLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<RouteAssignmentDto> CreateAsync(RouteAssignmentCreateDto input)
        {
            return _routeAssignmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<RouteAssignmentDto> UpdateAsync(Guid id, RouteAssignmentUpdateDto input)
        {
            return _routeAssignmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _routeAssignmentsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(RouteAssignmentExcelDownloadDto input)
        {
            return _routeAssignmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _routeAssignmentsAppService.GetDownloadTokenAsync();
        }
    }
}