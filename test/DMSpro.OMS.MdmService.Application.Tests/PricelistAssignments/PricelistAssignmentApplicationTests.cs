using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IPricelistAssignmentsAppService _pricelistAssignmentsAppService;
        private readonly IRepository<PricelistAssignment, Guid> _pricelistAssignmentRepository;

        public PricelistAssignmentsAppServiceTests()
        {
            _pricelistAssignmentsAppService = GetRequiredService<IPricelistAssignmentsAppService>();
            _pricelistAssignmentRepository = GetRequiredService<IRepository<PricelistAssignment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _pricelistAssignmentsAppService.GetListAsync(new GetPricelistAssignmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.PricelistAssignment.Id == Guid.Parse("2b969661-f3b7-439d-a59d-7645f2568324")).ShouldBe(true);
            result.Items.Any(x => x.PricelistAssignment.Id == Guid.Parse("696527d2-cbbb-4bae-b694-6dce484e2ade")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _pricelistAssignmentsAppService.GetAsync(Guid.Parse("2b969661-f3b7-439d-a59d-7645f2568324"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("2b969661-f3b7-439d-a59d-7645f2568324"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PricelistAssignmentCreateDto
            {
                Description = "4a4d5377d978451a904beb32402027653adb3d48460849dc9373957651dab9d71925665e62104f05848a22121b6d0aedbd7a6d6cf7e5406abbeb5626ff91d965500ecd69f25e48d4babaa7640bfb137d53e673fc297749c9bba46808103807fcdb883b4658dd4f88852f91598f29c981ea0d87b360824a1aa40133899f56797146afd32d34114b788c57a174dd2fff0049460cc1d8f6446b8000ef5ed1efead020c01f3124184356ad1085321013386b5dd2a4edc002468ebdc055f5c9e841fde05802116a514b72931b91845d25ad6ac6b9ecd517394388a57a4cd99a0e898447bd9ab5f8074d9ba3391bf30e60c1e2d9ea592e8f4448ce9da7",
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            };

            // Act
            var serviceResult = await _pricelistAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("4a4d5377d978451a904beb32402027653adb3d48460849dc9373957651dab9d71925665e62104f05848a22121b6d0aedbd7a6d6cf7e5406abbeb5626ff91d965500ecd69f25e48d4babaa7640bfb137d53e673fc297749c9bba46808103807fcdb883b4658dd4f88852f91598f29c981ea0d87b360824a1aa40133899f56797146afd32d34114b788c57a174dd2fff0049460cc1d8f6446b8000ef5ed1efead020c01f3124184356ad1085321013386b5dd2a4edc002468ebdc055f5c9e841fde05802116a514b72931b91845d25ad6ac6b9ecd517394388a57a4cd99a0e898447bd9ab5f8074d9ba3391bf30e60c1e2d9ea592e8f4448ce9da7");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PricelistAssignmentUpdateDto()
            {
                Description = "384a299cb95b4cf28ffb12681efc29c8f332c28562374ed3b406846bd1b56d280d1d975656584cd1b2a86c8bbb811f415a939049ac8e45e9bc5c867c9e88230fd0ed8ccf47cc4871be9cef1d2c269cc7113bac96d82443ed86feb8ffa18170267c237b47904e4922adc13d5d0920664eb9896a507e5e4f2581b12b3e998c35b403e6d438d36d470a8532c646148b80c7409a342162b14c20b37487a7175631801aad3a1f83dd45d18b51bb6ca31431f6a2acbd977abd43388c90398f0611f3eaaa76d3457a7648579786b4060c0f21b01d16569b986b4906b96a7e6fdd069f0f1c969830542a427a8d24d862c78e69a643397877c06f4ae0a9c0",
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            };

            // Act
            var serviceResult = await _pricelistAssignmentsAppService.UpdateAsync(Guid.Parse("2b969661-f3b7-439d-a59d-7645f2568324"), input);

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("384a299cb95b4cf28ffb12681efc29c8f332c28562374ed3b406846bd1b56d280d1d975656584cd1b2a86c8bbb811f415a939049ac8e45e9bc5c867c9e88230fd0ed8ccf47cc4871be9cef1d2c269cc7113bac96d82443ed86feb8ffa18170267c237b47904e4922adc13d5d0920664eb9896a507e5e4f2581b12b3e998c35b403e6d438d36d470a8532c646148b80c7409a342162b14c20b37487a7175631801aad3a1f83dd45d18b51bb6ca31431f6a2acbd977abd43388c90398f0611f3eaaa76d3457a7648579786b4060c0f21b01d16569b986b4906b96a7e6fdd069f0f1c969830542a427a8d24d862c78e69a643397877c06f4ae0a9c0");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _pricelistAssignmentsAppService.DeleteAsync(Guid.Parse("2b969661-f3b7-439d-a59d-7645f2568324"));

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("2b969661-f3b7-439d-a59d-7645f2568324"));

            result.ShouldBeNull();
        }
    }
}