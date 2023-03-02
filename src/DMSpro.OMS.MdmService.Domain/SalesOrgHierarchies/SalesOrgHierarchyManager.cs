using Volo.Abp.Domain.Services;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public partial class SalesOrgHierarchyManager : DomainService
    {
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public SalesOrgHierarchyManager(ISalesOrgHierarchyRepository salesOrgHierarchyRepository)
        {
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
        }
    }
}