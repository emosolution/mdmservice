using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompaniesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICompaniesAppService _companiesAppService;
        private readonly IRepository<Company, Guid> _companyRepository;

        public CompaniesAppServiceTests()
        {
            _companiesAppService = GetRequiredService<ICompaniesAppService>();
            _companyRepository = GetRequiredService<IRepository<Company, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _companiesAppService.GetListAsync(new GetCompaniesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Company.Id == Guid.Parse("d45111bf-4879-49ce-9cc6-313f51f8b197")).ShouldBe(true);
            result.Items.Any(x => x.Company.Id == Guid.Parse("9d7230d0-7215-4dfb-b0e9-b9d2d726b278")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companiesAppService.GetAsync(Guid.Parse("d45111bf-4879-49ce-9cc6-313f51f8b197"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d45111bf-4879-49ce-9cc6-313f51f8b197"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyCreateDto
            {
                Code = "ee5e2fdf689443cc9e77",
                Name = "e45aad00e9824a318f27080f689107b1a3e926b992424ae69cc5fc8c162ab3317377f4b3774140ed966ccf7c28e58c868ad9",
                Street = "9472743e6d3a4036a565451dd4c615fbeb1fe3d1acab485",
                Address = "c884de37022240efa569d2db81a5864bc430ff1af9584f6588fa49113ce91880ec18dcdc281b40219683b8fb66dbf95417890a68f4a647e98e2863ea0e6d153388622c9308f44721a171c6661db9a6cdb94f5fa96ea444218a3cdad836c90168952772462fd748cf889c3d217f1b610d449e645881754a3e8f1790ccc83a4e8c2c11db0604f24871bed37464bb1b4b05b2c59540f2084265a91672016feb1770feb46da24ef445d2806babf5adc552c5004553a0682d470ca752bead8ba94968e6180b9b96264ccca68565428d4ba5162adeceebf8f343d09028f6549b9b68fb4bf70afb54464de09430147154a3fdbb1d7972c2401c4a8bbb67890c982f5af2deca19735d2640fab4cbf87d40db613f88cec1e016934dc89d5971a496fe065d26f1a904192e4638ad740063f24b336809eaedee9c304ec18ea366eb3843b298f3cd6cb79f7a4b0e870211d5ea3bfcaddb5dbb3bdb2e414b80028256b99263a487ecd117b28e45b7ba0d332e7dcffe7b392cd30d1e7b4dd0b2ed73004aadbbc679baddbc5a424cf3a610ebfefc582d4799a2b0cb301a42afb1729e4ce3363f432366c844a44f41bbb9d512af2dd317fb1afa6ace174a45d9a485ace62ece5abbae3d5c19be64443fa7082ea7e9f5d58de03abf45676c43c69fa8469e50a38c6cfe600454b7c84b32843fad25f48b780f5b273e28",
                Phone = "7924034e23204f3a9ec8",
                License = "aa822d65653e4e75a8d3893e26bb1e60e6ca8ce2db15423db4778c67257c55f3915b9c3ee6d543a58cdaa5580b74b516f573",
                TaxCode = "d55a22752412440cbac017b6169062b08131e9ab55b44da8816531c4503a92285c8379ca77cb488188e9343a78b53fbba8a5",
                VATName = "8728dbf7c89d4e74a171d38a3f9f4d4752a636c6321a4630abc6a99931e5e6f8375",
                VATAddress = "3bbb0e5d2c834302b067a17ec53884c43da01da443b44bfd8b24da087f2226471977c08470f444aead714cec0c8462ba4332c652eaec409fa1f859716cd75efc3bb2f94e86654a56bebf57b318aae19c163c72e6c0cc4c889e383cd9efcb41f297c1e7b840624d42822614d1bda8cbc0fffb02a120de481fb9a0b83be85568d1bf36d607dc8b4a56ad849e6020471200a3556aea36b84fffb0338c7bf49d44679cdd136c77dc46bcba820739db87625cc333533fdcb841b797c14a80855acaf61b67a67a2eb54360b052e6f17600bf3115bdaf7c18654b97a1bfb86cefff3c58ca40f4d17ba7446795b636bd055cd22737cb7fb72f014a6fa588",
                ERPCode = "7c76e112ee68464fb7333cad2a3fd0fd56178c8ac8004b7f93555ea5844d7451eabd6b975d5d48eabef11ecee022fa4021ab",
                Active = true,
                EffectiveDate = new DateTime(2016, 2, 22),
                EndDate = new DateTime(2007, 8, 1),
                IsHO = true,
                Latitude = "e79096a3",
                Longitude = "e3ff718a1c714723b9ea0f99dea82c770c09b7e584c14aaf91ef76eb081e24502058baa8a42248aaa0bcc411a340c3e19",
                ContactName = "f8b6dec28d5641af",
                ContactPhone = "c9948f2ef79b4a4eb2bf8928d3ffbce336bd42ee5ce646dda2a3d55a05711eda473e6e9000d342beba1efe"
            };

            // Act
            var serviceResult = await _companiesAppService.CreateAsync(input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ee5e2fdf689443cc9e77");
            result.Name.ShouldBe("e45aad00e9824a318f27080f689107b1a3e926b992424ae69cc5fc8c162ab3317377f4b3774140ed966ccf7c28e58c868ad9");
            result.Street.ShouldBe("9472743e6d3a4036a565451dd4c615fbeb1fe3d1acab485");
            result.Address.ShouldBe("c884de37022240efa569d2db81a5864bc430ff1af9584f6588fa49113ce91880ec18dcdc281b40219683b8fb66dbf95417890a68f4a647e98e2863ea0e6d153388622c9308f44721a171c6661db9a6cdb94f5fa96ea444218a3cdad836c90168952772462fd748cf889c3d217f1b610d449e645881754a3e8f1790ccc83a4e8c2c11db0604f24871bed37464bb1b4b05b2c59540f2084265a91672016feb1770feb46da24ef445d2806babf5adc552c5004553a0682d470ca752bead8ba94968e6180b9b96264ccca68565428d4ba5162adeceebf8f343d09028f6549b9b68fb4bf70afb54464de09430147154a3fdbb1d7972c2401c4a8bbb67890c982f5af2deca19735d2640fab4cbf87d40db613f88cec1e016934dc89d5971a496fe065d26f1a904192e4638ad740063f24b336809eaedee9c304ec18ea366eb3843b298f3cd6cb79f7a4b0e870211d5ea3bfcaddb5dbb3bdb2e414b80028256b99263a487ecd117b28e45b7ba0d332e7dcffe7b392cd30d1e7b4dd0b2ed73004aadbbc679baddbc5a424cf3a610ebfefc582d4799a2b0cb301a42afb1729e4ce3363f432366c844a44f41bbb9d512af2dd317fb1afa6ace174a45d9a485ace62ece5abbae3d5c19be64443fa7082ea7e9f5d58de03abf45676c43c69fa8469e50a38c6cfe600454b7c84b32843fad25f48b780f5b273e28");
            result.Phone.ShouldBe("7924034e23204f3a9ec8");
            result.License.ShouldBe("aa822d65653e4e75a8d3893e26bb1e60e6ca8ce2db15423db4778c67257c55f3915b9c3ee6d543a58cdaa5580b74b516f573");
            result.TaxCode.ShouldBe("d55a22752412440cbac017b6169062b08131e9ab55b44da8816531c4503a92285c8379ca77cb488188e9343a78b53fbba8a5");
            result.VATName.ShouldBe("8728dbf7c89d4e74a171d38a3f9f4d4752a636c6321a4630abc6a99931e5e6f8375");
            result.VATAddress.ShouldBe("3bbb0e5d2c834302b067a17ec53884c43da01da443b44bfd8b24da087f2226471977c08470f444aead714cec0c8462ba4332c652eaec409fa1f859716cd75efc3bb2f94e86654a56bebf57b318aae19c163c72e6c0cc4c889e383cd9efcb41f297c1e7b840624d42822614d1bda8cbc0fffb02a120de481fb9a0b83be85568d1bf36d607dc8b4a56ad849e6020471200a3556aea36b84fffb0338c7bf49d44679cdd136c77dc46bcba820739db87625cc333533fdcb841b797c14a80855acaf61b67a67a2eb54360b052e6f17600bf3115bdaf7c18654b97a1bfb86cefff3c58ca40f4d17ba7446795b636bd055cd22737cb7fb72f014a6fa588");
            result.ERPCode.ShouldBe("7c76e112ee68464fb7333cad2a3fd0fd56178c8ac8004b7f93555ea5844d7451eabd6b975d5d48eabef11ecee022fa4021ab");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2016, 2, 22));
            result.EndDate.ShouldBe(new DateTime(2007, 8, 1));
            result.IsHO.ShouldBe(true);
            result.Latitude.ShouldBe("e79096a3");
            result.Longitude.ShouldBe("e3ff718a1c714723b9ea0f99dea82c770c09b7e584c14aaf91ef76eb081e24502058baa8a42248aaa0bcc411a340c3e19");
            result.ContactName.ShouldBe("f8b6dec28d5641af");
            result.ContactPhone.ShouldBe("c9948f2ef79b4a4eb2bf8928d3ffbce336bd42ee5ce646dda2a3d55a05711eda473e6e9000d342beba1efe");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyUpdateDto()
            {
                Code = "bb205d4794534272b380",
                Name = "bd206268df604ec2a83184343713b983fc603807cc134a45826ed216c9ac6d812e80c959da804cb2b2d195f882a4bcc6fbfd",
                Street = "64f31cf43d114af8bd2f585b54910a05",
                Address = "b028941ef3384c1fbfd80a8768e736be5faa60bbf2be414b922a3b739c19356fbf56016e62b047feb414a44b617b0703f33d1955297f40faad7e53c7cb18f366d15640e08a044c9c9a74f381120924facd202f5088f248cd91bd734bac16a03ee5b88d8a0808424292f9de5d55f35b351334ab2b17ac4f47aa3e4c150cb353732e54f2eaba7c4f20b886c3e5aca4bd5ee7ff3e8bc6c243bc8fb7862ff2f9da6ca63a1be0fbbf4bb78c84edea74f4b7e5147037f8ad8c4437a49f488a6ffd974c5fff83b98cda4d4abc77adb3ba4124181ab7e52fe8164ce0b5f0fe0a7583a42db77ddf1c256e4ed88d0e569504d1012b6245b57b6ce14b85b0996cc7b6cdf9633bdb24875f734937839132c8b4e3690d26dd7b68478e4bdcb1ab785fe63a85cee2bf0fccf17b4a168d4201adf87bb2616861b0a0e5d94ec0aa3feb0f30482fb7a47c4991b8f54bd88dcd5818ee32b6e10b870f407e3c47b8a87c9d25878240bec1ad74b1d3c44d08a155eff283bd89a88b173be668d0479b9b063d0133c018661b76558d072e43898857b4460c7ebf08d6e808fbb7af4bfdbf1a783323e80555855e08bcdb264de1ac2fdff1697b28ab0e245725c0714a97865cf610d51d6d578f87848781b64afc997cd87466ef52fdd9142877dda94e6b8ac91b3cc160ab97d0c62205d0b04e088977e51f2f69f9030d0267a9",
                Phone = "4fb40cb48f6a490e8fee",
                License = "624add8200a8407fbeeaa24c11768b862db9803444ce49998408be31e1faf12992c4f0c857ac4dbf944f5b7729765568d85b",
                TaxCode = "e53f523fefca4a26a94f0496a8a5f551e8692c4ffa554f629fffd560c86e350259db9a2d203c41d09a89f6ce409a2cebae7d",
                VATName = "889eeae71228429481b8e93b2e394b692dacc7884d51494294536ed73956e504a4",
                VATAddress = "7ab1a3f1863b4ff99b3c4b8cdf46a0f7e14d9df6ba2d4eb08271a200e416a355b2ff9d487fc74aa1839fa7e502d5c2c6496f5694518b48edb2f32e6be3b431bf8396bf4c26484e62b75e600f36defdd0c46a0ed7a4b14d1cae8aeac883a8b17b33042d118d5747f4bce9b93d8bf1d787cdda36bb46704f54b4695c14fe0261d1c1967ff63e1e4b4cbc6fd1669dde405dbacdc8e1d92649aea342b0e8927c65600d5f81e0b39d4723958b2a9c3e38b52147827830e38e421dad6379225f80e25e21c47fda415341c990ce1046e8ca72722d69fd61c653483ca722063a82062a270df1039d4cfa4c2f83a0239c49c8d0660d88c205fb104c3ba07d",
                ERPCode = "c66ad237c4c14e1d9da8548c8d3230f9ab42095f90644f32a97655e55fac6247a1de8a5bccba4b2bb0eb83a14b8311d839be",
                Active = true,
                EffectiveDate = new DateTime(2008, 8, 12),
                EndDate = new DateTime(2003, 6, 8),
                IsHO = true,
                Latitude = "fb5a5c29952644ed904a7fb4",
                Longitude = "c956b1a9370e44029f39082055a0048f82b79e23309e4d75b15d1c576ac87cd",
                ContactName = "3b730f1d79934bb89",
                ContactPhone = "560cddf31e0b4ab788446b3b738e546ae31b7d2e2ff841e0b3e4710feff0470621f5ba70956c4fc8aba89f81a1ed8"
            };

            // Act
            var serviceResult = await _companiesAppService.UpdateAsync(Guid.Parse("d45111bf-4879-49ce-9cc6-313f51f8b197"), input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("bb205d4794534272b380");
            result.Name.ShouldBe("bd206268df604ec2a83184343713b983fc603807cc134a45826ed216c9ac6d812e80c959da804cb2b2d195f882a4bcc6fbfd");
            result.Street.ShouldBe("64f31cf43d114af8bd2f585b54910a05");
            result.Address.ShouldBe("b028941ef3384c1fbfd80a8768e736be5faa60bbf2be414b922a3b739c19356fbf56016e62b047feb414a44b617b0703f33d1955297f40faad7e53c7cb18f366d15640e08a044c9c9a74f381120924facd202f5088f248cd91bd734bac16a03ee5b88d8a0808424292f9de5d55f35b351334ab2b17ac4f47aa3e4c150cb353732e54f2eaba7c4f20b886c3e5aca4bd5ee7ff3e8bc6c243bc8fb7862ff2f9da6ca63a1be0fbbf4bb78c84edea74f4b7e5147037f8ad8c4437a49f488a6ffd974c5fff83b98cda4d4abc77adb3ba4124181ab7e52fe8164ce0b5f0fe0a7583a42db77ddf1c256e4ed88d0e569504d1012b6245b57b6ce14b85b0996cc7b6cdf9633bdb24875f734937839132c8b4e3690d26dd7b68478e4bdcb1ab785fe63a85cee2bf0fccf17b4a168d4201adf87bb2616861b0a0e5d94ec0aa3feb0f30482fb7a47c4991b8f54bd88dcd5818ee32b6e10b870f407e3c47b8a87c9d25878240bec1ad74b1d3c44d08a155eff283bd89a88b173be668d0479b9b063d0133c018661b76558d072e43898857b4460c7ebf08d6e808fbb7af4bfdbf1a783323e80555855e08bcdb264de1ac2fdff1697b28ab0e245725c0714a97865cf610d51d6d578f87848781b64afc997cd87466ef52fdd9142877dda94e6b8ac91b3cc160ab97d0c62205d0b04e088977e51f2f69f9030d0267a9");
            result.Phone.ShouldBe("4fb40cb48f6a490e8fee");
            result.License.ShouldBe("624add8200a8407fbeeaa24c11768b862db9803444ce49998408be31e1faf12992c4f0c857ac4dbf944f5b7729765568d85b");
            result.TaxCode.ShouldBe("e53f523fefca4a26a94f0496a8a5f551e8692c4ffa554f629fffd560c86e350259db9a2d203c41d09a89f6ce409a2cebae7d");
            result.VATName.ShouldBe("889eeae71228429481b8e93b2e394b692dacc7884d51494294536ed73956e504a4");
            result.VATAddress.ShouldBe("7ab1a3f1863b4ff99b3c4b8cdf46a0f7e14d9df6ba2d4eb08271a200e416a355b2ff9d487fc74aa1839fa7e502d5c2c6496f5694518b48edb2f32e6be3b431bf8396bf4c26484e62b75e600f36defdd0c46a0ed7a4b14d1cae8aeac883a8b17b33042d118d5747f4bce9b93d8bf1d787cdda36bb46704f54b4695c14fe0261d1c1967ff63e1e4b4cbc6fd1669dde405dbacdc8e1d92649aea342b0e8927c65600d5f81e0b39d4723958b2a9c3e38b52147827830e38e421dad6379225f80e25e21c47fda415341c990ce1046e8ca72722d69fd61c653483ca722063a82062a270df1039d4cfa4c2f83a0239c49c8d0660d88c205fb104c3ba07d");
            result.ERPCode.ShouldBe("c66ad237c4c14e1d9da8548c8d3230f9ab42095f90644f32a97655e55fac6247a1de8a5bccba4b2bb0eb83a14b8311d839be");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2008, 8, 12));
            result.EndDate.ShouldBe(new DateTime(2003, 6, 8));
            result.IsHO.ShouldBe(true);
            result.Latitude.ShouldBe("fb5a5c29952644ed904a7fb4");
            result.Longitude.ShouldBe("c956b1a9370e44029f39082055a0048f82b79e23309e4d75b15d1c576ac87cd");
            result.ContactName.ShouldBe("3b730f1d79934bb89");
            result.ContactPhone.ShouldBe("560cddf31e0b4ab788446b3b738e546ae31b7d2e2ff841e0b3e4710feff0470621f5ba70956c4fc8aba89f81a1ed8");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companiesAppService.DeleteAsync(Guid.Parse("d45111bf-4879-49ce-9cc6-313f51f8b197"));

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == Guid.Parse("d45111bf-4879-49ce-9cc6-313f51f8b197"));

            result.ShouldBeNull();
        }
    }
}