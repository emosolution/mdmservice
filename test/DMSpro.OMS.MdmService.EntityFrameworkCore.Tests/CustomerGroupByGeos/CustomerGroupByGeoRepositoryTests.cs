using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupByGeos;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class CustomerGroupByGeoRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupByGeoRepository _customerGroupByGeoRepository;

        public CustomerGroupByGeoRepositoryTests()
        {
            _customerGroupByGeoRepository = GetRequiredService<ICustomerGroupByGeoRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupByGeoRepository.GetListAsync(
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d80a2a0d-fbc9-43f0-8b13-f0ca571774ef"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupByGeoRepository.GetCountAsync(
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}