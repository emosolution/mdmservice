using DevExtreme.AspNet.Data.ResponseModel;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public partial interface ICustomerAttributesAppService : IApplicationService
    {
        Task<CustomerAttributeDto> GetAsync(Guid id);

        Task<LoadResult> DeleteAsync();

        Task<LoadResult> CreateAsync(CustomerAttributeCreateDto input);

        Task<LoadResult> UpdateAsync(Guid id, CustomerAttributeUpdateDto input);
    }
}