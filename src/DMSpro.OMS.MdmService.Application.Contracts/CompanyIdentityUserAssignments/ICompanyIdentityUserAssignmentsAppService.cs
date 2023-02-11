using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Partial;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial interface ICompanyIdentityUserAssignmentsAppService : IApplicationService
    {
        Task<PagedResultDto<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>> GetListAsync(GetCompanyIdentityUserAssignmentsInput input);

        Task<CompanyIdentityUserAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CompanyIdentityUserAssignmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CompanyIdentityUserAssignmentDto> CreateAsync(CompanyIdentityUserAssignmentCreateDto input);

        Task<CompanyIdentityUserAssignmentDto> UpdateAsync(Guid id, CompanyIdentityUserAssignmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyIdentityUserAssignmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);
        Task<int> InsertFromExcelAsync(IFormFile file);
        Task<int> UpdateFromExcelAsync(IFormFile file);
    }
}