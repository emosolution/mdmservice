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
                    valueCode: "0c27a455d2b44e0f935cd5e8438dcb1118bca223a38a4cbfb",
                    valueName: "d6c4d9c3679347da8fa350412c8e76f60a5e4fc8405e49e4973067b9b8f46cc465426f395e154e6d847ef57ab4"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b5051aef-4045-4c45-9a11-1023a689c744"));
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
                    valueCode: "9a63d55654cc435cb2ec",
                    valueName: "ddd3058befd54d96be4cce5360"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}