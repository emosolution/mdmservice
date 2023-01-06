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
                id: Guid.Parse("67fe0da1-b355-4812-ac13-ef2b20992acc"),
                code: "a81b3cb09c174551a7d8",
                name: "45ea898989144d088143a827498b16799a480162ca9343d28024a6540e2fe63a67dbfaa",
                effectiveDate: new DateTime(2013, 11, 16),
                endDate: new DateTime(2009, 5, 6),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                itemGroupId: null
            ));

            await _mCPHeaderRepository.InsertAsync(new MCPHeader
            (
                id: Guid.Parse("eed14627-87a5-4c56-b5eb-569558c73523"),
                code: "887957c52d62492789ce",
                name: "d45f11acb702458ca4d2ebbcbc7db8175fd15fea9ee840db8e2ddc5f91bffc968019c2",
                effectiveDate: new DateTime(2021, 1, 19),
                endDate: new DateTime(2013, 6, 15),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                itemGroupId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}