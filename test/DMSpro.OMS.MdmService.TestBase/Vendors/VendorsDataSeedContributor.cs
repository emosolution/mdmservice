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
                id: Guid.Parse("e19c23e6-f052-4267-bae1-7c923cd2d7db"),
                code: "ec0c4b5bfda24e14afb6",
                name: "a46456150a1d4a9fb117d9cfa5bdee243c44ab523f9a4fae91bbf890ad817fddb7f3aab05b9f4b88991b6043c131fea87dd7f3dcaa6f41c5ad98208b7ae04b27c4f6113bfd674e9da1eeb5192833b0eb5266294ff78c4bfd9551f5ed172b283332b63eb4",
                shortName: "9acfef8cc37b40019d2a381f976e7ebc2ef3a70f42ae48139422b1e2bea67fdec5f15824b2c74ebdaee60c1f75f55f4f2ba4b314e05a4068a83c487b84be732c80e008c0e5d049acbb1ecbf7c388ba3a6e6b6a7f586b48509a53415b6d3a0e19ab3b292a",
                phone1: "7be3bff1abde45f4b887f5f729524f1cb97d740dd4b74832af2231777f6293756c8646a87d334eb8a1f9c9ad9",
                phone2: "b9a55f1d1b2b45debdbf30cd3335be7d3",
                erpCode: "1239cb6299d244599425a15ad083368ddc442bb0499a4440b879e9fb215acd6925c9458ea9",
                active: true,
                endDate: new DateTime(2021, 8, 11),
                linkedCompany: "b3b12ce857844831923b",
                warehouseId: Guid.Parse("cebddb84-f425-4f67-a045-5149bf03072b"),
                street: "2759a6b3a52347feaf02d897096091c22e5d87e096bc4a029beacb708b231c06fc3ba7d748f541e6bb269b6d860e4d93f",
                address: "8325723311a240ee9c013c682bd40c2a9ed7082f2d",
                latitude: "161294507a414d05ad24986bb905402f9571",
                longitude: "7a5704460ad14ed5b4ae562285bb56e397b4ec028d0646fc9548a441de9507674db881dc84704ba7",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null,
                companyId: Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246")
            ));

            await _vendorRepository.InsertAsync(new Vendor
            (
                id: Guid.Parse("fdb433da-2f47-466d-a83e-ae779c0cc364"),
                code: "ebe53f13f66347a6bfc7",
                name: "ed9d3c14781b490eb974796ffef4c6677aa4af667d61410e95fc313d6eefb6dee93e18a60e504dca9e8e44d2d710a8893e36ff8c607f49a98eee482624a7f15d535299789db2471e9235c624aa8afce97b5aee4d95a24873a200fe7ae6883d4a02a7e5d4",
                shortName: "037cbfaea6f14c4a8afcd90d75393754faea0bdb7dda4614a981db41229ecf81647f6ffcee4245e6926ab2e9156d9e059b41e703d2a64845a46f8e461a8e92c2efb7d304374c4c74be0d69009f7cf014a538059a38274c46bef56ce0d0f140bb333d604e",
                phone1: "2a89eb2174484fe791361ebfadaf661fcd923",
                phone2: "b233fd5504f8460298fd4ac9ee4c86a5466a71f927f2459aa2da047d57faa6aa7ebbd95",
                erpCode: "ec2f722be84640258a71a5132a5b53dfdf57c5c7685f45cd859fc6613c05ca14add5280a",
                active: true,
                endDate: new DateTime(2009, 11, 23),
                linkedCompany: "62ba645ecf2e46469124",
                warehouseId: Guid.Parse("b6db53d8-c31c-49ac-bfe8-5d1bde477f86"),
                street: "fe56bc1e2da447b8bf07807d4f132a4997d11b37cb434607b",
                address: "fb4670c3277143189dd9827b040ac88dfbb37f72e9c7437fa7b1ed92cf4dd7aafb856f7aebad4d849",
                latitude: "6e1d34e7018d41f",
                longitude: "44d97e81d5d4470bbf45da714e7d37e0c04fe85e18144841",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null,
                companyId: Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}