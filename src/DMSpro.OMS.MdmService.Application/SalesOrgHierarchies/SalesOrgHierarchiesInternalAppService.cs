using DMSpro.OMS.MdmService.Vendors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchiesInternalAppService: ApplicationService, ISalesOrgHierarchiesInternalAppService
    {
        private readonly ISalesOrgHierarchyRepository _repository;

        public SalesOrgHierarchiesInternalAppService(ISalesOrgHierarchyRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<SalesOrgHierarchyWithTenantDto> GetWithTenantIdAsynce(Guid id)
        {
            try
            {
                SalesOrgHierarchy result = await _repository.GetAsync(id);
                return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyWithTenantDto>(result);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}
