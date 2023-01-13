using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public partial interface IVisitPlansAppService : IApplicationService
    {
        Task<PagedResultDto<VisitPlanWithNavigationPropertiesDto>> GetListAsync(GetVisitPlansInput input);

        Task<VisitPlanWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<VisitPlanDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetMCPDetailLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetItemGroupLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<VisitPlanDto> CreateAsync(VisitPlanCreateDto input);

        Task<VisitPlanDto> UpdateAsync(Guid id, VisitPlanUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VisitPlanExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}