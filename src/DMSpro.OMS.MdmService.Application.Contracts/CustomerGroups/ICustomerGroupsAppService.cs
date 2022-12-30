using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public interface ICustomerGroupsAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerGroupDto>> GetListAsync(GetCustomerGroupsInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<CustomerGroupDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerGroupDto> CreateAsync(CustomerGroupCreateDto input);

        Task<CustomerGroupDto> UpdateAsync(Guid id, CustomerGroupUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}