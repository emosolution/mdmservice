using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupGeos;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeoRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupGeoRepository _customerGroupGeoRepository;

        public CustomerGroupGeoRepositoryTests()
        {
            _customerGroupGeoRepository = GetRequiredService<ICustomerGroupGeoRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupGeoRepository.GetListAsync(
                    description: "429320152b174aa1a47f",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("421897c5-aa57-451f-aab6-deab196cf440"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupGeoRepository.GetCountAsync(
                    description: "c25108633e834483874c",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}