using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public class WeightMeasurementsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IWeightMeasurementsAppService _weightMeasurementsAppService;
        private readonly IRepository<WeightMeasurement, Guid> _weightMeasurementRepository;

        public WeightMeasurementsAppServiceTests()
        {
            _weightMeasurementsAppService = GetRequiredService<IWeightMeasurementsAppService>();
            _weightMeasurementRepository = GetRequiredService<IRepository<WeightMeasurement, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _weightMeasurementsAppService.GetListAsync(new GetWeightMeasurementsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("d5870506-875a-4861-9faf-442ee0bbffc0")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("c34fb548-4635-4c85-8128-0f0e1a949356")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _weightMeasurementsAppService.GetAsync(Guid.Parse("d5870506-875a-4861-9faf-442ee0bbffc0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d5870506-875a-4861-9faf-442ee0bbffc0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WeightMeasurementCreateDto
            {
                Code = "d07d8a5c59804f19aaa15963f658589eda9332033dc84ecb923305e770f22",
                Name = "d015faadfb514239a242726124b07a887383ceaee0bb4d2880",
                Value = 1781829041
            };

            // Act
            var serviceResult = await _weightMeasurementsAppService.CreateAsync(input);

            // Assert
            var result = await _weightMeasurementRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("d07d8a5c59804f19aaa15963f658589eda9332033dc84ecb923305e770f22");
            result.Name.ShouldBe("d015faadfb514239a242726124b07a887383ceaee0bb4d2880");
            result.Value.ToString().ShouldBe("1781829041");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WeightMeasurementUpdateDto()
            {
                Code = "73317087123244ae",
                Name = "9488c373caa846de870fad9ba17390adadbbf1361a0541a399",
                Value = 597053901
            };

            // Act
            var serviceResult = await _weightMeasurementsAppService.UpdateAsync(Guid.Parse("d5870506-875a-4861-9faf-442ee0bbffc0"), input);

            // Assert
            var result = await _weightMeasurementRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("73317087123244ae");
            result.Name.ShouldBe("9488c373caa846de870fad9ba17390adadbbf1361a0541a399");
            result.Value.ToString().ShouldBe("597053901");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _weightMeasurementsAppService.DeleteAsync(Guid.Parse("d5870506-875a-4861-9faf-442ee0bbffc0"));

            // Assert
            var result = await _weightMeasurementRepository.FindAsync(c => c.Id == Guid.Parse("d5870506-875a-4861-9faf-442ee0bbffc0"));

            result.ShouldBeNull();
        }
    }
}