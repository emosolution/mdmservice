using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System.Collections.Generic;
using DMSpro.OMS.MdmService.ItemGroups;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
	[Authorize(MdmServicePermissions.CompanyInZones.Default)]
    public partial class CompanyInZonesAppService : PartialAppService<CompanyInZone, CompanyInZoneWithDetailsDto, ICompanyInZoneRepository>, 
        ICompanyInZonesAppService
    {
        private readonly ICompanyInZoneRepository _companyInZoneRepository;
        private readonly CompanyInZoneManager _companyInZoneManager;

        private readonly ICompanyRepository _companyRepository;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly IItemGroupRepository _itemGroupRepository;

        public CompanyInZonesAppService(ICurrentTenant currentTenant,
            ICompanyInZoneRepository repository,
            CompanyInZoneManager manager,
            IConfiguration settingProvider,
            ICompanyRepository companyRepository,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            IItemGroupRepository itemGroupRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.CompanyInZones.Default)
        {
            _companyInZoneRepository = repository;
            _companyInZoneManager = manager;

            _companyRepository = companyRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _itemGroupRepository = itemGroupRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyInZoneRepository", _companyInZoneRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupRepository", _itemGroupRepository));
        }

		
    }
}