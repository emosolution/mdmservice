using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly INumberingConfigRepository _numberingConfigRepository;

        public NumberingConfigRepositoryTests()
        {
            _numberingConfigRepository = GetRequiredService<INumberingConfigRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _numberingConfigRepository.GetListAsync(
                    prefix: "e88fabc858b34177b150",
                    suffix: "020e39c4c8de4d80aca5",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("e4a8349f-e1bc-4217-a569-6829fb5322e3"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _numberingConfigRepository.GetCountAsync(
                    prefix: "522c941224c645139c05",
                    suffix: "66ebea96bd524154a081",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}