using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EmployeeImages;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImageRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IEmployeeImageRepository _employeeImageRepository;

        public EmployeeImageRepositoryTests()
        {
            _employeeImageRepository = GetRequiredService<IEmployeeImageRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeImageRepository.GetListAsync(
                    description: "002913fee4694e89a07327b16e51dde91de821e721fb417ab7f98348a0cae6a561c434560a2446749edd9",
                    active: true,
                    isAvatar: true,
                    fileId: Guid.Parse("3ac87840-0546-4c93-b6fb-7ca3a5a22623")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("cc8b6322-3ee7-4a46-bd95-7ba6da256a12"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeImageRepository.GetCountAsync(
                    description: "8242eafc26cc453ba20182a2ea3a31f1a03ae1916ad643329f4341cc8fdfacde877ee926f4db4",
                    active: true,
                    isAvatar: true,
                    fileId: Guid.Parse("bbcd19ca-ac73-4a63-8cb7-cc4da40ae214")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}