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
                id: Guid.Parse("1d1f0f7d-0901-4b2a-8c3a-72021f844286"),
                code: "dad81512ca3b4aef9747",
                name: "a1ba07741297426da34cf8cdb36f924186e5f3baa84148679ea5f823eb70b88ef78cb4cafe0d43b694ab1b301f1e595c76619e7bd22a489c8e32076bb1b8d9dab2af27f496ae4d56b64bf0601f3beef1475bf66d462b49cfb3807749dd9c983870511a59fed24d2e9e9e6ffadea37ff1ac4f66033f844153992d690f1a12e92",
                level: 0,
                isRoute: true,
                isSellingZone: true,
                hierarchyCode: "fdbd3502beeb4684bec412d7990debb8a856c3bb3b564aa2be03c99fe90b1d9d9517ffb3cab2422d982d64cfe623ff195a029ac2921e49e38410103189fc9f98b0addc5371184bc1b88c68577efcd8f05fc56062f5da4261b4cd834dbc2cabbd0cb25ad8385449febe6bbae0f20b041dd69291da7e2d4886a11f7832ef45c525dc022ff4b4fd457eadb7c2b6017f1db764e66287471442d69068bfb58c977167264a6de2e4a2492e8c42395021bc903bca269c0bfff941ab96b3ac830aa117d2dda5408150594be6ad971de0a850bda41234e0c77f044f6f8ecfac105acf425a4f591636456c4ea0bba32a9268b4721d02c824b0346147b1a993",
                active: true,
                directChildren: 244255732,
                salesOrgHeaderId: Guid.Parse("2d65cdf9-4242-44e8-8583-a57e4f3ee7f7"),
                parentId: null
            ));

            await _salesOrgHierarchyRepository.InsertAsync(new SalesOrgHierarchy
            (
                id: Guid.Parse("eafc07fd-99a5-426a-b63f-51e1b4b3e040"),
                code: "bb40b51ad0d84f779c0b",
                name: "4b12b0b0d065429388c2011bcd5a4d611200a0b49e99492d85520c47f59bf01aa7358d62b14847efa08ec12943c86d390d25990ca9204184a1a716cd581408a68707a49c749043fe815a5b9a088eed6dbbb845e5fab64bcebd3da7d6165e75c13e0db4c4328a43798cb5959c1ecb529d034208e0227a4097b78335cf96cedd3",
                level: 5,
                isRoute: true,
                isSellingZone: true,
                hierarchyCode: "229436bb15b24eaa9fc148ff06577a24c59cc14e70f04dbe8c8c272d22acaba75f5b9a9fb6084e4ebb76c65be7426feb779f28cfd4974a3e9c7c52dac2d2646f12c872f6bc704fa28391e9d92d0e71e49fe2c599ef794ae2834152eccbad7095618c19a547b342b7a67bc1929ae76352e763b05a9fab42eeb9693d969c8cbdc1f82188d56433474c8e27bfb9e47c85dc62af2bfd824c49ff810f3b55a64f2443b0851aeecfef451692dace34adf548350d46c9f9aff9488ebd19b9e61a9f254a502f322da4c64339a0903cbeaf99ae5c0a8b737bf3ab456582ccd917b7f38f162689eeb438154b7f842761f39864bbf367db3f081d57423ea592",
                active: true,
                directChildren: 1927269101,
                salesOrgHeaderId: Guid.Parse("2d65cdf9-4242-44e8-8583-a57e4f3ee7f7"),
                parentId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}