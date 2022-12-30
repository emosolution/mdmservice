using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.Companies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompaniesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor0;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor1;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor2;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor3;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor4;

        public CompaniesDataSeedContributor(ICompanyRepository companyRepository, IUnitOfWorkManager unitOfWorkManager, CompaniesDataSeedContributor companiesDataSeedContributor, GeoMastersDataSeedContributor geoMastersDataSeedContributor0, GeoMastersDataSeedContributor geoMastersDataSeedContributor1, GeoMastersDataSeedContributor geoMastersDataSeedContributor2, GeoMastersDataSeedContributor geoMastersDataSeedContributor3, GeoMastersDataSeedContributor geoMastersDataSeedContributor4)
        {
            _companyRepository = companyRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _companiesDataSeedContributor = companiesDataSeedContributor;
            _geoMastersDataSeedContributor0 = geoMastersDataSeedContributor0;
            _geoMastersDataSeedContributor1 = geoMastersDataSeedContributor1;
            _geoMastersDataSeedContributor2 = geoMastersDataSeedContributor2;
            _geoMastersDataSeedContributor3 = geoMastersDataSeedContributor3;
            _geoMastersDataSeedContributor4 = geoMastersDataSeedContributor4;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companiesDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor0.SeedAsync(context);
            await _geoMastersDataSeedContributor1.SeedAsync(context);
            await _geoMastersDataSeedContributor2.SeedAsync(context);
            await _geoMastersDataSeedContributor3.SeedAsync(context);
            await _geoMastersDataSeedContributor4.SeedAsync(context);

            await _companyRepository.InsertAsync(new Company
            (
                id: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                code: "76e9955d0e6046edb76f",
                name: "561cefb0c3b643a19085880dce3a49f270bb7ec73ab347f98ccd4bbaebb2e0e3370c692757e94a0d8c49d8bc32107cd4cfe7",
                street: "e96534b7c15b4aeca48d1dc70d32581f52f88baca95a49b29ff14",
                address: "81371bec04f24907aaa47b5405b1bad8637d35903f0b44079c59222d26cb57ea684cbe060b4149b1a2f413c10983b99e5c74400c90a945eb9651f79dfe61ef3fbd05af06d89a4180842a4900c9eceeb923e2443f8b0a46b1ac79a300e5d503e1e201550c4e99492a93d2e2e66d6e1baf75978d6a13ce4dea96da1bde73032ad8206da625fbe448ac861c3e24f1ebdf25806a5ecc7dab4086b71c3824e9b70e78eec701029e3c440b8d9296fc1d15bd3888fbe6e1bdae43b9a7e91cf7fda4a4ec82fdc231ed184b3ead9b3d2fe19530d6c48fce414d8740258aa7c7e6063f338a5bbbf979409849e5beb58321515beb0beae7a5bf170d4615b521c28996b1214f59f960f1e919443ab434d9e9b6d66779ed5b37049a664fd692acb5d90d0564b89d0cda17c7344247b2148e8fb951b501ef571e5d95d74536bf56b0b6d722f9ce4b992e8129d244cf8d141b76bd9dc6f0f5e400d0bd404979856dadca4e02d24a1012c6f007e946d7b55e41951ee4e3bc2b08d963068e4057a376ce8062716d14fcca99b40ed3499ca5d9b3709af276e27d9add00e8614beba8da5145af160172604d3e1039084da1a7bc06a876f1639c337fc7dcfedb4428834ebc8b401f4bc3c3c2198a4cc84ef6b08a648c1df9fbaca3eba96010ab47ce922769704515753e2d1062263c7049788f35594d7eb3b53565f0b09a",
                phone: "3d695c50da0e4709a730",
                license: "8ac426872fc0497d8d96d1dc20cd1c1f887534bbb96644398d7ca73bad36decb70b5b047cd384586a686cd245eb6328680c4",
                taxCode: "5bca8e802b8c4d379a38e2db6c42a2e6485d70f9f24c478cbc7016ad43657e9c5f0f39b194974ddd8234298c2ee005f54f4a",
                vatName: "bf8dc4a325554832b76ba5b559f0bddb42aacdb6391f4897b0d4d574bdee6ed41de46f8aa79a4a9799229b1c3044357fe4",
                vatAddress: "83b4a5880e0b4c43930719b7ef59b8ca552a943fd6434c99bd1f597082fa548ad8fc57c6bb8c41f49ca9033f48e84236",
                erpCode: "079b347d67c940459961a3b7d7e66acca4da54336c034a899e0d2202d37d213a8e613a88b9334c9aa7872ee7ebca1915db6f",
                active: true,
                effectiveDate: new DateTime(2021, 4, 10),
                endDate: new DateTime(2020, 2, 13),
                isHO: true,
                latitude: "40e2d844339c446a892d169fbda06d12074b5399b6574dfa85f7b4ca150a45480",
                longitude: "cfe017e774a0458183a99ab34917e4bec0e1af2",
                contactName: "271bed18bfbb4931aec3fc8f47ec7f",
                contactPhone: "9eb5825898f947a1ab654e05c76cf278086669f026d941ffa099ea39107",
                parentId: null,
                geoLevel0Id: null,
                geoLevel1Id: null,
                geoLevel2Id: null,
                geoLevel3Id: null,
                geoLevel4Id: null
            ));

            await _companyRepository.InsertAsync(new Company
            (
                id: Guid.Parse("2164ab11-34c0-48d9-ba88-2230c4ef2b21"),
                code: "643ec1cb3adf44b99b0d",
                name: "4798d4a9a94843849752734ee677ac7a7c91bed730944815bd042844bfe7c13e46c20035cbed474695a2de2bd89c44c58b2f",
                street: "0a61e212e5144dbca186e2453bfe636cad9cd862a0154544b378805925e561c9fc0145db144f4119acf",
                address: "c5317204b9dc421991f2799ebde5423550ddf63729cb4deebf920ba2033159b4d45267f5afae4d0d89fc83f4488f63e282502de2b4a54c6b956b430b4b16e66384888ef1d21740b1af97b178246ed40363bd6a86b197465ebeb855705e1fd53a8ba7514ffa36498a8188b9c4961f437060c5e3c411bd4d0097c2da54d4290013c340748ed61048f29219b7e51df3cc28d36b305aa5294421b42de8ad0d36fe080c4ed8d3eaf34773a408f9fe8191a39c384710b3f5c84a35acea2980aeb413804ddeb00cf0df471ea72a69c11c9fcaeeafbe11c1e1f04f73be07a0bbc47c314c7fc10dc5c35245b9925e83443b5982b9e275969f3bfc4dcf866f2bab7a5ccb66cf2a5191a3294152ae9434d6a9bd17bc705cd7bcaaa74e9c86681d6bf97a8c754a092494ecb84909a8e3f5c0e8b5b71fac7e26444357459ba4e654b73e6e565c352c679025cf48efbb103b272b5f143630d1cb0ff00b4e7784ffa9c2cae72c9aad23ac99e8914c7da27107db88670bfec80898877eda4b7597b0434e99db3b1a40976ac9dc794cbeadc9136ae412165032fa4bf0b3ff4f82ada032bd3e9c59913febeaed3c334c588fb6eae950673d56a19a4fc608da49ca83c29346fd2c69ca7aff281f18714100a9ffdbf132ab2a8e79c7b51a5473442280f42e88cb6207a5aae9f7eee6ac4176866e099b305452d1469d320e",
                phone: "6aeb4e09a6d4480caaa1",
                license: "7343ceb115bd486a923763bd57b3f2aa9109ada8fecd4dbb87e1e2779aa0a0847e0100bef21649b4bf7f83ab363e7076cf80",
                taxCode: "bd8c3c1ec5d0403f9c62a80d533785853247100c0d264670973f76510217bb4d043df8c859a54bf899964fb97afd4757275d",
                vatName: "629aba6acb5f44d79f76204df4264a60d98231b4d691489481f544350b702f382fd210386faf4d9cbd614dbd00f",
                vatAddress: "6b1bf310d5fa479a95976d3e91add132778684b59a254600b054e1eb885f6047",
                erpCode: "66ab6843a135473e99744214804bed9aa2c1f9c741cc4b5aad1c2cb6a986dd40af0ad6bc24374ecdb23bd30269a329dbd6ec",
                active: true,
                effectiveDate: new DateTime(2000, 9, 27),
                endDate: new DateTime(2020, 9, 12),
                isHO: true,
                latitude: "c750f856eb184e59948cbbf8303f6812d75f5292b7d14c8db6fc9abbee587a35ac0ff99b94e3417b81176b52f34e30",
                longitude: "818541a786de4849a7d29595ac0fab735f2c3ebbb7d34fc594277c4059534b019af17ec9",
                contactName: "3b3ddc0144d",
                contactPhone: "9f9c040fa1674fcfafe280a60ebae0ec876f573c337f45acb0dcfa7a09b00",
                parentId: null,
                geoLevel0Id: null,
                geoLevel1Id: null,
                geoLevel2Id: null,
                geoLevel3Id: null,
                geoLevel4Id: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}