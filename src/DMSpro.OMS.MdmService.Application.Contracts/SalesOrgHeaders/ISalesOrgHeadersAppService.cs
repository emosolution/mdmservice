using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public partial interface ISalesOrgHeadersAppService : IApplicationService
    {
        Task<SalesOrgHeaderDto> GetAsync(Guid id);

        Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input);

        Task<SalesOrgHeaderDto> ReleaseAsync(Guid id);

        Task<SalesOrgHeaderDto> InactiveAsync(Guid id);
    }
}