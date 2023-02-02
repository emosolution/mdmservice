using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.EmployeeInZones;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeInZones
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("EmployeeInZone")]
    [Route("api/mdm-service/employee-in-zones")]
    public partial class EmployeeInZoneController : AbpController, IEmployeeInZonesAppService
    {
        private readonly IEmployeeInZonesAppService _employeeInZonesAppService;

        public EmployeeInZoneController(IEmployeeInZonesAppService employeeInZonesAppService)
        {
            _employeeInZonesAppService = employeeInZonesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<EmployeeInZoneWithNavigationPropertiesDto>> GetListAsync(GetEmployeeInZonesInput input)
        {
            return _employeeInZonesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<EmployeeInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _employeeInZonesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeInZoneDto> GetAsync(Guid id)
        {
            return _employeeInZonesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _employeeInZonesAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpGet]
        [Route("employee-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input)
        {
            return _employeeInZonesAppService.GetEmployeeProfileLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<EmployeeInZoneDto> CreateAsync(EmployeeInZoneCreateDto input)
        {
            return _employeeInZonesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeInZoneDto> UpdateAsync(Guid id, EmployeeInZoneUpdateDto input)
        {
            return _employeeInZonesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _employeeInZonesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeInZoneExcelDownloadDto input)
        {
            return _employeeInZonesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _employeeInZonesAppService.GetDownloadTokenAsync();
        }
    }
}