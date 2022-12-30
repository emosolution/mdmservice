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
            result.Items.Any(x => x.Vendor.Id == Guid.Parse("a626f6bb-483a-42fe-aa9e-177a7202e7b5")).ShouldBe(true);
            result.Items.Any(x => x.Vendor.Id == Guid.Parse("fc577795-ffb4-4140-b748-b66d210682c2")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vendorsAppService.GetAsync(Guid.Parse("a626f6bb-483a-42fe-aa9e-177a7202e7b5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a626f6bb-483a-42fe-aa9e-177a7202e7b5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VendorCreateDto
            {
                Code = "71e18997f39040e78778",
                Name = "785cc77a76c84ff4bf3db4221f663e9d27af74d434a945d4b97f84c8548694f02ecaa75825e84f0a9a0ef7fd31ab0b870faba2c834b94abca3e8dba9a4b19f1a7fc4ac01b84346f88d6a2c5821b3da5539c3a412029b4defae1a767ecbfa094ee3034504",
                ShortName = "08e675daa8254865b063bf5398b2ab9c66627ec2c2a14337a4db0b86b20ee29aa88e7309ed9d421d99074b163a710a5b654ca987dc954798829c19559cc075d184355fb94b9e41ab9d4a4fde426ef7d518d1f14b61584da6b8e43677338d1fb0a035cefc",
                Phone1 = "ad1bf2be9a91401fb3a98c5fe2028e8791767050c2e44054a92d0589ca8306cf7269bc49adf14efe975417813fd76",
                Phone2 = "8a6a6fa5c1754dd6a2534fee80004a491e5ec1544011444",
                ERPCode = "c79ec02c1ec049f28acc64a3697f57",
                Active = true,
                EndDate = new DateTime(2004, 6, 23),
                WarehouseId = Guid.Parse("b7407754-f3ae-4f9a-b0ed-98374fc94eef"),
                Street = "c228bfa4fa344438b067a0bfeca126c5ca8647ed7e524ef7905e1680b62a82fa7a6649d0f6bd48b",
                Address = "a51bbbf3b9c04237b4cf9b11d03ad911830ff638a6cd445d83f51614c933015d18a237f929a1",
                Latitude = "1a614d93f55a4891b",
                Longitude = "bb2b5f69d2c44c11b86d0de101a12f5d1cd",
                LinkedCompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),

            };

            // Act
            var serviceResult = await _vendorsAppService.CreateAsync(input);

            // Assert
            var result = await _vendorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("71e18997f39040e78778");
            result.Name.ShouldBe("785cc77a76c84ff4bf3db4221f663e9d27af74d434a945d4b97f84c8548694f02ecaa75825e84f0a9a0ef7fd31ab0b870faba2c834b94abca3e8dba9a4b19f1a7fc4ac01b84346f88d6a2c5821b3da5539c3a412029b4defae1a767ecbfa094ee3034504");
            result.ShortName.ShouldBe("08e675daa8254865b063bf5398b2ab9c66627ec2c2a14337a4db0b86b20ee29aa88e7309ed9d421d99074b163a710a5b654ca987dc954798829c19559cc075d184355fb94b9e41ab9d4a4fde426ef7d518d1f14b61584da6b8e43677338d1fb0a035cefc");
            result.Phone1.ShouldBe("ad1bf2be9a91401fb3a98c5fe2028e8791767050c2e44054a92d0589ca8306cf7269bc49adf14efe975417813fd76");
            result.Phone2.ShouldBe("8a6a6fa5c1754dd6a2534fee80004a491e5ec1544011444");
            result.ERPCode.ShouldBe("c79ec02c1ec049f28acc64a3697f57");
            result.Active.ShouldBe(true);
            result.EndDate.ShouldBe(new DateTime(2004, 6, 23));
            result.WarehouseId.ShouldBe(Guid.Parse("b7407754-f3ae-4f9a-b0ed-98374fc94eef"));
            result.Street.ShouldBe("c228bfa4fa344438b067a0bfeca126c5ca8647ed7e524ef7905e1680b62a82fa7a6649d0f6bd48b");
            result.Address.ShouldBe("a51bbbf3b9c04237b4cf9b11d03ad911830ff638a6cd445d83f51614c933015d18a237f929a1");
            result.Latitude.ShouldBe("1a614d93f55a4891b");
            result.Longitude.ShouldBe("bb2b5f69d2c44c11b86d0de101a12f5d1cd");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VendorUpdateDto()
            {
                Code = "5648638458924b069615",
                Name = "eb3be5d8482344f482d07484771eab07ba57725ecf0149b7a73038a282b7f166a5fe5a0a28b24342b58036ec82733854e400ccabbd64431d9935f390ba9cbc91c59d99ce5d254b4c8d42dc7e9ad2b05b36bf56efa0764ac8bc7a35dd18905e045187592a",
                ShortName = "89de1ef6cee44eee8cb2c84e7782787982ee67543b93434885ac80e493b419c773d045c0969f4d9fb3c207820a8787b66648ef1fa5a742199108a25093573ebfe9cb5071a29f444ab83f0de930a407ba867b916b63634e19be646019345554ebe0503cca",
                Phone1 = "e6e061a8f69a47a48db19ac3b5f8437a9273d48f93ca4d76bbc25da83f3",
                Phone2 = "3145b1d64073438da8e5d2f9a56ca374b346bc8d1d394980bb35eec21b0bf3ad4fba83776f804ff",
                ERPCode = "d1adb80a688b43dab69bf9e3e7fffc142461cc460b2a4e3b80f64f5bd839c2f149faa5c",
                Active = true,
                EndDate = new DateTime(2004, 5, 14),
                WarehouseId = Guid.Parse("6147f92e-977c-4690-bfd9-e9d2ca319acf"),
                Street = "1aa6201660644741b8ad3da40800fc55e93df2878b3448ff929318d764e44bda1ce74e1b02cc4f",
                Address = "7fd88eaba6cf46429cf9c69fa8298f04f3",
                Latitude = "b4e8004f83174b33b0e419e0a6dca777a87fdb1eddf44874b914e503eee9052b9682b4447a2b47118344",
                Longitude = "c40b531c09c8425e97d3881508ebc7ad2280e402c5fc",
                LinkedCompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),

            };

            // Act
            var serviceResult = await _vendorsAppService.UpdateAsync(Guid.Parse("a626f6bb-483a-42fe-aa9e-177a7202e7b5"), input);

            // Assert
            var result = await _vendorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("5648638458924b069615");
            result.Name.ShouldBe("eb3be5d8482344f482d07484771eab07ba57725ecf0149b7a73038a282b7f166a5fe5a0a28b24342b58036ec82733854e400ccabbd64431d9935f390ba9cbc91c59d99ce5d254b4c8d42dc7e9ad2b05b36bf56efa0764ac8bc7a35dd18905e045187592a");
            result.ShortName.ShouldBe("89de1ef6cee44eee8cb2c84e7782787982ee67543b93434885ac80e493b419c773d045c0969f4d9fb3c207820a8787b66648ef1fa5a742199108a25093573ebfe9cb5071a29f444ab83f0de930a407ba867b916b63634e19be646019345554ebe0503cca");
            result.Phone1.ShouldBe("e6e061a8f69a47a48db19ac3b5f8437a9273d48f93ca4d76bbc25da83f3");
            result.Phone2.ShouldBe("3145b1d64073438da8e5d2f9a56ca374b346bc8d1d394980bb35eec21b0bf3ad4fba83776f804ff");
            result.ERPCode.ShouldBe("d1adb80a688b43dab69bf9e3e7fffc142461cc460b2a4e3b80f64f5bd839c2f149faa5c");
            result.Active.ShouldBe(true);
            result.EndDate.ShouldBe(new DateTime(2004, 5, 14));
            result.WarehouseId.ShouldBe(Guid.Parse("6147f92e-977c-4690-bfd9-e9d2ca319acf"));
            result.Street.ShouldBe("1aa6201660644741b8ad3da40800fc55e93df2878b3448ff929318d764e44bda1ce74e1b02cc4f");
            result.Address.ShouldBe("7fd88eaba6cf46429cf9c69fa8298f04f3");
            result.Latitude.ShouldBe("b4e8004f83174b33b0e419e0a6dca777a87fdb1eddf44874b914e503eee9052b9682b4447a2b47118344");
            result.Longitude.ShouldBe("c40b531c09c8425e97d3881508ebc7ad2280e402c5fc");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vendorsAppService.DeleteAsync(Guid.Parse("a626f6bb-483a-42fe-aa9e-177a7202e7b5"));

            // Assert
            var result = await _vendorRepository.FindAsync(c => c.Id == Guid.Parse("a626f6bb-483a-42fe-aa9e-177a7202e7b5"));

            result.ShouldBeNull();
        }
    }
}