using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public partial interface ICustomerImagesAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerImageWithNavigationPropertiesDto>> GetListAsync(GetCustomerImagesInput input);

        Task<CustomerImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CustomerImageDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerImageExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}