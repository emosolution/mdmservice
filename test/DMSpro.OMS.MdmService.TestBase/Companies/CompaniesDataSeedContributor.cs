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
                id: Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"),
                code: "fcc0c5b331fe4802b528",
                name: "530d2fb7696e4c02a7fab6ce39cc35f27f06e96e17ba4e06869a714f48478fb1a7bc33cbdbf342d99888e8c9fa124eef1f03",
                street: "7c895458d9e64dde81dac7f0ad11cf415aee2743e8854ed88a1333",
                address: "6e6ffa79c7594049bc54bea3afca7d528d43409a4c0f4c17afd585742027406214022f22b8bc429b88769dd20391fb4e946362e2054846fea37ab669571a4e11765cd23cccf54a7e9e723d534fa456706555fb982ef74b69be8ec85237d2d166b46aac8e8d74411f9498f0d5621f6af04330a1ab9cb548e7b7543530751caf570c39fcec848c40ad9e6e10001aba81b56751293fa53046788e5eb853cfaa11fed0c757cbd2b845f5bbcc65e96736fccd21fcac938b1048139efd6b3d6dbc5b704b439a14de174e7d8e80a976442fc110dce270becb6b4571a9db77aae39372f217949dd1dbb0478f89a4658d632e6d04dd45e73b44524058bdc6ee9205b5379c7f071d41349e409e8ceffc53a44d2345f320fdb741284280aae3e7f7fcbc584d0c5ba6bc7d354a8792058d5f0901ce3be6d36572b4f940c691bacb6cb2b21bdf3611a9485d5845579d8ebf30f0756d2ec0227a94541246278047e894aa61bd75859f6569f0e04988b1989121b89bdd6aa86c29708b2449d1a180592e109e026aeae35773b0cd4d148156db6b8a69f9745738b51827ad4bab8f67f9c61ab8d94384bd9ddca84e49cb9bb2564c8297363d02840dc6da214ab1bfe6301c7c521bf950026c0157674e29990705082ea94c3557657b92206741c4a0b5da294d8a559e983c72c0ec57495ca6f2d3261a0fee0c2a40cba0",
                phone: "325fa95ee07c4acda3e2",
                license: "c009d321f9a8431592c0dd5ea668c09e85a0bcbf8ab94ae580dcde90ec3abee2ec1657a0ef1d4ca0b58671449570243f9927",
                taxCode: "800e14879af84fa9be54705caeda50be86f1b3bae1424882899f78a0b68f7c7fd966756d827744baabec5da12a45bf5ef77c",
                vatName: "67d3069dc1",
                vatAddress: "577236a92924448490177ed474bc8bc0a329b2363eab4482a4",
                erpCode: "8dc2770d969b47659fc800d92f62d76280b993688cb34f62b4d7a56af53f63f3af67fd90797840a194b118b0c257202b0a6a",
                active: true,
                effectiveDate: new DateTime(2003, 3, 15),
                endDate: new DateTime(2001, 1, 21),
                isHO: true,
                latitude: "c5ca0f51e9bc4ac785256851a446f9218a56ce3987aa41e5a7aeef4495f505e50afac6d",
                longitude: "7400f73244404e2b91aa162dcc560d44cdea05d371564308a51362d8222d7438319601ecbd0e492ba2c16ea743ac3",
                contactName: "312e7837275f4ef5b8c41b4dc451b704ac38524a4e3f4be4a08c55859d3e004a0552918f24ff420fb375f",
                contactPhone: "a8debc35f6fb4f0a94070f0a81cec04d07203f9a70354373a88036b68057a2266d78028c07fd4fea88d7a61a5d59d9253",
                parentId: null,
                geoLevel0Id: null,
                geoLevel1Id: null,
                geoLevel2Id: null,
                geoLevel3Id: null,
                geoLevel4Id: null
            ));

            await _companyRepository.InsertAsync(new Company
            (
                id: Guid.Parse("6a72cddb-40c4-4405-b648-3ba089a88f75"),
                code: "b0e66019b62241f59ab8",
                name: "031d1eb86ea8460180122e8b76ab6d247940cc75f6204e9685ca7641d47b3db979ad64a17cd54d74847bb8b8c7d2928ca38a",
                street: "4d96c24f7e5b46ac9884e363b50b9019d1bf36d64d704512aee1",
                address: "0316d46905084f4fac9cc41156057687e56475a57e454954be4c19670ee009bb2ddf349fe8cd47399a2958a7b7e82e8c237e25baaf52450dbc57f795a32dc2a9fc1ffe15538d449a9935be3b642fa211cb096a2763ad45d0a35c69ed882f023bcb602e8ca84a43f3b4d0aa6e841420a6effcd69201a24fdc9b23029b5ade741ca2db9e80f8ed4747b4797f73b9d16fad23ce2092f72d4110968e991d04880c4a91c994af26964c548d3a6876affd0a688815e96142db4ed8a011e8ad3d74cc844629d4fa1ef54ed78d32c4d70124a5680a85d78b498346f295aa4bc5d6fed6340cc11d0d6cc64a0aaa730b92bd1d65fb0d5bb5b79e3346f6a3779d6941ff8ccdff5be74bdb274bb7b70f311bc0315157ad8c7f5d573d4a5cb227c5ebfe0f5483bfa99f6ad1c746b9b3944dc34e32834b612e2f15087e4d54b942484a3e17337647d9dcac7c47448e8387eae91e81b747a7937becd20d4f44aa8780988fe05ff6203b859661e243e59eafb645a405a60e474e1bde233e40bda9a9c77bdcfde74c76bf370ceee7418aa96b782d00cbfa22d4f32773119d4a73a96cade72a0d3d607cfbe786433b4fb4b04162c73840269d31f0989066194d64b7867823bef4b5340b0e9a2a16ba4f2b8526a02f413dabd8e455eb76026444b5b840cc05dbc1c2c3cbd6bda287184624a04d88ff610cc36226725bd8",
                phone: "8812876ece7741578152",
                license: "90d91100a2174d83956fb20e6cbc59c73819ebb355bb45ee89fcef2b5190c818fc52a80c3ce64f33b55ccd2b509616801f2b",
                taxCode: "0adccce8a46d47c4b0a71f9d599da46cc44b89d202c84e5185a75f76e8f21e2242fb55cc44a94084948955af4d8b7c39498d",
                vatName: "7b4a6f682467420dbcc757de5e7674a6b08b835c7b0c4a46a5fd1f6ace29581e35f7a02c",
                vatAddress: "e4b493f9fa114ecbb43046d920b09296233",
                erpCode: "4c0d1fc01f824e10856c7630fa1f6e5b6c513eb4b33e42f1a002679d71c51c6ea6edcb08bfdb40b1ae8e1983555b62f186dc",
                active: true,
                effectiveDate: new DateTime(2022, 11, 3),
                endDate: new DateTime(2018, 6, 7),
                isHO: true,
                latitude: "02fcc43cc68947deb4df8d662134eb2aa05eef724d924091b8f8f9fb5240b4a3",
                longitude: "0e5e16cb8a794521b48466a22c3321ea0f3699cfa3f640aeaa7c0ca3576d6c7afa372029d80b493ca5c8e",
                contactName: "1d0e7d5e7b3b4f73af124365cd738f038d3e6bf057a24d54bf9b4c5333",
                contactPhone: "fc272e367e754d41ad9e5e3c8e8573ff0d974f795af74abe91679f2",
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