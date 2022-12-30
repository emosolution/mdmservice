using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IWorkingPositionRepository _workingPositionRepository;

        public WorkingPositionRepositoryTests()
        {
            _workingPositionRepository = GetRequiredService<IWorkingPositionRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _workingPositionRepository.GetListAsync(
                    code: "5bc59e9653624df6aa70",
                    name: "ccef596cb06c457cace6e5b9b914947212259f2b8d644608a4d22464371c38f184c6a0fcfae541f1aec2f099be92",
                    description: "1d6c99b36e7",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("f7942152-dbeb-42ad-9935-c3064738dc4e"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _workingPositionRepository.GetCountAsync(
                    code: "79de8f9dd2534096b86c",
                    name: "1917671cf2684827b2b44c7b21929ae80ffa12",
                    description: "4d1bd92937d84310820d58780",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}