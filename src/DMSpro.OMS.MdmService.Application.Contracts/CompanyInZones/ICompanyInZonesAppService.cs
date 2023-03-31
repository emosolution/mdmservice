using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public partial interface ICompanyInZonesAppService : IApplicationService
    {
        Task<CompanyInZoneDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<CompanyInZoneDto> CreateAsync(CompanyInZoneCreateDto input);
        Task<CompanyInZoneDto> UpdateAsync(Guid id, CompanyInZoneUpdateDto input);
    }
}