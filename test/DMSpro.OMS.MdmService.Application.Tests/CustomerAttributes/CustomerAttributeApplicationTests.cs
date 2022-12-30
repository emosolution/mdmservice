using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerAttributesAppService _customerAttributesAppService;
        private readonly IRepository<CustomerAttribute, Guid> _customerAttributeRepository;

        public CustomerAttributesAppServiceTests()
        {
            _customerAttributesAppService = GetRequiredService<ICustomerAttributesAppService>();
            _customerAttributeRepository = GetRequiredService<IRepository<CustomerAttribute, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerAttributesAppService.GetListAsync(new GetCustomerAttributesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("d7a80fca-0873-49ee-860d-38bbc820d59e")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerAttributesAppService.GetAsync(Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerAttributeCreateDto
            {
                AttrNo = 0,
                AttrName = "4c0307cda87b4336b3bd16494a935571b24a19e2dd014bc2b874e5e932e802b147a56b21b8ae4e27bd84ef37948bb78aa055",
                HierarchyLevel = 18,
                Active = true
            };

            // Act
            var serviceResult = await _customerAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _customerAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe(0);
            result.AttrName.ShouldBe("4c0307cda87b4336b3bd16494a935571b24a19e2dd014bc2b874e5e932e802b147a56b21b8ae4e27bd84ef37948bb78aa055");
            result.HierarchyLevel.ShouldBe(18);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerAttributeUpdateDto()
            {
                AttrNo = 3,
                AttrName = "33542470e18041ab9d0295d7078d32f01fa805fe1c2049dbaf222e98ce254167375415386214410f9af23399cd2e657605cd",
                HierarchyLevel = 18,
                Active = true
            };

            // Act
            var serviceResult = await _customerAttributesAppService.UpdateAsync(Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"), input);

            // Assert
            var result = await _customerAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe(3);
            result.AttrName.ShouldBe("33542470e18041ab9d0295d7078d32f01fa805fe1c2049dbaf222e98ce254167375415386214410f9af23399cd2e657605cd");
            result.HierarchyLevel.ShouldBe(18);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerAttributesAppService.DeleteAsync(Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"));

            // Assert
            var result = await _customerAttributeRepository.FindAsync(c => c.Id == Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"));

            result.ShouldBeNull();
        }
    }
}