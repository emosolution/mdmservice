using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public interface ICustomerGroupByAttsAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerGroupByAttWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByAttsInput input);

        Task<CustomerGroupByAttWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<CustomerGroupByAttDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCusAttributeValueLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerGroupByAttDto> CreateAsync(CustomerGroupByAttCreateDto input);

        Task<CustomerGroupByAttDto> UpdateAsync(Guid id, CustomerGroupByAttUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByAttExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}