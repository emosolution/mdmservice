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
            result.Items.Any(x => x.Company.Id == Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")).ShouldBe(true);
            result.Items.Any(x => x.Company.Id == Guid.Parse("2164ab11-34c0-48d9-ba88-2230c4ef2b21")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companiesAppService.GetAsync(Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyCreateDto
            {
                Code = "a57a66352fd1431490b0",
                Name = "d9471e8745664f16a4d539a88dd590d88ca2e9be14184078a6ffdabc0b96b5aceabe249f7e3f48d78ff9ee22f4df54bfc5a8",
                Street = "5072f2be900e4f7ba44d2",
                Address = "2f74cd7a8c5f4bef909fa2552076d136e85bbc8ed8bb46c7a864480ee8c0ff6c557a818b5a6b415c8ed306064f70bfa2f2ff10cda0b149c0bd862c38ff8114687907e0d506244c3ca80ac18f297a28e2c93ea01f9eaf4140835943618b1522457ec45f5952ae4bde9e9247bc590df0a23f1deb31b17644d2a8995091e0e64bcb15e08a1306044f1f86d6fcc84d8c49cbc89335d4c32447cea2e06739308d4050bc4aa978aae4470c84c498e9c75950d612a3553d62a744a391bc7452283630904a9bf2717bf84d1d842007525594976856c56bab81b14d898292c5ba8d6edb76b631dd81e23d4f4c9c8903afabb59215b6967b731ddd410396c2f3bd13203788aaf6bf775f84428da3062d10bdafbce925bb0ef0076e4952b76d37d8a3c5d383fb2909333f8841aca3a5966328fac8b2bd51690b29c3437b9d0e1af4afd49a108af513ba45854119bcd46842f4cc4c55af43450a8c354833853871885a3581e27fd18c438cea4e22be93bcaad83ab5985120d0a71a9d4189b1b3a6a7f67e9bea8b5f98805b2446fd916775df56ba23d92ac4a45627cd4e91a43948ea29e9b715dde221e65d1a414083ccdfefb1db118347d6df8e28f446f09d93b369a95526999cbae198015349d0a0b9b3c23e614c2aba511d0f06fd4d019ac7fd90f78b161756ad99167b2a477fbcfff815664ad390822d7762",
                Phone = "17632f22c8934ecea70d",
                License = "58d58e18cfac43c3841e0cdb4d6ad5fbb3a4e17b9a6240568009b9b541e03983dd80598fcb1a44b3a07ee6ca88bfd7585758",
                TaxCode = "dc8b0539872949ab98556ef8ecaa437c3fbe47ed40eb43338d8ab0afce6edf33fe3211ddb81c472dbb45e782c592fe630bf7",
                VATName = "20a963237ddb4589a9308f2b14c5acb664a0fce896004b77930b2e9f21c7677",
                VATAddress = "ae2c6e89aac646978755f561638cfc33e7106b9bab0b4074a98a09ddd4182aade187bd2d2bd14910bc0bdccd1eff5",
                ERPCode = "316aad6af1d948279147e5b4293c19304cde32083e19458d967224447d4df8dd3807f2e8f67a4b59866bd7f144136ac52195",
                Active = true,
                EffectiveDate = new DateTime(2010, 4, 17),
                EndDate = new DateTime(2021, 6, 6),
                IsHO = true,
                Latitude = "3e59fbd0d0544538b5094ab28bfd79da31778ad998ec483ca56a837d3a8b81de3174af744b884",
                Longitude = "29e2a2916f7b47ca9fb3d066ecf02004fd50de037eb142cd8196f2d0d0740dc826f9fc32b89643b2826fff6c27bfc22",
                ContactName = "68bbd534fbaa49398eb4510f61494617ff3855bf525f4d69b6db7c6a",
                ContactPhone = "34e4ae2c2bf14ee481bed93c0e002ff44adf4a2127e44559ae55329471748d9414f14df390564d33ac5802a6934eebc"
            };

            // Act
            var serviceResult = await _companiesAppService.CreateAsync(input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("a57a66352fd1431490b0");
            result.Name.ShouldBe("d9471e8745664f16a4d539a88dd590d88ca2e9be14184078a6ffdabc0b96b5aceabe249f7e3f48d78ff9ee22f4df54bfc5a8");
            result.Street.ShouldBe("5072f2be900e4f7ba44d2");
            result.Address.ShouldBe("2f74cd7a8c5f4bef909fa2552076d136e85bbc8ed8bb46c7a864480ee8c0ff6c557a818b5a6b415c8ed306064f70bfa2f2ff10cda0b149c0bd862c38ff8114687907e0d506244c3ca80ac18f297a28e2c93ea01f9eaf4140835943618b1522457ec45f5952ae4bde9e9247bc590df0a23f1deb31b17644d2a8995091e0e64bcb15e08a1306044f1f86d6fcc84d8c49cbc89335d4c32447cea2e06739308d4050bc4aa978aae4470c84c498e9c75950d612a3553d62a744a391bc7452283630904a9bf2717bf84d1d842007525594976856c56bab81b14d898292c5ba8d6edb76b631dd81e23d4f4c9c8903afabb59215b6967b731ddd410396c2f3bd13203788aaf6bf775f84428da3062d10bdafbce925bb0ef0076e4952b76d37d8a3c5d383fb2909333f8841aca3a5966328fac8b2bd51690b29c3437b9d0e1af4afd49a108af513ba45854119bcd46842f4cc4c55af43450a8c354833853871885a3581e27fd18c438cea4e22be93bcaad83ab5985120d0a71a9d4189b1b3a6a7f67e9bea8b5f98805b2446fd916775df56ba23d92ac4a45627cd4e91a43948ea29e9b715dde221e65d1a414083ccdfefb1db118347d6df8e28f446f09d93b369a95526999cbae198015349d0a0b9b3c23e614c2aba511d0f06fd4d019ac7fd90f78b161756ad99167b2a477fbcfff815664ad390822d7762");
            result.Phone.ShouldBe("17632f22c8934ecea70d");
            result.License.ShouldBe("58d58e18cfac43c3841e0cdb4d6ad5fbb3a4e17b9a6240568009b9b541e03983dd80598fcb1a44b3a07ee6ca88bfd7585758");
            result.TaxCode.ShouldBe("dc8b0539872949ab98556ef8ecaa437c3fbe47ed40eb43338d8ab0afce6edf33fe3211ddb81c472dbb45e782c592fe630bf7");
            result.VATName.ShouldBe("20a963237ddb4589a9308f2b14c5acb664a0fce896004b77930b2e9f21c7677");
            result.VATAddress.ShouldBe("ae2c6e89aac646978755f561638cfc33e7106b9bab0b4074a98a09ddd4182aade187bd2d2bd14910bc0bdccd1eff5");
            result.ERPCode.ShouldBe("316aad6af1d948279147e5b4293c19304cde32083e19458d967224447d4df8dd3807f2e8f67a4b59866bd7f144136ac52195");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2010, 4, 17));
            result.EndDate.ShouldBe(new DateTime(2021, 6, 6));
            result.IsHO.ShouldBe(true);
            result.Latitude.ShouldBe("3e59fbd0d0544538b5094ab28bfd79da31778ad998ec483ca56a837d3a8b81de3174af744b884");
            result.Longitude.ShouldBe("29e2a2916f7b47ca9fb3d066ecf02004fd50de037eb142cd8196f2d0d0740dc826f9fc32b89643b2826fff6c27bfc22");
            result.ContactName.ShouldBe("68bbd534fbaa49398eb4510f61494617ff3855bf525f4d69b6db7c6a");
            result.ContactPhone.ShouldBe("34e4ae2c2bf14ee481bed93c0e002ff44adf4a2127e44559ae55329471748d9414f14df390564d33ac5802a6934eebc");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyUpdateDto()
            {
                Code = "fda80114c0cc4806a345",
                Name = "88a4efb5598f4f569f0a2e6f0903f6d50cd3e4b1f314407b962beb16e5a967085b1e2939060d4f838aae9ba4d79b0091e4c4",
                Street = "7eaffb81cd0e437a99abfebea3958e9d1dd11d8a1e5c4",
                Address = "001ba38ab4314c36a82dc8b7acb1c7e485133ed5a374482bb5e7478b2fd29b563cb3a8cc6d4b41cca6a4b5a5aeb85164cd16f28c3e8042c6b4d4e8bf9659b5d300cd178debae4dc980063370c035f634a27e42922fbd4952ada07e1299b595b1acc83467cf0b405dabd480233bbd218465264f177f2547dc810f82c37b6fe6d9851ef44bcc5348dbab231f5f0b815a5a76b490f76b054fdcabd71644fa90826e01e660e9ea0c4c94811afb91f001c10e3239f7f3c8ec4547827490f1ad80dfd9aa22c493f93446d19cc581f398ec5c519570ee158dca45c78a4005382fe997a39b0bdeef55e54f43aede999e68af63a8d5560df424114fc09ad0b6764934bf4fe7dbadc848864c4aa5375d63616052dbfbc85d93073e4f2bbab7067d01c8c753114ad70dfa9e46bfa9f555b7c08e787381b34aad14244423a6eb9cc455ad619b1fdbdd5155f942098354ddbc9d3e19c94bf773b762674a88a1f956b5418acfdba51e623dd54e4380aae9d5e5b449ebd039cf5b7b6fce457daa8618bbbafa398bce08b25472454a30a0067711f46f7a5528af8b21ff6c4b7296df8943d0c23f54d79dd9b11a3945deab96da9dafb99c028e21569935244c54b853d3d734afd4f4eaf396d00d83433687b72e241bca23f455099d25aa2348a2b8a866231436b37c509b569fef384be08d928f86badb5d103798baa7",
                Phone = "b055946f0f294a19b13b",
                License = "dd8a5ae047e8420b8a86b29dcf9f5578bb156e6e74be42b0b44a9246ef38601d938be2a8b3564d4cb791282f91903508abcd",
                TaxCode = "789ee2ab8e3f41229b01c259aa5f40b66777118813fc4f63a1cd733aa3feae4c415cc2187a234eab999e9743ec034e881487",
                VATName = "41e295f44bc0429596d0db6bf4d85",
                VATAddress = "e5547387eb83477f91bb6daba068910193e3a7936cb54bc49817c2e",
                ERPCode = "74b6e5675a4546dda837dce1b0b6ad56f6e5a49b1b8f4cafa80eb231ae5291f766ecfe79d25a41ac98411fc6bfee58930b33",
                Active = true,
                EffectiveDate = new DateTime(2013, 3, 21),
                EndDate = new DateTime(2001, 9, 2),
                IsHO = true,
                Latitude = "5c606e37a7bc4e46ac967ad51a27c69b6b76392da3ce4acf96711b4512dc",
                Longitude = "066b41280d8b421d94f8af15281f9ca292e0b57510694fc7ae5",
                ContactName = "7771d52fc8734cc18ab76654161ba09a20ecc6fbd4e24b93af0ef39ee5a1ed4124",
                ContactPhone = "ed3b048f3e004f5ba"
            };

            // Act
            var serviceResult = await _companiesAppService.UpdateAsync(Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"), input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("fda80114c0cc4806a345");
            result.Name.ShouldBe("88a4efb5598f4f569f0a2e6f0903f6d50cd3e4b1f314407b962beb16e5a967085b1e2939060d4f838aae9ba4d79b0091e4c4");
            result.Street.ShouldBe("7eaffb81cd0e437a99abfebea3958e9d1dd11d8a1e5c4");
            result.Address.ShouldBe("001ba38ab4314c36a82dc8b7acb1c7e485133ed5a374482bb5e7478b2fd29b563cb3a8cc6d4b41cca6a4b5a5aeb85164cd16f28c3e8042c6b4d4e8bf9659b5d300cd178debae4dc980063370c035f634a27e42922fbd4952ada07e1299b595b1acc83467cf0b405dabd480233bbd218465264f177f2547dc810f82c37b6fe6d9851ef44bcc5348dbab231f5f0b815a5a76b490f76b054fdcabd71644fa90826e01e660e9ea0c4c94811afb91f001c10e3239f7f3c8ec4547827490f1ad80dfd9aa22c493f93446d19cc581f398ec5c519570ee158dca45c78a4005382fe997a39b0bdeef55e54f43aede999e68af63a8d5560df424114fc09ad0b6764934bf4fe7dbadc848864c4aa5375d63616052dbfbc85d93073e4f2bbab7067d01c8c753114ad70dfa9e46bfa9f555b7c08e787381b34aad14244423a6eb9cc455ad619b1fdbdd5155f942098354ddbc9d3e19c94bf773b762674a88a1f956b5418acfdba51e623dd54e4380aae9d5e5b449ebd039cf5b7b6fce457daa8618bbbafa398bce08b25472454a30a0067711f46f7a5528af8b21ff6c4b7296df8943d0c23f54d79dd9b11a3945deab96da9dafb99c028e21569935244c54b853d3d734afd4f4eaf396d00d83433687b72e241bca23f455099d25aa2348a2b8a866231436b37c509b569fef384be08d928f86badb5d103798baa7");
            result.Phone.ShouldBe("b055946f0f294a19b13b");
            result.License.ShouldBe("dd8a5ae047e8420b8a86b29dcf9f5578bb156e6e74be42b0b44a9246ef38601d938be2a8b3564d4cb791282f91903508abcd");
            result.TaxCode.ShouldBe("789ee2ab8e3f41229b01c259aa5f40b66777118813fc4f63a1cd733aa3feae4c415cc2187a234eab999e9743ec034e881487");
            result.VATName.ShouldBe("41e295f44bc0429596d0db6bf4d85");
            result.VATAddress.ShouldBe("e5547387eb83477f91bb6daba068910193e3a7936cb54bc49817c2e");
            result.ERPCode.ShouldBe("74b6e5675a4546dda837dce1b0b6ad56f6e5a49b1b8f4cafa80eb231ae5291f766ecfe79d25a41ac98411fc6bfee58930b33");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2013, 3, 21));
            result.EndDate.ShouldBe(new DateTime(2001, 9, 2));
            result.IsHO.ShouldBe(true);
            result.Latitude.ShouldBe("5c606e37a7bc4e46ac967ad51a27c69b6b76392da3ce4acf96711b4512dc");
            result.Longitude.ShouldBe("066b41280d8b421d94f8af15281f9ca292e0b57510694fc7ae5");
            result.ContactName.ShouldBe("7771d52fc8734cc18ab76654161ba09a20ecc6fbd4e24b93af0ef39ee5a1ed4124");
            result.ContactPhone.ShouldBe("ed3b048f3e004f5ba");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companiesAppService.DeleteAsync(Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"));

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"));

            result.ShouldBeNull();
        }
    }
}