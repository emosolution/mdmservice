using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.EmployeeImages;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeImages
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("EmployeeImage")]
    [Route("api/mdm-service/employee-images")]
    public partial class EmployeeImageController : AbpController, IEmployeeImagesAppService
    {
        private readonly IEmployeeImagesAppService _employeeImagesAppService;

        public EmployeeImageController(IEmployeeImagesAppService employeeImagesAppService)
        {
            _employeeImagesAppService = employeeImagesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<EmployeeImageWithNavigationPropertiesDto>> GetListAsync(GetEmployeeImagesInput input)
        {
            return _employeeImagesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<EmployeeImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _employeeImagesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeImageDto> GetAsync(Guid id)
        {
            return _employeeImagesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("employee-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input)
        {
            return _employeeImagesAppService.GetEmployeeProfileLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<EmployeeImageDto> CreateAsync(EmployeeImageCreateDto input, IRemoteStreamContent file)
        {
            return _employeeImagesAppService.CreateAsync(input, file);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeImageDto> UpdateAsync(Guid id, EmployeeImageUpdateDto input, IRemoteStreamContent file)
        {
            return _employeeImagesAppService.UpdateAsync(id, input, file);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeImageExcelDownloadDto input)
        {
            return _employeeImagesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _employeeImagesAppService.GetDownloadTokenAsync();
        }
    }
}