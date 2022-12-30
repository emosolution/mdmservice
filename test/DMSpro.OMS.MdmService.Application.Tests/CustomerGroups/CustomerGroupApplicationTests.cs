using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupsAppService _customerGroupsAppService;
        private readonly IRepository<CustomerGroup, Guid> _customerGroupRepository;

        public CustomerGroupsAppServiceTests()
        {
            _customerGroupsAppService = GetRequiredService<ICustomerGroupsAppService>();
            _customerGroupRepository = GetRequiredService<IRepository<CustomerGroup, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerGroupsAppService.GetListAsync(new GetCustomerGroupsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("a53045fa-bd3b-449b-97f1-8e265b1358f4")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("e80e8de3-f2ab-4d55-a329-bf3cbc47e66a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupsAppService.GetAsync(Guid.Parse("a53045fa-bd3b-449b-97f1-8e265b1358f4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a53045fa-bd3b-449b-97f1-8e265b1358f4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupCreateDto
            {
                Code = "b9fb00f40e8e4e77a686",
                Name = "657637d110f7464d8a9449b84251722e80854bbd61804388a4acf06b39d1de2a31eb32c3d837402a93e9c1303602",
                Active = true,
                EffectiveDate = new DateTime(2019, 9, 15),
                GroupBy = default,
                Status = default
            };

            // Act
            var serviceResult = await _customerGroupsAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("b9fb00f40e8e4e77a686");
            result.Name.ShouldBe("657637d110f7464d8a9449b84251722e80854bbd61804388a4acf06b39d1de2a31eb32c3d837402a93e9c1303602");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2019, 9, 15));
            result.GroupBy.ShouldBe(default);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupUpdateDto()
            {
                Code = "c3999427dc384dbd8231",
                Name = "1dc82cc1a97c4f6b89078b501ac3796c70fee5cc88eb4342a79401ac7d0df",
                Active = true,
                EffectiveDate = new DateTime(2008, 4, 8),
                GroupBy = default,
                Status = default
            };

            // Act
            var serviceResult = await _customerGroupsAppService.UpdateAsync(Guid.Parse("a53045fa-bd3b-449b-97f1-8e265b1358f4"), input);

            // Assert
            var result = await _customerGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("c3999427dc384dbd8231");
            result.Name.ShouldBe("1dc82cc1a97c4f6b89078b501ac3796c70fee5cc88eb4342a79401ac7d0df");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2008, 4, 8));
            result.GroupBy.ShouldBe(default);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupsAppService.DeleteAsync(Guid.Parse("a53045fa-bd3b-449b-97f1-8e265b1358f4"));

            // Assert
            var result = await _customerGroupRepository.FindAsync(c => c.Id == Guid.Parse("a53045fa-bd3b-449b-97f1-8e265b1358f4"));

            result.ShouldBeNull();
        }
    }
}