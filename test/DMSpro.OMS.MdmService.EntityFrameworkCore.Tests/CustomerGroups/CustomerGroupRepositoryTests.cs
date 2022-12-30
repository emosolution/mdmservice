using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupRepository _customerGroupRepository;

        public CustomerGroupRepositoryTests()
        {
            _customerGroupRepository = GetRequiredService<ICustomerGroupRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupRepository.GetListAsync(
                    code: "da5d436cd5954d88a90e",
                    name: "784ea625b4f5411e9a00e4c3bf99c095936f4c5865e648898c3cf820c10cab9328e1a3cfebec4b9d96172c4b4",
                    active: true,
                    groupBy: default,
                    status: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a53045fa-bd3b-449b-97f1-8e265b1358f4"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupRepository.GetCountAsync(
                    code: "4e439fb805ab45d09292",
                    name: "838838978fc84b31be44077763b283e0dc2d41ee27dd4947a589d4927796c486ccd72c2da37f4aa3a8b088d29",
                    active: true,
                    groupBy: default,
                    status: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}