using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupByAttsAppService _customerGroupByAttsAppService;
        private readonly IRepository<CustomerGroupByAtt, Guid> _customerGroupByAttRepository;

        public CustomerGroupByAttsAppServiceTests()
        {
            _customerGroupByAttsAppService = GetRequiredService<ICustomerGroupByAttsAppService>();
            _customerGroupByAttRepository = GetRequiredService<IRepository<CustomerGroupByAtt, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerGroupByAttsAppService.GetListAsync(new GetCustomerGroupByAttsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerGroupByAtt.Id == Guid.Parse("b5051aef-4045-4c45-9a11-1023a689c744")).ShouldBe(true);
            result.Items.Any(x => x.CustomerGroupByAtt.Id == Guid.Parse("6e1edfb3-8b46-4d26-8da6-84b3137c4319")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupByAttsAppService.GetAsync(Guid.Parse("b5051aef-4045-4c45-9a11-1023a689c744"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b5051aef-4045-4c45-9a11-1023a689c744"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupByAttCreateDto
            {
                ValueCode = "f088ec6e548b4dc883d7ade5680d6518be050fdd7d084e79b0069834ff4cce3e2",
                ValueName = "93d0165c07cc44b9bc2e2144e9e0dcce6e9c1abcaec94282bd3dba9239a7eb",
                CustomerGroupId = Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                CusAttributeValueId = Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd")
            };

            // Act
            var serviceResult = await _customerGroupByAttsAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupByAttRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ValueCode.ShouldBe("f088ec6e548b4dc883d7ade5680d6518be050fdd7d084e79b0069834ff4cce3e2");
            result.ValueName.ShouldBe("93d0165c07cc44b9bc2e2144e9e0dcce6e9c1abcaec94282bd3dba9239a7eb");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupByAttUpdateDto()
            {
                ValueCode = "b2a185980c06486dac2583ac3674659bba171593cd9849349822f519e362e6c43cd46bbaa4",
                ValueName = "20502661080143bc85597cb283c7e589e278693008",
                CustomerGroupId = Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                CusAttributeValueId = Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd")
            };

            // Act
            var serviceResult = await _customerGroupByAttsAppService.UpdateAsync(Guid.Parse("b5051aef-4045-4c45-9a11-1023a689c744"), input);

            // Assert
            var result = await _customerGroupByAttRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ValueCode.ShouldBe("b2a185980c06486dac2583ac3674659bba171593cd9849349822f519e362e6c43cd46bbaa4");
            result.ValueName.ShouldBe("20502661080143bc85597cb283c7e589e278693008");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupByAttsAppService.DeleteAsync(Guid.Parse("b5051aef-4045-4c45-9a11-1023a689c744"));

            // Assert
            var result = await _customerGroupByAttRepository.FindAsync(c => c.Id == Guid.Parse("b5051aef-4045-4c45-9a11-1023a689c744"));

            result.ShouldBeNull();
        }
    }
}