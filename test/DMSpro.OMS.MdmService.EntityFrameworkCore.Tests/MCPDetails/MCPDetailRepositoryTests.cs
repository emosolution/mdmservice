using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public class MCPDetailRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IMCPDetailRepository _mCPDetailRepository;

        public MCPDetailRepositoryTests()
        {
            _mCPDetailRepository = GetRequiredService<IMCPDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _mCPDetailRepository.GetListAsync(
                    code: "02692e47c4144a22bc1e",
                    monday: true,
                    tuesday: true,
                    wednesday: true,
                    thursday: true,
                    friday: true,
                    saturday: true,
                    sunday: true,
                    week1: true,
                    week2: true,
                    week3: true,
                    week4: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _mCPDetailRepository.GetCountAsync(
                    code: "1d126a13ebd34048b761",
                    monday: true,
                    tuesday: true,
                    wednesday: true,
                    thursday: true,
                    friday: true,
                    saturday: true,
                    sunday: true,
                    week1: true,
                    week2: true,
                    week3: true,
                    week4: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}