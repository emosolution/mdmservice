using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributeRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerAttributeRepository _customerAttributeRepository;

        public CustomerAttributeRepositoryTests()
        {
            _customerAttributeRepository = GetRequiredService<ICustomerAttributeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerAttributeRepository.GetListAsync(
                    attrName: "d75a8396156444f7a4f8fef68925c67a9bc1cafd5a644b549e088f6b2f9064f921eba12b503149358e7fbcd9ded79c61c291",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerAttributeRepository.GetCountAsync(
                    attrName: "2f7842cae56248f798b63e33dfb6986c2a1d649fb83d4b32b2d97ee7fd7a27a4b38e51023db14a4591d02e49fd832e770675",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}