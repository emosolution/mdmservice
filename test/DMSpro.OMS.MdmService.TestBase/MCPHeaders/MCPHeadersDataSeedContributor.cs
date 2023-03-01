using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.MCPHeaders;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeadersDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IMCPHeaderRepository _mCPHeaderRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        private readonly ItemGroupsDataSeedContributor _itemGroupsDataSeedContributor;

        public MCPHeadersDataSeedContributor(IMCPHeaderRepository mCPHeaderRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, CompaniesDataSeedContributor companiesDataSeedContributor, ItemGroupsDataSeedContributor itemGroupsDataSeedContributor)
        {
            _mCPHeaderRepository = mCPHeaderRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor; _companiesDataSeedContributor = companiesDataSeedContributor; _itemGroupsDataSeedContributor = itemGroupsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);
            await _companiesDataSeedContributor.SeedAsync(context);
            await _itemGroupsDataSeedContributor.SeedAsync(context);

            await _mCPHeaderRepository.InsertAsync(new MCPHeader
            (
                id: Guid.Parse("a4ad8966-af84-450c-b00a-57297115617a"),
                code: "2df2bee569f844bc92ce",
                name: "a58b4c9ff1134803824ed6baccb0746578f30fc8dded4ed1830ce6f0755ed8d1c316ccff74474528832dfce6d13031fa5aff87cbacd9444cb8d537011ba84e90a0389d8d8dda42ed99d7b59f0c0874ce7aff65c70f03412482513957d5c05e78c613d2a709c8463db788d670e56e0f20a6d3daaad11e4a3583ff61e319cd840",
                effectiveDate: new DateTime(2016, 6, 7),
                endDate: new DateTime(2017, 9, 6),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                itemGroupId: null
            ));

            await _mCPHeaderRepository.InsertAsync(new MCPHeader
            (
                id: Guid.Parse("af2c85a3-9be9-408f-99d2-90789e7bee0a"),
                code: "5a7e3a6b33b945fb9ede",
                name: "e6590e92b9b3411c9e4ded3cc50dede46ecef02fe11440c3984d9a3ca26b2b775a43ef724ccb48a99c7b818cf82808c343e6eb28de444659b4b4a26d06fd417d5a7277203aef461b89161317ed4ddd9ce697ae712cef445690a8f2c649134e2292b4a8f7038a4e33aaed864bd0d9efb58cd07d5b2c1941e0ab6499cf6c224af",
                effectiveDate: new DateTime(2002, 8, 23),
                endDate: new DateTime(2018, 4, 4),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                itemGroupId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}