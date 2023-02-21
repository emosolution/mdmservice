using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public partial interface IEmployeeImagesAppService : IApplicationService
    {
        Task<PagedResultDto<EmployeeImageWithNavigationPropertiesDto>> GetListAsync(GetEmployeeImagesInput input);

        Task<EmployeeImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<EmployeeImageDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input);

        Task<EmployeeImageDto> CreateAsync(EmployeeImageCreateDto input);

        Task<EmployeeImageDto> UpdateAsync(Guid id, EmployeeImageUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeImageExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}