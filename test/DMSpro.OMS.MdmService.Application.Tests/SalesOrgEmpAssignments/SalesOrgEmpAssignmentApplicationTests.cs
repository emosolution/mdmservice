using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISalesOrgEmpAssignmentsAppService _salesOrgEmpAssignmentsAppService;
        private readonly IRepository<SalesOrgEmpAssignment, Guid> _salesOrgEmpAssignmentRepository;

        public SalesOrgEmpAssignmentsAppServiceTests()
        {
            _salesOrgEmpAssignmentsAppService = GetRequiredService<ISalesOrgEmpAssignmentsAppService>();
            _salesOrgEmpAssignmentRepository = GetRequiredService<IRepository<SalesOrgEmpAssignment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _salesOrgEmpAssignmentsAppService.GetListAsync(new GetSalesOrgEmpAssignmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.SalesOrgEmpAssignment.Id == Guid.Parse("60067dee-66de-400a-8300-0c7a70249917")).ShouldBe(true);
            result.Items.Any(x => x.SalesOrgEmpAssignment.Id == Guid.Parse("a1ca850d-9877-4814-a7aa-0b5814f5383a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _salesOrgEmpAssignmentsAppService.GetAsync(Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesOrgEmpAssignmentCreateDto
            {
                IsBase = true,
                EffectiveDate = new DateTime(2018, 9, 15),
                EndDate = new DateTime(2018, 7, 12),
                HierarchyCode = "68226d6b862a4c94b16b21e1ef9c9342feef81f1d88b4aa8b185c8167658b40cb97b25eca2d543038bdbc99f5a4d4ce9d0120e9abb604822ba854702224332a728cd7a02e3ad4c73881629c05d1c8db1375a83046d46499c8cda378930483924ec89fb19dab048139c8ed135b0eea26b80f901edbc6b4b1c93447dae4a4e213cc75bd82b9f6146abb804a1cbc7d30bdfa30ee57da5e746f1ace8c50bcca1d39adc5394a64fd74b49b9c38d1ccc0cdcfbfc6a95ac4d864c9eb42afbaf64116b903676fd9729fe4723aeadd4b6d27ff58c627d765a08034cc9bfe18b0bf3c3531b97b2c68cef534cfa9e3b4e2c737ca4010b25e4b7de2547a387ea",
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _salesOrgEmpAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _salesOrgEmpAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsBase.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2018, 9, 15));
            result.EndDate.ShouldBe(new DateTime(2018, 7, 12));
            result.HierarchyCode.ShouldBe("68226d6b862a4c94b16b21e1ef9c9342feef81f1d88b4aa8b185c8167658b40cb97b25eca2d543038bdbc99f5a4d4ce9d0120e9abb604822ba854702224332a728cd7a02e3ad4c73881629c05d1c8db1375a83046d46499c8cda378930483924ec89fb19dab048139c8ed135b0eea26b80f901edbc6b4b1c93447dae4a4e213cc75bd82b9f6146abb804a1cbc7d30bdfa30ee57da5e746f1ace8c50bcca1d39adc5394a64fd74b49b9c38d1ccc0cdcfbfc6a95ac4d864c9eb42afbaf64116b903676fd9729fe4723aeadd4b6d27ff58c627d765a08034cc9bfe18b0bf3c3531b97b2c68cef534cfa9e3b4e2c737ca4010b25e4b7de2547a387ea");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesOrgEmpAssignmentUpdateDto()
            {
                IsBase = true,
                EffectiveDate = new DateTime(2008, 2, 3),
                EndDate = new DateTime(2002, 7, 10),
                HierarchyCode = "5efad74e2c37452b8a481a45fcfc642d78247920414046908460c5e5fec314eff9770eff0aa941399d46f1a2f77d4ca0dc3cf6390c7b48408e1bc694ade1c4dab1279c932ee8476eb99014bb6f2a0c031bf10b51608d428db9d0119823d4faf13bbd09e014f24ec5a1bf4d89de7d1efb457c8cbd6300434bad3d815c0b5c4edf1e0c5a92b8904cd6b32a438d1ae324894f3961d008a044cc96e4cf2f7b5a032dc2685a6413af42bbb95761308f615a1ee6c7c42069e74d318426ad96f548bdd2518674aa010c4c9491bd6320804033e9ba0cc98676db41ae998e58758bb89ee746558fdc4d8946a790cbf4bb14ebf99ea3d775f7c2c34ba19068",
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _salesOrgEmpAssignmentsAppService.UpdateAsync(Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"), input);

            // Assert
            var result = await _salesOrgEmpAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsBase.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2008, 2, 3));
            result.EndDate.ShouldBe(new DateTime(2002, 7, 10));
            result.HierarchyCode.ShouldBe("5efad74e2c37452b8a481a45fcfc642d78247920414046908460c5e5fec314eff9770eff0aa941399d46f1a2f77d4ca0dc3cf6390c7b48408e1bc694ade1c4dab1279c932ee8476eb99014bb6f2a0c031bf10b51608d428db9d0119823d4faf13bbd09e014f24ec5a1bf4d89de7d1efb457c8cbd6300434bad3d815c0b5c4edf1e0c5a92b8904cd6b32a438d1ae324894f3961d008a044cc96e4cf2f7b5a032dc2685a6413af42bbb95761308f615a1ee6c7c42069e74d318426ad96f548bdd2518674aa010c4c9491bd6320804033e9ba0cc98676db41ae998e58758bb89ee746558fdc4d8946a790cbf4bb14ebf99ea3d775f7c2c34ba19068");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesOrgEmpAssignmentsAppService.DeleteAsync(Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"));

            // Assert
            var result = await _salesOrgEmpAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"));

            result.ShouldBeNull();
        }
    }
}