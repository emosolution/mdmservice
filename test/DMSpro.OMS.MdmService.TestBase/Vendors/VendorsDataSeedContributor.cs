using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.Companies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.Vendors;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVendorRepository _vendorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        private readonly PriceListsDataSeedContributor _priceListsDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor0;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor1;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor2;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor3;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor4;

        public VendorsDataSeedContributor(IVendorRepository vendorRepository, IUnitOfWorkManager unitOfWorkManager, CompaniesDataSeedContributor companiesDataSeedContributor, 
            PriceListsDataSeedContributor priceListsDataSeedContributor, GeoMastersDataSeedContributor geoMastersDataSeedContributor0, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor1, GeoMastersDataSeedContributor geoMastersDataSeedContributor2, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor3, GeoMastersDataSeedContributor geoMastersDataSeedContributor4)
        {
            _vendorRepository = vendorRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _companiesDataSeedContributor = companiesDataSeedContributor; _priceListsDataSeedContributor = priceListsDataSeedContributor; 
            _geoMastersDataSeedContributor0 = geoMastersDataSeedContributor0; _geoMastersDataSeedContributor1 = geoMastersDataSeedContributor1; 
            _geoMastersDataSeedContributor2 = geoMastersDataSeedContributor2; _geoMastersDataSeedContributor3 = geoMastersDataSeedContributor3; 
            _geoMastersDataSeedContributor4 = geoMastersDataSeedContributor4;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companiesDataSeedContributor.SeedAsync(context);
            await _priceListsDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor0.SeedAsync(context);
            await _geoMastersDataSeedContributor1.SeedAsync(context);
            await _geoMastersDataSeedContributor2.SeedAsync(context);
            await _geoMastersDataSeedContributor3.SeedAsync(context);
            await _geoMastersDataSeedContributor4.SeedAsync(context);

            await _vendorRepository.InsertAsync(new Vendor
            (
                id: Guid.Parse("a626f6bb-483a-42fe-aa9e-177a7202e7b5"),
                code: "bb0b4b33d1724aff81d7",
                name: "33b19b9b39ad4e7fb513411709beb21b98f19cb8b2ab4e0bbb5795135c2f0c82302d81d26b4540fa9b16f2bcb87e93305cb5b0f162664a318436c0bc57034cf0ff732eefae0f4f59b9c260c0675e6b602ebf66b2a055460ab6b1096d9318e30af05422f2",
                shortName: "2f80b77b8c024bdf8d8d6f449609d000f91e5991345e4688a10f8d0b370568de89955d189a274e86bc99d79bc3c75e50652e517bcfed4bfba361006d20eccf69c6e23e44ce534fa4be309051253243afbdb184ed95c94a85bd8dc750c1a784b8fe342436",
                phone1: "fa94ec6caf2f46f59dda3d5fdd3a4b18abce40921a4f40a6a72c4c8d7c89308bc2c207",
                phone2: "cf67e4b532bc4d33bc8be265a20519ec6e31e316a0ab47b9960e2c8",
                erpCode: "630c44c14a314281a6b8ea004",
                active: true,
                endDate: new DateTime(2006, 3, 4),
                warehouseId: Guid.Parse("a551cfa2-7bb4-4a84-bd93-88684b7cb6fb"),
                street: "323939b5bf574d5a8f604db81331005d7f8efd2301ec45e0bf400d70d1ed0439",
                address: "f8957e363c07417387769a2a7e6799ea2dd0974b",
                latitude: "ba196ee992d04712ad",
                longitude: "a6bd5a2eb0694633a46633069846350a7d22d",
                linkedCompanyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null
            ));

            await _vendorRepository.InsertAsync(new Vendor
            (
                id: Guid.Parse("fc577795-ffb4-4140-b748-b66d210682c2"),
                code: "86e689b9162947d2958b",
                name: "bf45e211ba4e4b7e959f57dc277bbbfee777fba0a6404d61aff0e9a40c8c5a5392cbf67a5fd14d1db20ab095813c69b199dab7c2f8284398b6b9f70cf4eb3ca79605a784f3794d54893d89890e9f46e1327734d595c24c33bba4c652fbd06cd446f2686e",
                shortName: "3cdba0163b86409fb824e4cccf86f6cdceea8d89e113452eab5f2f9d68c04c709bf6adc00711419cbd8c82b1c45fc912791763275fc94ed093d5206756a6c765391b7b8215de4440955d18b42c26d5bf43eec5efb96b4f489a87ce0a70affc81e7860000",
                phone1: "3e0399ee6f2445f3868cf",
                phone2: "d9c55593bc114324b570af9fa77da3a3bd008e50b7114132b9ffd42d36fd09990643e5a626ab4",
                erpCode: "7e4de57144754e87a628f118621b474",
                active: true,
                endDate: new DateTime(2000, 5, 8),
                warehouseId: Guid.Parse("402712da-6dbe-403a-93b2-59d18d8005c0"),
                street: "f91cc4f085934a7081fca146943b9b8a9c4fb5859b7b48c",
                address: "586e9cd0d7484a4ba24fe5dcb82458d1538e21913b5a44d2975af0d868b3f3707a2781242f1e4711b4c20199ad2d",
                latitude: "dd7e8ec4819e4e96841843e597297c8",
                longitude: "f4edb1421d0f433cb173333ca62d9cbe313ad89489eb4f048658d3540cceaaf",
                linkedCompanyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}