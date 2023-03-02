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
            result.Items.Any(x => x.Id == Guid.Parse("89509369-e333-4c67-9869-c40b3d1481bb")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("d7d80755-df0f-49c8-bfd4-ddc9a353faa5")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _weightMeasurementsAppService.GetAsync(Guid.Parse("89509369-e333-4c67-9869-c40b3d1481bb"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("89509369-e333-4c67-9869-c40b3d1481bb"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WeightMeasurementCreateDto
            {
                Code = "ede5d01eeeb043458400bb23f7a40a31e1328c641b8d44deacf76a0",
                Name = "57a6d2dfee5249a291840dbede85796df8da1edd9e2441f3a1",
                Value = 732663663
            };

            // Act
            var serviceResult = await _weightMeasurementsAppService.CreateAsync(input);

            // Assert
            var result = await _weightMeasurementRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ede5d01eeeb043458400bb23f7a40a31e1328c641b8d44deacf76a0");
            result.Name.ShouldBe("57a6d2dfee5249a291840dbede85796df8da1edd9e2441f3a1");
            result.Value.ShouldBe(732663663);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WeightMeasurementUpdateDto()
            {
                Code = "36888d45a8044073839f74c71b4a2a9968c89a112b0548",
                Name = "ab143c1581bc4f359f63978ae300b47dcac00c568ec040aa92",
                Value = 250681632
            };

            // Act
            var serviceResult = await _weightMeasurementsAppService.UpdateAsync(Guid.Parse("89509369-e333-4c67-9869-c40b3d1481bb"), input);

            // Assert
            var result = await _weightMeasurementRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("36888d45a8044073839f74c71b4a2a9968c89a112b0548");
            result.Name.ShouldBe("ab143c1581bc4f359f63978ae300b47dcac00c568ec040aa92");
            result.Value.ShouldBe(250681632);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _weightMeasurementsAppService.DeleteAsync(Guid.Parse("89509369-e333-4c67-9869-c40b3d1481bb"));

            // Assert
            var result = await _weightMeasurementRepository.FindAsync(c => c.Id == Guid.Parse("89509369-e333-4c67-9869-c40b3d1481bb"));

            result.ShouldBeNull();
        }
    }
}