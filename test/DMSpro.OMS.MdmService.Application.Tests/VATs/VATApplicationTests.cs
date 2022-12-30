using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.VATs
{
    public class VATsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IVATsAppService _vATsAppService;
        private readonly IRepository<VAT, Guid> _vATRepository;

        public VATsAppServiceTests()
        {
            _vATsAppService = GetRequiredService<IVATsAppService>();
            _vATRepository = GetRequiredService<IRepository<VAT, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _vATsAppService.GetListAsync(new GetVATsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("a0228ab7-7881-4112-a1e3-844a94951144")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vATsAppService.GetAsync(Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VATCreateDto
            {
                Code = "1373136482484633976e",
                Name = "9a0ed6b43bca490c8567d9029479163d6fe2aa587df34181ab5d6c1f95a2ed42b3c388f746fb4da49af7143f01450a796f47",
                Rate = 56464
            };

            // Act
            var serviceResult = await _vATsAppService.CreateAsync(input);

            // Assert
            var result = await _vATRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("1373136482484633976e");
            result.Name.ShouldBe("9a0ed6b43bca490c8567d9029479163d6fe2aa587df34181ab5d6c1f95a2ed42b3c388f746fb4da49af7143f01450a796f47");
            result.Rate.ToString().ShouldBe("56464");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VATUpdateDto()
            {
                Code = "7ba078c9e1024cbda404",
                Name = "e8cbe896ab0743ff9a846d29f5ee23302941eba4179e4da28a997cd10312097947f28d4309f7402ba557375d255b35c37e0f",
                Rate = 83164
            };

            // Act
            var serviceResult = await _vATsAppService.UpdateAsync(Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"), input);

            // Assert
            var result = await _vATRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("7ba078c9e1024cbda404");
            result.Name.ShouldBe("e8cbe896ab0743ff9a846d29f5ee23302941eba4179e4da28a997cd10312097947f28d4309f7402ba557375d255b35c37e0f");
            result.Rate.ToString().ShouldBe("83164");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vATsAppService.DeleteAsync(Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"));

            // Assert
            var result = await _vATRepository.FindAsync(c => c.Id == Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"));

            result.ShouldBeNull();
        }
    }
}