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

        public MCPHeadersDataSeedContributor(IMCPHeaderRepository mCPHeaderRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, CompaniesDataSeedContributor companiesDataSeedContributor)
        {
            _mCPHeaderRepository = mCPHeaderRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor; _companiesDataSeedContributor = companiesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);
            await _companiesDataSeedContributor.SeedAsync(context);

            await _mCPHeaderRepository.InsertAsync(new MCPHeader
            (
                id: Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730"),
                code: "9cbbdc5f3557449c98df",
                name: "6e21c9cc5e574113b1",
                effectiveDate: new DateTime(2015, 6, 14),
                endDate: new DateTime(2002, 11, 11),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            ));

            await _mCPHeaderRepository.InsertAsync(new MCPHeader
            (
                id: Guid.Parse("00c6faa0-177a-4143-9ed3-1e27e13d4916"),
                code: "75ddc503b6b347ddba91",
                name: "daf4b0b576c549e9b98a4108968000a5879567d8666749eb9170a47a43012517da080b",
                effectiveDate: new DateTime(2020, 6, 26),
                endDate: new DateTime(2013, 5, 11),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}