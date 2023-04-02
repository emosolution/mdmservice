using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public partial interface ISalesOrgHeadersAppService : IApplicationService
    {
        Task<SalesOrgHeaderDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input);

        Task<SalesOrgHeaderDto> UpdateAsync(Guid id, SalesOrgHeaderUpdateDto input);
    }
}