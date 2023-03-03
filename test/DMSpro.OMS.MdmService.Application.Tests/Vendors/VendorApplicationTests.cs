using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IVendorsAppService _vendorsAppService;
        private readonly IRepository<Vendor, Guid> _vendorRepository;

        public VendorsAppServiceTests()
        {
            _vendorsAppService = GetRequiredService<IVendorsAppService>();
            _vendorRepository = GetRequiredService<IRepository<Vendor, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _vendorsAppService.GetListAsync(new GetVendorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Vendor.Id == Guid.Parse("1f7f0feb-4286-440d-86d0-3e8dba6e75fa")).ShouldBe(true);
            result.Items.Any(x => x.Vendor.Id == Guid.Parse("2341d664-3a18-4717-96e4-fc7326ea3bac")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vendorsAppService.GetAsync(Guid.Parse("1f7f0feb-4286-440d-86d0-3e8dba6e75fa"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1f7f0feb-4286-440d-86d0-3e8dba6e75fa"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VendorCreateDto
            {
                Code = "2ca7392e5f944a199ce4",
                Name = "449bca56583846e89e5d1842cd581f21b132911de5624cf6b2354a7fccdc37d1549b73b7ea2d487391ff570918e095a48b213fd278664749aebb049347b1512951266efe4fe8493890af5d75ec51bd8fbb4c47b308c94f5aaf028d004961b4baa3bf70ca",
                ShortName = "b324eff50db34d4e9c284beaea723620a557bc37a3ba47e2b3641c107154e669d4046747f7d9453ab50cef17bdc6b5ee502f26f4e13947a4a2be3e62c10af0c9cae9698039b14d87b277ec22cd291ae323216d7ba6dc41ecb22d637e1951d7125d282bde",
                Phone1 = "ef998c84f480424ba89fe43b6d43e3f8fa09262e36184f948b3d5a752fdb652ff6d5814ba84d47c7b37a148576ebb0e3baf13e600b494fcfaf5bc96829ead033d6560c94b1824053bb038030d2ac8633c1bc7bb6ca6449efbaaa4182c278f4e71720ab558ab94a69a76cfa503dee496873ee1399982e4a2b904fad318cb8dd1",
                Phone2 = "23e922ad106848e9a3e04a9e2db3892ac28668ad6adf49b5b4ab26d9a8fa6a22f505775fada6436bbb46929b2734ecbf1adf64fd5a5f441a934b5351fe0cfc017d61c8e56e774cdbb82bcde1a67185dcb145706f6d7c493a8cf0e413954d314c22a23727d71044a988c91b0b21da79950012e31719f94344b26a145020214d0",
                ERPCode = "d8dad226e7bc47bbbe7e1add9b633f8cbffbbe3352e74f8eac4ea8417deaec17e56f0a89d50d423ebcd59636d60bdf2ce6a7eb24217049dc9da7da7b27d360287e40333af08e4e09aa858d6cb8379dca72916434dc97405bbe6540b03dc26db825cf0a0cc1a74bac8e244cc57303e5c32604bf221f89445aa2c2f950cc97914",
                Active = true,
                EndDate = new DateTime(2000, 11, 8),
                Street = "c106b9b72f9447f491c365cec4b5729484afed826a924d7fa79dff45b913672a51317be8c9ed4f06894850f74fec044a2332a6a3d32c4b4cbb30e17f0ef811f68de28a58a4f24dd0951d6ba516b4f102922302bf62344e3d8d2181e313266ff90abd862b858d49e39563db390f56f45183b56bd0becc4173b597e8534f70580",
                Address = "007159f482d44c47a6eb67324bdb08c336e7ac3af3f548cda40a7a97b1ce89330cc39b671de84e138284fa05851000c71c2e305b787841018a2829e65c708b1f4cbe73a0dba54214af06497ca0bb3c01593e4ba0ed3747c3bf22c29a101bee0040ad19ab639546ce94bf4ee3fd328a2dd05823de861d47ebb8ad47ec87375662e8b1d5801e5e466aa84a86c9f1ffe133d54910318f2f4c439d0e55be0f30763611ec2f6b1bae40d5bc0c91700ffb1fc2f570fa9ebd3d480d9ff2047849e1b962036e0fc9c4014a8889ea5421c5c882c8d204a830100542808d66c01f777dc72fb9b750f9323e41aca06316d5e490d1a64c7389d9876d44309381",
                Latitude = "0c7bec8b98874e35b08d57405521129c506ecbf407c94f549fb02a48b753a04efec66a9e27b143e9bac5c662cb614c906e628b2455ca4f7ab5386ad3860c593d61df4c6717904bd4809591342509adfbae62233be5a14050aff40d31349cef70060abe62f541434597599bba0798351954878837ec3b42c68643ca540f57f79",
                Longitude = "c90a40cab9bc47d599c1acd706e810f0218d1dfd51fb439383a0dfadb76fe9baa2c91bf41e5848f6b644d8005f8ab1762146646027094e4fbaab27c15e0ca7bd389070efc1e14d55b7895712466a93fb77acdc4639d14ceca305db764dcfabaf59576b2ceb2746aa84a061fd55021fc0196d342f779a48de99dc990e77f2f0c",
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),

            };

            // Act
            var serviceResult = await _vendorsAppService.CreateAsync(input);

            // Assert
            var result = await _vendorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("2ca7392e5f944a199ce4");
            result.Name.ShouldBe("449bca56583846e89e5d1842cd581f21b132911de5624cf6b2354a7fccdc37d1549b73b7ea2d487391ff570918e095a48b213fd278664749aebb049347b1512951266efe4fe8493890af5d75ec51bd8fbb4c47b308c94f5aaf028d004961b4baa3bf70ca");
            result.ShortName.ShouldBe("b324eff50db34d4e9c284beaea723620a557bc37a3ba47e2b3641c107154e669d4046747f7d9453ab50cef17bdc6b5ee502f26f4e13947a4a2be3e62c10af0c9cae9698039b14d87b277ec22cd291ae323216d7ba6dc41ecb22d637e1951d7125d282bde");
            result.Phone1.ShouldBe("ef998c84f480424ba89fe43b6d43e3f8fa09262e36184f948b3d5a752fdb652ff6d5814ba84d47c7b37a148576ebb0e3baf13e600b494fcfaf5bc96829ead033d6560c94b1824053bb038030d2ac8633c1bc7bb6ca6449efbaaa4182c278f4e71720ab558ab94a69a76cfa503dee496873ee1399982e4a2b904fad318cb8dd1");
            result.Phone2.ShouldBe("23e922ad106848e9a3e04a9e2db3892ac28668ad6adf49b5b4ab26d9a8fa6a22f505775fada6436bbb46929b2734ecbf1adf64fd5a5f441a934b5351fe0cfc017d61c8e56e774cdbb82bcde1a67185dcb145706f6d7c493a8cf0e413954d314c22a23727d71044a988c91b0b21da79950012e31719f94344b26a145020214d0");
            result.ERPCode.ShouldBe("d8dad226e7bc47bbbe7e1add9b633f8cbffbbe3352e74f8eac4ea8417deaec17e56f0a89d50d423ebcd59636d60bdf2ce6a7eb24217049dc9da7da7b27d360287e40333af08e4e09aa858d6cb8379dca72916434dc97405bbe6540b03dc26db825cf0a0cc1a74bac8e244cc57303e5c32604bf221f89445aa2c2f950cc97914");
            result.Active.ShouldBe(true);
            result.EndDate.ShouldBe(new DateTime(2000, 11, 8));
            result.Street.ShouldBe("c106b9b72f9447f491c365cec4b5729484afed826a924d7fa79dff45b913672a51317be8c9ed4f06894850f74fec044a2332a6a3d32c4b4cbb30e17f0ef811f68de28a58a4f24dd0951d6ba516b4f102922302bf62344e3d8d2181e313266ff90abd862b858d49e39563db390f56f45183b56bd0becc4173b597e8534f70580");
            result.Address.ShouldBe("007159f482d44c47a6eb67324bdb08c336e7ac3af3f548cda40a7a97b1ce89330cc39b671de84e138284fa05851000c71c2e305b787841018a2829e65c708b1f4cbe73a0dba54214af06497ca0bb3c01593e4ba0ed3747c3bf22c29a101bee0040ad19ab639546ce94bf4ee3fd328a2dd05823de861d47ebb8ad47ec87375662e8b1d5801e5e466aa84a86c9f1ffe133d54910318f2f4c439d0e55be0f30763611ec2f6b1bae40d5bc0c91700ffb1fc2f570fa9ebd3d480d9ff2047849e1b962036e0fc9c4014a8889ea5421c5c882c8d204a830100542808d66c01f777dc72fb9b750f9323e41aca06316d5e490d1a64c7389d9876d44309381");
            result.Latitude.ShouldBe("0c7bec8b98874e35b08d57405521129c506ecbf407c94f549fb02a48b753a04efec66a9e27b143e9bac5c662cb614c906e628b2455ca4f7ab5386ad3860c593d61df4c6717904bd4809591342509adfbae62233be5a14050aff40d31349cef70060abe62f541434597599bba0798351954878837ec3b42c68643ca540f57f79");
            result.Longitude.ShouldBe("c90a40cab9bc47d599c1acd706e810f0218d1dfd51fb439383a0dfadb76fe9baa2c91bf41e5848f6b644d8005f8ab1762146646027094e4fbaab27c15e0ca7bd389070efc1e14d55b7895712466a93fb77acdc4639d14ceca305db764dcfabaf59576b2ceb2746aa84a061fd55021fc0196d342f779a48de99dc990e77f2f0c");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VendorUpdateDto()
            {
                Code = "ce70c87e2f7a4aafbe5e",
                Name = "0855ecd5e1ae46cab5131f820db6fae1c57c39c4387d450c9c9e75041da581efa29634d185074eb692123f4a5564b5ee7a3514198ded44a89e109866e608a2c371b06342e9b049c9bbeb82d5bea0443adda253bfe297432fad852a5426fa760d9b92e855",
                ShortName = "294cd32ab82c4252a1daa6567377168b223473bef43d4dff851a267a1e818057d7defbbc3a3c40b4b9d26b63f26025d82666f6a2f7b245de9254a8058358c20f59ff53031ede4ceb9f68b4e2ef53929ce557fca3969c4bd2a3cc0b8e15b381e1302dd70d",
                Phone1 = "3f9a687e5b244b0abdca3b791272aff7447bf281369241a0a845bd0e1f63269586961967511040c1a81716df1ad56a69b818078c0c5d4dc788c46f38860d20e4869afc3d48fb4e41bb2e4a85373e432bde02ce8be3f6418881ab09ba89aa3871a6df42e6b85a4be8aa2266b4ff9a8e6e0ab7df3329e748a192a09271528bf31",
                Phone2 = "01b80aec25744c458b2e17d4290014ec29c876d694af434e827b2bb5cc45081149cf438d9ace44aeb7c86423fc260ee07266d721e4e847788ed673cb904027a86bcd84bdd3194311b23a5312e77955b65ba0082c7e444a5b9357f975d3b04bcae565c59c9ba64a788baf693bb6bf4ec370d84292174f480fa848eb318977c9c",
                ERPCode = "dc3b77ce11fc4065a13394322ed5ac0b06ea64c33b774a1d89985b9d840a121376c651be27da4c60be5cad50383143041162deec33564f5ebf2db67d1287832618050fa7dc0246888f772b49770b0528fa0b80ef1e2c4a5496f225f63f10f197e06c3c88228c4bfea8a67aa497b68e1ee5a200634d404f0c9c1091868d8e006",
                Active = true,
                EndDate = new DateTime(2011, 7, 27),
                Street = "39820d80df0f4087bffc40015b9fdd82827e903be5c842d18b420c84869c68a644a0b054906745a19be28c11c8405b30137ba09e97524106ad3c37c83be1887094bf5558c8d945608290f437bd01e8895d5f110eb1c14b5d9ae4caa482d324e86818ff69acd7442d915670b1047ca2abe875818fa3ee4dc38ecbd42c69d2151",
                Address = "dfc3d648204749668e74ae8a13525a18b292429447be4cd08dce53cd4ff6e7a9c9c68d7b725a4baa9ed1708ebb08847f1235ec03f022492f9e23843d02b7677531ed9014d06a4d92b21b62a00e75f455cd81987c94a34275b2fed43b7ab90163aa07e5b8ecc748e0ad3464b2709b10609090432b3aa94d5fa678994dfc865548a2b9bef9b43d455699ee8b2ff4abd5ab19fed4b216234388a2f69e6fef1104f6c049ab1ad9a741b09f9c5ec28b9ddedd523d51da240f47e991cdf512333a093fa9c69a4282bf475a9ba88f59a67fb9e17a6a4e67d12e4810831cdd44015af6a04bae4175587742459208a1c3d5dd1df19ad69679a2c44f0d816a",
                Latitude = "20b5813f064b480b8a1ba5559f3eacc4bfabe56781444960b6d24f7fc12974465f3d0f172cd9450f90f9cd17063fb86ccd52312aef074bf88fe35da6df24f0764a09ce929c76403a8b8f3b629c8a4b17c69ffdcaa4d84784804c28283f98e4d15694ad87dfa3452e8ff7cb65f5e319d14a8e86308c2d4d0b8e8457b808f0ff0",
                Longitude = "4e30fe3541394c33bdf1fac579856e3de368334195df47b4ad07612d71c1bd2b2cb002def0d84e1cba7392bee127d1a7925563f07e0046e4ae166b384c785616e737d46489c345608907cc9b551d357c3f5a1919239d42718a701ada1ec023ff9c30e1c3b3624d5ba8e34d96671f016a1c83a0a8167342679b8a526b1116e09",
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),

            };

            // Act
            var serviceResult = await _vendorsAppService.UpdateAsync(Guid.Parse("1f7f0feb-4286-440d-86d0-3e8dba6e75fa"), input);

            // Assert
            var result = await _vendorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ce70c87e2f7a4aafbe5e");
            result.Name.ShouldBe("0855ecd5e1ae46cab5131f820db6fae1c57c39c4387d450c9c9e75041da581efa29634d185074eb692123f4a5564b5ee7a3514198ded44a89e109866e608a2c371b06342e9b049c9bbeb82d5bea0443adda253bfe297432fad852a5426fa760d9b92e855");
            result.ShortName.ShouldBe("294cd32ab82c4252a1daa6567377168b223473bef43d4dff851a267a1e818057d7defbbc3a3c40b4b9d26b63f26025d82666f6a2f7b245de9254a8058358c20f59ff53031ede4ceb9f68b4e2ef53929ce557fca3969c4bd2a3cc0b8e15b381e1302dd70d");
            result.Phone1.ShouldBe("3f9a687e5b244b0abdca3b791272aff7447bf281369241a0a845bd0e1f63269586961967511040c1a81716df1ad56a69b818078c0c5d4dc788c46f38860d20e4869afc3d48fb4e41bb2e4a85373e432bde02ce8be3f6418881ab09ba89aa3871a6df42e6b85a4be8aa2266b4ff9a8e6e0ab7df3329e748a192a09271528bf31");
            result.Phone2.ShouldBe("01b80aec25744c458b2e17d4290014ec29c876d694af434e827b2bb5cc45081149cf438d9ace44aeb7c86423fc260ee07266d721e4e847788ed673cb904027a86bcd84bdd3194311b23a5312e77955b65ba0082c7e444a5b9357f975d3b04bcae565c59c9ba64a788baf693bb6bf4ec370d84292174f480fa848eb318977c9c");
            result.ERPCode.ShouldBe("dc3b77ce11fc4065a13394322ed5ac0b06ea64c33b774a1d89985b9d840a121376c651be27da4c60be5cad50383143041162deec33564f5ebf2db67d1287832618050fa7dc0246888f772b49770b0528fa0b80ef1e2c4a5496f225f63f10f197e06c3c88228c4bfea8a67aa497b68e1ee5a200634d404f0c9c1091868d8e006");
            result.Active.ShouldBe(true);
            result.EndDate.ShouldBe(new DateTime(2011, 7, 27));
            result.Street.ShouldBe("39820d80df0f4087bffc40015b9fdd82827e903be5c842d18b420c84869c68a644a0b054906745a19be28c11c8405b30137ba09e97524106ad3c37c83be1887094bf5558c8d945608290f437bd01e8895d5f110eb1c14b5d9ae4caa482d324e86818ff69acd7442d915670b1047ca2abe875818fa3ee4dc38ecbd42c69d2151");
            result.Address.ShouldBe("dfc3d648204749668e74ae8a13525a18b292429447be4cd08dce53cd4ff6e7a9c9c68d7b725a4baa9ed1708ebb08847f1235ec03f022492f9e23843d02b7677531ed9014d06a4d92b21b62a00e75f455cd81987c94a34275b2fed43b7ab90163aa07e5b8ecc748e0ad3464b2709b10609090432b3aa94d5fa678994dfc865548a2b9bef9b43d455699ee8b2ff4abd5ab19fed4b216234388a2f69e6fef1104f6c049ab1ad9a741b09f9c5ec28b9ddedd523d51da240f47e991cdf512333a093fa9c69a4282bf475a9ba88f59a67fb9e17a6a4e67d12e4810831cdd44015af6a04bae4175587742459208a1c3d5dd1df19ad69679a2c44f0d816a");
            result.Latitude.ShouldBe("20b5813f064b480b8a1ba5559f3eacc4bfabe56781444960b6d24f7fc12974465f3d0f172cd9450f90f9cd17063fb86ccd52312aef074bf88fe35da6df24f0764a09ce929c76403a8b8f3b629c8a4b17c69ffdcaa4d84784804c28283f98e4d15694ad87dfa3452e8ff7cb65f5e319d14a8e86308c2d4d0b8e8457b808f0ff0");
            result.Longitude.ShouldBe("4e30fe3541394c33bdf1fac579856e3de368334195df47b4ad07612d71c1bd2b2cb002def0d84e1cba7392bee127d1a7925563f07e0046e4ae166b384c785616e737d46489c345608907cc9b551d357c3f5a1919239d42718a701ada1ec023ff9c30e1c3b3624d5ba8e34d96671f016a1c83a0a8167342679b8a526b1116e09");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vendorsAppService.DeleteAsync(Guid.Parse("1f7f0feb-4286-440d-86d0-3e8dba6e75fa"));

            // Assert
            var result = await _vendorRepository.FindAsync(c => c.Id == Guid.Parse("1f7f0feb-4286-440d-86d0-3e8dba6e75fa"));

            result.ShouldBeNull();
        }
    }
}