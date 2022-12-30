using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public interface IPricelistAssignmentsAppService : IApplicationService
    {
        Task<PagedResultDto<PricelistAssignmentWithNavigationPropertiesDto>> GetListAsync(GetPricelistAssignmentsInput input);

        Task<PricelistAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<PricelistAssignmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<PricelistAssignmentDto> CreateAsync(PricelistAssignmentCreateDto input);

        Task<PricelistAssignmentDto> UpdateAsync(Guid id, PricelistAssignmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PricelistAssignmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}