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
                id: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                code: "f55cb7db899a414e856e",
                name: "07815d930b894ef79649e0e2ae8432f854bc10e4083641a69881e28f6e7219fed55de24fca4b43df8a8a2942e382096fe344",
                street: "033b2da7a2394579993a78a599fccedfd154d41aec114fbca9469e54e81a7f70b734fd7a6bbc45138fa7c5bde55444560e4cc67b3a214ddc94ce28f64b8f1b0405f124c9de8f4f8b8b673513f9703d1dd4b6b1b30f7a4a5188a7a68de2b8017c9d7c19828a41469281cd08fa8a097da58d9bad56a20d44c6824e1444e77bd1b",
                address: "daaa1c95e57e4d72a188497847cedf0134056189952c4e47a75a4aacc1ac404f7efc461200ee44d69efc982ddf4ac8b9dbfece466d1d462ca71c1e405ce260d64bbf9bdfc7034070bae00be58e1d2f10b3afb84f151f4d2fb86100405aebdc98402d4691e2b74fe695eec6f87866cbd682854091b90244df88bc1b9fa194dfa118b0dab4affc487f92708f6f5adfd71af87b63b6f33d4e388c8aefe0cfa69d5f9c889de999274b0683ead2fd6c68f5d398e71efa174f4ecbb005b2be4d04ab764e8235f74f1041f39044ab1be2319ace269cce656dbb43a18791f4b37393d32c00323a0560eb429abb9f4a4dafe1e6eaf8b30e3404b1458e808fbbf59d6274da5b505418b80e435a8135a243e83631ef9ba7df883603492186eda78c47d34d6a3ff14adfe1cb4568a1ac9a673612a395f89df124525244919d486eb3cdd1413ceaea080e59c642a49cb3ae3eabc6c7f705bac9350a2347e081774168015c84b228e3163b8d2140fbb5dbbcde372b6bd42c06ed5cfe9f4646801ffe4547c918c30272fec39bde430ba15f7d0cd934cd35e628d1a90c8744f297db4d0dd2fb7e7776fa645f4767479e948ce92ed6e7a19df2e4073a05a946b7a4fe917b72ec6c58af2194b5f386414a9e70cd7028a4e615420bb3c325b74711af873c64102c3ea0e55d8cf3b30646a9b4672b5eebcebff325ca2618",
                phone: "e6e014231f6447d2ab1a",
                license: "2745be2a64504c0baefc6ad165d1890dc0aab541d22f491fb1c5d5523f9117e4df600ee18f9747b7b21f7b24346ea689b201",
                taxCode: "093cb3a4be674a83aec6be456274ff777f68ca08c20544d49a13daaf512096fb66ea538133524aa4b90d6e78a79dbf545aa1",
                vatName: "cc49b4fc64b34897b0b56de80e26ad69b478d92840e74239aeaff010aa3ac56ee40947f3c6304477bd968ff7fc06ce6b0bcbd118ebbc4c57badb35557cb5261b414ff32afbcc46ed98d7e969d21aae028168fc27053147628831a08210d5b5670f1082e28fb445c3b417f35e42c9400b4ce7c328eca04ad68dfb09588dcf21f",
                vatAddress: "9871354f6e2c49ba802d8783941ed46e8ca42419e66e4bd08b21e44fe281f57d0899d2b5c7f74c9c99043d54859441c5af71a5c691934ab5ab8fb8fff4ce0aa511e22f0c8c7e45d8829c90cb7a1c5fc4580391e8bed04ccfb14bfbe09e1ece67618bf8b233424b6098c30117037d7b4379ec08d4537c4ba8ac654c065bf10748255702cb903647bd9400fd1b26e701f7235ea172a62e468f9a56c17f94cefd10113e9126263d43abad539e863a604d0c9bcaf25cec5e4ee79f4d3815990ff7042eb881ecad974be690ae3949447c4dca1b293f65d15b4dfba8660efda1d9c8791b324eec230a4f609670de8b58a9389f4856fd6e2d1c4cb3b508",
                erpCode: "4e1fb762863340f7b870e1d279403ca4d40160887a474535a0c91484dab29abd484961c5e6a047ca9799c716e4bd730cdc17",
                active: true,
                effectiveDate: new DateTime(2018, 2, 27),
                endDate: new DateTime(2015, 5, 21),
                isHO: true,
                latitude: "9c1cad3fa03048c287532c60c46a1cc8cec1c18b4412430d96e9992d846dcbab5b570fab7f2449f1aef29b6a76dd6a1ab3cc5a5afcf34abd8a88b29d74b8ae5b325a63e08d3d4205905a00b07ce7325b809a6b0e03c34f6598ca5688fb4ffed2171bd100cc0c4791a99d5a883ac332cafd5aebde58a94b30b73ac03aac584e6",
                longitude: "d28b9863586b490599251ce0ce1e31c2d9d79c02a3ff491a9a13e3f69a4625b85c45855e58fc410992c9bae59943207f6f3995736ae14155a6a312d944c02e10ff3ff2cbe91e47b5ba52e6c7ba92112aece88a4c7154494fba68e8f0a5d7181330691a728d0246fe972b44741181ceba5fc8067479714858b093b487f0496b5",
                contactName: "8d8c31350d914fc8ae2dd96c24356942b6dc796ac883413fad2e1ee502c971d5adb2866e1c9643fcab06c7dc4525a87cc90f3d04af1c4762a45de2832f0e44f4c9d14fe765ef49a181473e86559b272f1ffce4b7ce3742e5ab756238aaba4cf093fc120f4df94c1bbbe8dfc344067f8b00a321e85c9040a5b7c68782809ce0c",
                contactPhone: "fcc618b336884828bdc49ac1b4adadfd850a701c5f914e609425092ce1620cf7db86aeb4a35548c6bc8a8cb62c19a2cbaf7aebff4c014bab87984a5d681912eed9458f18c9574d0aa820362b7db9b3fa0bce25a6060f47baa52dcb272c43391e4ac4df4bd25745c9a636ecc828726fbb98165d9c75be4d20ac801c2e6f77335",
                parentId: null,
                geoLevel0Id: null,
                geoLevel1Id: null,
                geoLevel2Id: null,
                geoLevel3Id: null,
                geoLevel4Id: null
            ));

            await _companyRepository.InsertAsync(new Company
            (
                id: Guid.Parse("68a46bca-710b-45cb-87e1-4d2b6852e8da"),
                code: "1e78ec68de744b319eda",
                name: "ea0d59d833cf4bbaa199a475b68be0e516f307a440b74690a41127ef99ad3ae34fed6309e1134a368d7a790e4d262d494262",
                street: "757054b72be5469fa09fa3646d978e57186665ae959948bf99ce1feef8fb22709370acb6be514fbbbf5fa9650cd5c221f2fc50d18a60491596b9cd8fd106a640742775038f5e4048ab009f876166500b50fb8fec2dcf4fdd8fdc8907833d42cf406ad2e370f349ecbd5b5090264acde254c313e3cf604b2a868c293da8ce377",
                address: "e03bdb005907486b8f8a9a708c42ca0829f2e675198c494da41fcdda75ab0b4bc90487fae5bc47eb9b1c817a1eb5f39cbaa1287917374e129a7d099d3a44a207f21a749a91494d3cb9768188759ac8f11f3973b2d8b44d83bf733a3a83169fb06c6ef30d0d4c459facf7b84209605b28823b659ed22d44d9a7568e4bb9c135290052877efe2a4e07abb4f879a2d21d24d9b9287a1d064ab0b3a35c0bf0ef4dee82cdf0bf19c6487dae60ac7fa42290b267cba6d811b741668206ff0db3af403efa063d972dcb46aba148c54e62c3463a48c2cfa00a874d43a37ea9548db3fafb475d73c8a8154245adec7d31032380b96b25958c9b754877b1d9648b1f4e7d0ffeb633f389ba404797b5ba380f1f95b62ceb04cdfb004a388cd069d11f1aee41992da04a419644b1b0549e883e087542aa445f04741d4f30b73f250b844fa8eac2fe84de94074a42a8966d9e93904ad9ac3087634ee846058a8f8d24951a48008b9ee961cf8f45f79150f86b30846d22db7e1bd520654c33b5e02f32796e0eef332cc6d5949f409996008c07985d667b0b2e4f8570a44de8bde994b98948144483406c0d3f7f4f94a4a13199b8fb3d88d9418cb653794816b9e4412ac22a37c68e20e1c5e0f04ac4a8dd21b8b7bf2d7488c235f7ef7e43759a2a50bf939604e6038d726b9eed420f80d9a25bd7e9d93d3c6c031d",
                phone: "6736e30db18e4400a294",
                license: "6d6ca12ebd114481918e665597acf1b99c433e0f253b4defb8e6eac9ae2702a10979915bc6b34fafb5d23a152560c2e709fe",
                taxCode: "fbc045776db748bf9fe2753501bdf3abf04510db96b8449aaa330ad5e2316f3b2d3e7c91d9fc4c728fbbce6a03091986f50e",
                vatName: "531967ceba6e456b83e7764ebb4142932ee897860bfc4dd78b0392a1f8b7ca256ea091bca2434fbda48e9d3ee864e67d1e5e74d72b2144eabf9986176f03916cd8db1f280f30425da2d1d83041db35d99dddc74d40104aed8f52b8663f3a05b05204e93c1dbc453e87fd42b8366999e62ddba41c56694418a74c19f030d25d5",
                vatAddress: "28f94e20591f48458559882f33b8dcdd2ed51007bce3407ba8e28c8304f5470e70b52c16155c4c28aa22e03c47b87fab816682c37c0b4a4e81ab7bd434df4902eace79a5ab00445c81327068fa6c8728d6f3359d3ef94f42b3753c53deb4967ac934fb3ca1f744b9831aea975cc7b63c3f67917cc03348828af17c9fdfd1dd450e96bd35eec14dc89de1bc43493124ac654f367a4ebb4bf199626a69e296324da005aa9c290140209a175792333a099f4684d2f598dc45d8bfe352dbeb80344c764336afabc9486b8e94a393d1ae75c0d629f3f6387b4e67a23ab8889b6176d0194fecafb0e84c76809b66f0d83b6982201e95029a46412d8e14",
                erpCode: "be7313220af84815a55f2059574ed5bf04bd319292634246b8626d91d30fb8318fa5420af7ea49129fe938acee4199bd15ca",
                active: true,
                effectiveDate: new DateTime(2001, 8, 20),
                endDate: new DateTime(2017, 8, 9),
                isHO: true,
                latitude: "e36c236284b140c5bce6b6665b98982b36dab60ebc1e41cca493e0a408c4f163a6036cc218ca49b1ad08e08cc227ffa7d402376e5c214ede8baca050564d470be6f6f8422db042a0985cd492f1b4106cbd8ba55e2e57408eabce33de07c14186cb1a4f367b8f4dafbbf6789410ead36c5c325a30f8904b93bcaa96f3e0af143",
                longitude: "01681a58aabc49a6a7a80d8fa13f23cef2a6149cafbc40aca7f7ed288bb451d75db55a5bb7a84e3696b41c92e258a8e797771d48e7274d3ab7d01574fd185b3dd9658e7f1fe741c385fbfd75f43e51ee0ca06a8fc63b4f74a568b9f995ed7e9ca345fd0caf6a4156bad7ec7bf988a08f4fa79f66a3ee4e3b969d7529dbf56e2",
                contactName: "30746c957566433390f44ee7da91a8838dbfd86e6ebe4865b61445414b0a79dec2e8b1b3bacc4e0588214e7d14dd7237f10d82dbccab44d59e46e272346066b86091d6ba3d354dd49440521a2200b193c74802bea9af4c4290db765669d8036f9cc5fc1cde5e44ddaf8b9fc698692806f894aa45041345738e4984f0a007da4",
                contactPhone: "f6c492bd04944f0b8b86bd9987400bcb919d0f7d101c4169848bb8518fa3bbdee04670a575e442de9c136b3e433d2e8491e45e54351c4b4dbc78f8de52f9b906eee06f1fa1714c79a5e4e076245cc35ce9ac317f733b4f1fbf30899906011a259013f0af8f0b410dbf8d889ac2fcdf9997959a69eb4b450aa5b15722e34c4e8",
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