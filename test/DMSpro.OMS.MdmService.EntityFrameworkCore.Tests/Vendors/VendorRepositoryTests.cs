using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Vendors;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorRepositoryTests()
        {
            _vendorRepository = GetRequiredService<IVendorRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vendorRepository.GetListAsync(
                    code: "ec0c4b5bfda24e14afb6",
                    name: "a46456150a1d4a9fb117d9cfa5bdee243c44ab523f9a4fae91bbf890ad817fddb7f3aab05b9f4b88991b6043c131fea87dd7f3dcaa6f41c5ad98208b7ae04b27c4f6113bfd674e9da1eeb5192833b0eb5266294ff78c4bfd9551f5ed172b283332b63eb4",
                    shortName: "9acfef8cc37b40019d2a381f976e7ebc2ef3a70f42ae48139422b1e2bea67fdec5f15824b2c74ebdaee60c1f75f55f4f2ba4b314e05a4068a83c487b84be732c80e008c0e5d049acbb1ecbf7c388ba3a6e6b6a7f586b48509a53415b6d3a0e19ab3b292a",
                    phone1: "7be3bff1abde45f4b887f5f729524f1cb97d740dd4b74832af2231777f6293756c8646a87d334eb8a1f9c9ad9",
                    phone2: "b9a55f1d1b2b45debdbf30cd3335be7d3",
                    erpCode: "1239cb6299d244599425a15ad083368ddc442bb0499a4440b879e9fb215acd6925c9458ea9",
                    active: true,
                    linkedCompany: "b3b12ce857844831923b",
                    warehouseId: Guid.Parse("cebddb84-f425-4f67-a045-5149bf03072b"),
                    street: "2759a6b3a52347feaf02d897096091c22e5d87e096bc4a029beacb708b231c06fc3ba7d748f541e6bb269b6d860e4d93f",
                    address: "8325723311a240ee9c013c682bd40c2a9ed7082f2d",
                    latitude: "161294507a414d05ad24986bb905402f9571",
                    longitude: "7a5704460ad14ed5b4ae562285bb56e397b4ec028d0646fc9548a441de9507674db881dc84704ba7"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("e19c23e6-f052-4267-bae1-7c923cd2d7db"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vendorRepository.GetCountAsync(
                    code: "ebe53f13f66347a6bfc7",
                    name: "ed9d3c14781b490eb974796ffef4c6677aa4af667d61410e95fc313d6eefb6dee93e18a60e504dca9e8e44d2d710a8893e36ff8c607f49a98eee482624a7f15d535299789db2471e9235c624aa8afce97b5aee4d95a24873a200fe7ae6883d4a02a7e5d4",
                    shortName: "037cbfaea6f14c4a8afcd90d75393754faea0bdb7dda4614a981db41229ecf81647f6ffcee4245e6926ab2e9156d9e059b41e703d2a64845a46f8e461a8e92c2efb7d304374c4c74be0d69009f7cf014a538059a38274c46bef56ce0d0f140bb333d604e",
                    phone1: "2a89eb2174484fe791361ebfadaf661fcd923",
                    phone2: "b233fd5504f8460298fd4ac9ee4c86a5466a71f927f2459aa2da047d57faa6aa7ebbd95",
                    erpCode: "ec2f722be84640258a71a5132a5b53dfdf57c5c7685f45cd859fc6613c05ca14add5280a",
                    active: true,
                    linkedCompany: "62ba645ecf2e46469124",
                    warehouseId: Guid.Parse("b6db53d8-c31c-49ac-bfe8-5d1bde477f86"),
                    street: "fe56bc1e2da447b8bf07807d4f132a4997d11b37cb434607b",
                    address: "fb4670c3277143189dd9827b040ac88dfbb37f72e9c7437fa7b1ed92cf4dd7aafb856f7aebad4d849",
                    latitude: "6e1d34e7018d41f",
                    longitude: "44d97e81d5d4470bbf45da714e7d37e0c04fe85e18144841"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}