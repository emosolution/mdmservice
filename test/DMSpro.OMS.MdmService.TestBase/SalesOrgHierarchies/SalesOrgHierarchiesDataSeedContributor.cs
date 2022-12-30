using DMSpro.OMS.MdmService.SalesOrgHeaders;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchiesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHeadersDataSeedContributor _salesOrgHeadersDataSeedContributor;

        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        public SalesOrgHierarchiesDataSeedContributor(ISalesOrgHierarchyRepository salesOrgHierarchyRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHeadersDataSeedContributor salesOrgHeadersDataSeedContributor, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor)
        {
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHeadersDataSeedContributor = salesOrgHeadersDataSeedContributor; _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHeadersDataSeedContributor.SeedAsync(context);
            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);

            await _salesOrgHierarchyRepository.InsertAsync(new SalesOrgHierarchy
            (
                id: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                code: "afeb4dce5b37425286b0",
                name: "77bb8ffbb5c74b8daf7b65d446f064046d9fe0e0047f46fda67026477271f7e5d7b9dfd95be546bb9af",
                level: 7,
                isRoute: true,
                isSellingZone: true,
                hierarchyCode: "6398f174ed204fdf979e19e2fb038ea36426410745",
                active: true,
                salesOrgHeaderId: Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"),
                parentId: null
            ));

            await _salesOrgHierarchyRepository.InsertAsync(new SalesOrgHierarchy
            (
                id: Guid.Parse("272d040f-b303-4617-b1cd-dc72b98aebbd"),
                code: "1e28dc6c259d4134932c",
                name: "66c0eb441a9d4418a9db5875078f6960de97fb57b04a4cbcaa23451a445104ce10f7a693f7b945048e",
                level: 2,
                isRoute: true,
                isSellingZone: true,
                hierarchyCode: "a43863e13a5f40aa8a1e91ef5179031bb5249513479e49a8ae555ff",
                active: true,
                salesOrgHeaderId: Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"),
                parentId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}