using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupByAtts;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupByAttRepository _customerGroupByAttRepository;

        public CustomerGroupByAttRepositoryTests()
        {
            _customerGroupByAttRepository = GetRequiredService<ICustomerGroupByAttRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupByAttRepository.GetListAsync(
                    valueCode: "eaa533bcf8fe444bbbe5",
                    valueName: "364356d9dc754b99bc838d5dc7942d4b8476081c435147938ee086010e531c3d79ca5fe7b720449fb666d361ebdc6862907a4c98de9940489ea73d02a1457ac437a18c763fc44d528b5d8e335cfb1a1d7fd8c609f38f48e18d4f3f6df1cdb8f31679d9252fd449f29a8d725809bf0d9751f7a6ef8f434521bcab489c03445da"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("461ea009-5f7b-4e83-a5e1-35e0c69a231d"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupByAttRepository.GetCountAsync(
                    valueCode: "c9460c857f6d47eb88ac",
                    valueName: "ff6451e7ff914fc485144e50bc8f4416497a5be47505421794ced6ab6b9a68d51be0f29ac2ba41a28fd08fb6634e5d0cf2aee96532244a7d8bb36e71428e39c55195c0b497d5442d8750ece2e2c539a2e8146676cf184fe7abdb123ffbdd9f39b666e9ef063448cd8fc32a6ae0e2d81fad1d6632e5fd4209b19aac1db35081a"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}