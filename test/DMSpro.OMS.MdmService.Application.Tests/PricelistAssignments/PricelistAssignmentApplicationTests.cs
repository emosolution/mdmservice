using System;
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
        public async Task GetAsync()
        {
            // Act
            var result = await _pricelistAssignmentsAppService.GetAsync(Guid.Parse("db548c16-3cf0-49b8-a724-8b8dfa26ffda"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("db548c16-3cf0-49b8-a724-8b8dfa26ffda"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PricelistAssignmentCreateDto
            {
                Description = "d44e89dfcda44b15b28c08e5174ba90e846d05f12eb74528b84403acd83ab86f49b245c49d4f4364a509650d1bcd72379b9f889498f94d11ae67a32b6f9099c09a0361e973784183a2b0adf10996f18e20c21395c6474e0298eba62e6fb3d48d5eed8bfefec642808645c52292afff6f2394e9ba5d8f426a801a55898794c804db6cd34111bb41ac9832012963f41bbddf91d52c2ee44171b20c6aa8d6c1e651e1c7de2c88d04ba9954ff856fe2814bba10ab3f2db504845898c6113a0caa972bb0b6a8a7b6d430a8b9b5609a7f7d715b62bd0c1d983409584fb93f9a5c6b221eeea83dd59934a08ba168593cdb9f4b726731acf5856438cbda9",
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            };

            // Act
            var serviceResult = await _pricelistAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("d44e89dfcda44b15b28c08e5174ba90e846d05f12eb74528b84403acd83ab86f49b245c49d4f4364a509650d1bcd72379b9f889498f94d11ae67a32b6f9099c09a0361e973784183a2b0adf10996f18e20c21395c6474e0298eba62e6fb3d48d5eed8bfefec642808645c52292afff6f2394e9ba5d8f426a801a55898794c804db6cd34111bb41ac9832012963f41bbddf91d52c2ee44171b20c6aa8d6c1e651e1c7de2c88d04ba9954ff856fe2814bba10ab3f2db504845898c6113a0caa972bb0b6a8a7b6d430a8b9b5609a7f7d715b62bd0c1d983409584fb93f9a5c6b221eeea83dd59934a08ba168593cdb9f4b726731acf5856438cbda9");
            result.ReleaseDate.ShouldBe(null);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PricelistAssignmentUpdateDto()
            {
                Description = "8791c3fe2cd84b21ba75b0d1d354d2e72722c5c6257846b49977cb9dd493ca4541fc183108ba4ba6af432a4d3332c7c859156a1d3cce4e748fe062b00760ae85575b181fd3784e1a8544a115f7904da321fcb97712d04daab1f156ac6bc5d09babfe276269134c0e8ec4393ddbec8472a5ae22e0e0a74d159b13a3b05499ad9fb79e4f529e48495d9863da6cfd06dafe45dd1a85674c4d938cf0701c9de440af629725a8c58b4693825e4413dc8506cc88db97f8a3ad43fbaf5b998ee7dd564b710df7e929334374b901d3c2f2721fdf0374afb533b148a18fe34767ffd80875a8e44a1e7a2b4bbaa433fe4204a3f6e56a2d348b54d34eed93b5",
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            };

            // Act
            var serviceResult = await _pricelistAssignmentsAppService.UpdateAsync(Guid.Parse("db548c16-3cf0-49b8-a724-8b8dfa26ffda"), input);

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("8791c3fe2cd84b21ba75b0d1d354d2e72722c5c6257846b49977cb9dd493ca4541fc183108ba4ba6af432a4d3332c7c859156a1d3cce4e748fe062b00760ae85575b181fd3784e1a8544a115f7904da321fcb97712d04daab1f156ac6bc5d09babfe276269134c0e8ec4393ddbec8472a5ae22e0e0a74d159b13a3b05499ad9fb79e4f529e48495d9863da6cfd06dafe45dd1a85674c4d938cf0701c9de440af629725a8c58b4693825e4413dc8506cc88db97f8a3ad43fbaf5b998ee7dd564b710df7e929334374b901d3c2f2721fdf0374afb533b148a18fe34767ffd80875a8e44a1e7a2b4bbaa433fe4204a3f6e56a2d348b54d34eed93b5");
            result.ReleaseDate.ShouldBe(null);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _pricelistAssignmentsAppService.DeleteAsync(Guid.Parse("db548c16-3cf0-49b8-a724-8b8dfa26ffda"));

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("db548c16-3cf0-49b8-a724-8b8dfa26ffda"));

            result.ShouldBeNull();
        }
    }
}