using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerAttributeValues;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomersDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SystemDatasDataSeedContributor _systemDatasDataSeedContributor;

        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        private readonly PriceListsDataSeedContributor _priceListsDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor0;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor1;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor2;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor3;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor4;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor0;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor1;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor2;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor3;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor4;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor5;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor6;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor7;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor8;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor9;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor10;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor11;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor12;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor13;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor14;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor15;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor16;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor17;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor18;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor19;

        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        public CustomersDataSeedContributor(ICustomerRepository customerRepository, IUnitOfWorkManager unitOfWorkManager, SystemDatasDataSeedContributor systemDatasDataSeedContributor, 
            CompaniesDataSeedContributor companiesDataSeedContributor, PriceListsDataSeedContributor priceListsDataSeedContributor, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor0, GeoMastersDataSeedContributor geoMastersDataSeedContributor1,
            GeoMastersDataSeedContributor geoMastersDataSeedContributor2, GeoMastersDataSeedContributor geoMastersDataSeedContributor3, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor4, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor0, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor1, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor2, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor3, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor4, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor5, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor6, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor7,
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor8, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor9, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor10, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor11,
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor12, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor13,
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor14,
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor15, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor16, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor17, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor18, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor19, 
            CustomersDataSeedContributor customersDataSeedContributor)
        {
            _customerRepository = customerRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _systemDatasDataSeedContributor = systemDatasDataSeedContributor; _companiesDataSeedContributor = companiesDataSeedContributor; _priceListsDataSeedContributor = priceListsDataSeedContributor; 
            _geoMastersDataSeedContributor0 = geoMastersDataSeedContributor0; 
            _geoMastersDataSeedContributor1 = geoMastersDataSeedContributor1; 
            _geoMastersDataSeedContributor2 = geoMastersDataSeedContributor2; 
            _geoMastersDataSeedContributor3 = geoMastersDataSeedContributor3; 
            _geoMastersDataSeedContributor4 = geoMastersDataSeedContributor4; 
            _customerAttributeValuesDataSeedContributor0 = customerAttributeValuesDataSeedContributor0; _customerAttributeValuesDataSeedContributor1 = customerAttributeValuesDataSeedContributor1; 
            _customerAttributeValuesDataSeedContributor2 = customerAttributeValuesDataSeedContributor2; _customerAttributeValuesDataSeedContributor3 = customerAttributeValuesDataSeedContributor3; 
            _customerAttributeValuesDataSeedContributor4 = customerAttributeValuesDataSeedContributor4; _customerAttributeValuesDataSeedContributor5 = customerAttributeValuesDataSeedContributor5; 
            _customerAttributeValuesDataSeedContributor6 = customerAttributeValuesDataSeedContributor6; _customerAttributeValuesDataSeedContributor7 = customerAttributeValuesDataSeedContributor7; 
            _customerAttributeValuesDataSeedContributor8 = customerAttributeValuesDataSeedContributor8; _customerAttributeValuesDataSeedContributor9 = customerAttributeValuesDataSeedContributor9; 
            _customerAttributeValuesDataSeedContributor10 = customerAttributeValuesDataSeedContributor10; _customerAttributeValuesDataSeedContributor11 = customerAttributeValuesDataSeedContributor11; 
            _customerAttributeValuesDataSeedContributor12 = customerAttributeValuesDataSeedContributor12; _customerAttributeValuesDataSeedContributor13 = customerAttributeValuesDataSeedContributor13; 
            _customerAttributeValuesDataSeedContributor14 = customerAttributeValuesDataSeedContributor14; _customerAttributeValuesDataSeedContributor15 = customerAttributeValuesDataSeedContributor15; 
            _customerAttributeValuesDataSeedContributor16 = customerAttributeValuesDataSeedContributor16; _customerAttributeValuesDataSeedContributor17 = customerAttributeValuesDataSeedContributor17; 
            _customerAttributeValuesDataSeedContributor18 = customerAttributeValuesDataSeedContributor18; _customerAttributeValuesDataSeedContributor19 = customerAttributeValuesDataSeedContributor19;
            _customersDataSeedContributor = customersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _systemDatasDataSeedContributor.SeedAsync(context);
            await _companiesDataSeedContributor.SeedAsync(context);
            await _priceListsDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor0.SeedAsync(context);
            await _geoMastersDataSeedContributor1.SeedAsync(context);
            await _geoMastersDataSeedContributor2.SeedAsync(context);
            await _geoMastersDataSeedContributor3.SeedAsync(context);
            await _geoMastersDataSeedContributor4.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor0.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor1.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor2.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor3.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor4.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor5.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor6.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor7.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor8.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor9.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor10.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor11.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor12.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor13.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor14.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor15.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor16.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor17.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor18.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor19.SeedAsync(context);
            await _customersDataSeedContributor.SeedAsync(context);

            await _customerRepository.InsertAsync(new Customer
            (
                id: Guid.Parse("b01d38a2-a195-46f8-8e56-23ec001f6931"),
                code: "5fc4eb8d9d6a48dab1dd",
                name: "d380c142804643a28efa3474b1033df623d49f9a4b9c406c8777353fcc6bfad03c4e48e57415454aa408bcfa01005c45b5617b069f2f43d998ddb294dbbd85d6e176160037604164ab05ac2e6b69e786875e08de5a774847afec3ebd9b18e40fa221b3f3c9a9451985c56b5bf3589dffa59d10c73e0f4141a429ef68674a3cd",
                phone1: "99ad41f38666458d96a07442088f37e3fa7b395adafe4bb586",
                phone2: "0da1a5c17ede47c7a2c3303c86b6867f3ab7c8b87b564a72ba",
                erpCode: "6f020e741ef1434ba67a",
                license: "34bd05dae541435b8749e454114fce4ca308402f1121483790",
                taxCode: "5b38d1c5b85e4224af25f3eb75f82d1f5317c39e45fc4b66b2",
                vatName: "dddc572b11a14f648f482274435e1302a8eb1addd83d4aa0b13507766efde7674fde50a6ce7b4b3b990bcbf7dac4278fd02e51a1deca4225864f5958b8bbc9d361c5e0dbb09f4b8b8e388c0276156aec8cf0e41855094cdd876b8f5aa57cd98fe26151ccbbf8465f96b08fe811593e016527d19ab36e4ea3923c092ed63d100",
                vatAddress: "5258a317699f47d180f7bca2343b31708ec3e629ca6e41b68b98fa2aff865081cc245d835a30487d9b665fff43e3a71fcfcc5fb2f877425b9e64c09491fb6fd9ff0adec533b3460e956ded59b1be0be246e730aa4c574acb99ea17c6ca707cacafc6625aa5844c13be282ff5df7febdf11e999c59b364d019205d4b1b75b1d8f55d9dccdfa2d49eb98835b0b11cc6b9659db175cc5fe498db9d3b56e28158e1bf1f75d3d5930432099c52d971bc907ccb7f76bb690dd4c3184017330e284df1c512e363794e2488aa9a628a40a49e1e7c74b10a835ac4377a4541dacc9b892fb45ec1e385dbc4888b1571be1c089e27456685f4f2fba4253844620c428aaa73a437810b8be944944b9bbf5900c8419c68763d1aa935b405e908e4edcf5ad0759dfdcc2c3d519478daa62373434f816a9fc8158cc258e42afb5f8867a32759e3af7ed87798c08423d86dc0eb52f64a56b1034bac2bb95448d86c54e77c2164adb3d17fb04634e49c8bedbe606f587511f71bea1ba1d4941f3bb7417958c761904f3833d03c19d48c883e3789e8ebda9f65b0c932ae1aa48c1a732f5514ae1be5dabb75d752bde4f93ab7dbfcfcfbe5cf28588bb3a72344bf7b6071f9a8fd134180d39f990150349cdb477535782dc939a8e28b4a09bb24ceba89d2f7c2a16a4092de644decb1b4e978a6e4d3d2172b228eac34681",
                active: true,
                effectiveDate: new DateTime(2022, 5, 15),
                endDate: new DateTime(2011, 10, 4),
                creditLimit: 919200647,
                isCompany: true,
                warehouseId: Guid.Parse("aceff041-f1dd-4dd0-8b1a-9c05e2d5255a"),
                street: "3584e74b0e864d85a7c8aa19d70c6865242305aadbe545849deea673f45853d525dc1ec3271b45c5a221c060414a80579e66d543e71c4d45908c5c6736faaef1dfbd3b66358447c09477217c5de012ac31615242cceb4cc79bc5c8ca7af557ac415ec99e3e794bbda0f1e769650cca0964ccd5ca1590482894332164b4e8350",
                address: "29de2d21ad3340ed8a802962c81e051dcfa4a3dda3304a0fbe857889671c8e8e8adba1ba78c843a8a56fd3ff467d2414785aef9e178241a9a41572e6d131d00687b94ffc336245879ba2889d845b570f1e538d9de36647288173fcaebe16785bd93adfe5fd7d46f99285d95b465a3b37b77aef51c4e447a5b6fe89608adb4826d0821bc1951d457eaf334c3e1933c18aff239b33a37c447199491a01d9383bea241e24a227474a57a9b22598d48f03a095969062ec7b46989bf165b43b2ce27cb045728e093d49db96a9d7bf856e4f79c119b0bd0eb6477c9e84cbbe2102ab932fb01ff799154cab9344a597861eb8b5588c5d23aa744b829c1b",
                latitude: "e89afb6958d14ba1a84ec34f24a432d0eb0789644d484f5faf18d28c2c91048729ddf6a8c30f45059288210e549d51472758199a5ed048cd8004ce6981e06b71b9eecebf853c44679382a54411bdc0699eddaa3928b14287bcd07f1628d1b977b3b3f3f8340748b2ae497ee0e68d3795bb604b139b4645648fbf9b4ed641e1a",
                longitude: "d12a0accbdd4420ea62e89c08140e0f8cbc0f103a0744abba3274c163cd1320e64302fdc0ba44576b24e3f7e2c85390945bff9f4375e4938bcfaeeb7a5b4d57d0bd67ad79f60424abaf0ee9c401573e8496b89384eb041109bcdaaec7c76edcde43090cd85334c85b05e5dec1ebc5170ff2414c4d0534cffa3e3d08b9f1782d",
                sfaCustomerCode: "875187295c07475fbfd5",
                lastOrderDate: new DateTime(2012, 6, 12),
                paymentTermId: null,
                linkedCompanyId: null,
                priceListId: null,
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null,
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
                attr5Id: null,
                attr6Id: null,
                attr7Id: null,
                attr8Id: null,
                attr9Id: null,
                attr10Id: null,
                attr11Id: null,
                attr12Id: null,
                attr13Id: null,
                attr14Id: null,
                attr15Id: null,
                attr16Id: null,
                attr17Id: null,
                attr18Id: null,
                attr19Id: null,
                paymentId: null
            ));

            await _customerRepository.InsertAsync(new Customer
            (
                id: Guid.Parse("6a5382e8-87b0-4716-afc6-a8bd737377fc"),
                code: "8ab05448ebbc4cc9bd2c",
                name: "7a2577aba23849cf9ef3b8725e7505bc5162493448b34e5885fe80694dc11dbbb7934139c4d64faeb4b51bf9b9c0acb65511bc22cb2a430c88174b39140b51930283cfda894143f4bfce392ef90098b8281c7ab7fbad4f2da24062cbda69c218b0cd9a32a7854ee99b50db744ad9388e74368fcf10db4e4b80515fc436d279f",
                phone1: "3965fa0ee9ca473e9613d18b8dbe3d56379a08ace97441019d",
                phone2: "995968b4283c443b9165b5f28311555136cf756df3e34f19a9",
                erpCode: "ff9ec3bdc7eb4e51a732",
                license: "8fe20536dca34b628956865ae2bfba2114357cd6e7d041dd93",
                taxCode: "b041a3bc3951486bb96d52ea5c9c3ff9227d1c90e9534cad91",
                vatName: "9f323dd156cc4d6cb1ae7d473a9540657e59771ede2849cdbfb71dd012cccdb9c143b492d9f54c4898ce8642ad3014391686de1c51ee4d128e50d0ca1bb877ef0068dd8376a44fe7b2492390bd99f106a1d154fe178c410d9f3a8cc875fc019388bc0b607d964c979b87a434dddbdcf2ee217e89f23f459f9537481cda2bf12",
                vatAddress: "ade75b006ef94a379cb5fda695382ad4964634e731974e5dbdab3cdd2ba8c924a4f4eedf5f724e588a7fdc5c0a7584dc58e0d68e15694bc39242cfb123914b40b8ccfe879ee342f6bddb182e01c09ea789ff1f15dff143d5a2e3dcfc452e0f4b708a45ba0dd34bc98caba50604c4774d1d61350e4bbb423496e0c1e5c44ba313ca2f396939244cd3818ac87f587457c5f624b6cceb1f4abaa517348ff0c6bbe07c86377f3cad4dbb8eb92703613767775724ee3baba4420dba294548280f677081b5156fe5a445ae82d89571bd06f09b995d66dbd7464935bbbd8caa6f95c1e38065f8bb352a495c92dd8cfefcce2355477a221556a64af78881bebb7ad2f38c77ce14587d37472a94fcf3884766910f81a71faa5be048d78e44b8d4da24e40a55c787601c854a268a900d70b173052b71cd9072733c4f0ea87fdc05c3fe3e178aaa1c293ad44df69b6b0b0042afa985535d67fc44174f5691dca7cbfd52fb7adc3d6fa59318455d8d716e7ce8e2e6e4e4cd479a839741c982ea3feff011ef7b5577d98badfe414e83d6480479502a9dd3799b1c39db4a0d9ca5b95b49cf34aaf5dd2e6ffe3844dcb53074dd0ff8f9fef07f1699250041bda18a4e730169f02f6b679b3393b841699ecdaf299ff05703ff1dac27dbd74e51a408d7a8ffd3a1482ea50c223c074953a03fdd65bc4ad22faec015c7",
                active: true,
                effectiveDate: new DateTime(2019, 8, 12),
                endDate: new DateTime(2022, 5, 7),
                creditLimit: 1903392816,
                isCompany: true,
                warehouseId: Guid.Parse("f71e6ce5-98d9-46a7-9b11-94333a888d81"),
                street: "caa399adfc1a4ff0af1d308f82e7786a65b4dd1620fa4a15841b152c21bc5c8a8fdda16c6db64d2abde3fbe8083f046dc9684c365fa54a6eb98de8ab3d6a1ff7b53d3d614a59497ca1399f468286c9f5350bf186a84c4108a6873aba9b7605a49b54cd58d8b64eb0803bca3223d91beab00fd7e31f1a40868e6b6c0ee5fa04d",
                address: "3dad404775d64db294208b5727596463addeb4472bb0433889b283cb3d191a82d75acc26cd1041b8ab95f8657ba90a50f49775eae3ed416392586b3e74adf59e10a5adf8a8dc4f16935c0a8b36eab96df931da89bda54ce6b096cc391e1e1ae3c7f0e540e42e4847aa624366c27b6d8be7e59b5dab444ceabcba4501a52dc619aac5167c33e9484b9fda223d02aadf3698f125296e7e4edf86e139c68481d57b7c74cffea9a944488f0db27aadabb012742775a4974e4622af0e54a65b101c00212b1faf07874d6789ee3388f5994cd8cd831fb5419b4b688989bce09ba08dd8a8aae331fd6f4e5982dd2c10e1a9b7df5acfba7a437c4436ba5d",
                latitude: "dc23f48d7d9c4a80a4197267c2b9ab11fc2ca9cef49c45c0b86cb9b38a79d081db87013fd5a14b3683035400beb17891028ad878aba44a36a2b05fb236b96a08dab28ab8c107466c8fa0befd517887d9e6c20548a3ea4ce29d10561e32d0b3ed598f791731a24eac963138c443e41956ffd4f48a0f8845e7bac92bb639090fa",
                longitude: "17acf3d9874044f495dc7a73b525ea83dd0404d5b35747a0b1eef6f47378f3fc35f978981b184a8da3952a7278339e7fe8652acc16234e0fabf6d5a7c2cca3727dfb014a789c4bc3827405639411d99a66512df2787848179790ac0a2382e6cd9da925e5664e4808b82caf8a65062617f5235055ce804f6b8ed6fe212a8f4cc",
                sfaCustomerCode: "bd4175a2f1f145408f01",
                lastOrderDate: new DateTime(2001, 1, 5),
                paymentTermId: null,
                linkedCompanyId: null,
                priceListId: null,
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null,
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
                attr5Id: null,
                attr6Id: null,
                attr7Id: null,
                attr8Id: null,
                attr9Id: null,
                attr10Id: null,
                attr11Id: null,
                attr12Id: null,
                attr13Id: null,
                attr14Id: null,
                attr15Id: null,
                attr16Id: null,
                attr17Id: null,
                attr18Id: null,
                attr19Id: null,
                paymentId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}