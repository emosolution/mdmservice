using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerContactsAppService _customerContactsAppService;
        private readonly IRepository<CustomerContact, Guid> _customerContactRepository;

        public CustomerContactsAppServiceTests()
        {
            _customerContactsAppService = GetRequiredService<ICustomerContactsAppService>();
            _customerContactRepository = GetRequiredService<IRepository<CustomerContact, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerContactsAppService.GetListAsync(new GetCustomerContactsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerContact.Id == Guid.Parse("5deb6549-b60f-4dc7-a3b8-b62ec7e734b4")).ShouldBe(true);
            result.Items.Any(x => x.CustomerContact.Id == Guid.Parse("f74433b5-1678-44e5-8cfc-e89661efc363")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerContactsAppService.GetAsync(Guid.Parse("5deb6549-b60f-4dc7-a3b8-b62ec7e734b4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5deb6549-b60f-4dc7-a3b8-b62ec7e734b4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerContactCreateDto
            {
                Title = default,
                FirstName = "9c71567d3c424889a7b5ed77699dc6a5b53783e95c854e1d95537e8b6ddd35664cbcd9bc74",
                LastName = "4ef532b6b3e343599ef7d044d891cb93fc150",
                Gender = default,
                DateOfBirth = new DateTime(2016, 2, 2),
                Phone = "daea506ff961498e81296b1b336c1295d16fce6cfc034f14835c31f37837b8b807643b518eff4f53a88b919a3884499447",
                Email = "a114efebcf054f83b90376ba4eaed31ebfbfbadea36b41039bd1fdc7830e6720fe1b",
                Address = "c927cd48d2ae467d817e64b86977240aa7cdfa09fe",
                IdentityNumber = "3d180a13c9b5421b82eaffc3591df41bc1c8b52735744b4aa69d5c2315164b",
                BankName = "ea9a8b5c919d45ca918068e908a0eccc986480cdef9e4",
                BankAccName = "93c1134b5d0e4cc99b5f4ac56db696b",
                BankAccNumber = "4c54189f7cdf41148cddb2cf074664753308c518141a45f3bb3b447431dfe34605314fab3de649fc9c3fe485179",
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerContactsAppService.CreateAsync(input);

            // Assert
            var result = await _customerContactRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe(default);
            result.FirstName.ShouldBe("9c71567d3c424889a7b5ed77699dc6a5b53783e95c854e1d95537e8b6ddd35664cbcd9bc74");
            result.LastName.ShouldBe("4ef532b6b3e343599ef7d044d891cb93fc150");
            result.Gender.ShouldBe(default);
            result.DateOfBirth.ShouldBe(new DateTime(2016, 2, 2));
            result.Phone.ShouldBe("daea506ff961498e81296b1b336c1295d16fce6cfc034f14835c31f37837b8b807643b518eff4f53a88b919a3884499447");
            result.Email.ShouldBe("a114efebcf054f83b90376ba4eaed31ebfbfbadea36b41039bd1fdc7830e6720fe1b");
            result.Address.ShouldBe("c927cd48d2ae467d817e64b86977240aa7cdfa09fe");
            result.IdentityNumber.ShouldBe("3d180a13c9b5421b82eaffc3591df41bc1c8b52735744b4aa69d5c2315164b");
            result.BankName.ShouldBe("ea9a8b5c919d45ca918068e908a0eccc986480cdef9e4");
            result.BankAccName.ShouldBe("93c1134b5d0e4cc99b5f4ac56db696b");
            result.BankAccNumber.ShouldBe("4c54189f7cdf41148cddb2cf074664753308c518141a45f3bb3b447431dfe34605314fab3de649fc9c3fe485179");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerContactUpdateDto()
            {
                Title = default,
                FirstName = "58d5b6a810234b6a92ed5b6981f9f808e45b17f7ac42",
                LastName = "c671fe52647a44fd9799e0d9bc12539107c1baee279646509",
                Gender = default,
                DateOfBirth = new DateTime(2020, 11, 24),
                Phone = "9d14d747f48c4864ad219b326607b169cd1cb6f2101e4cf8abf39405d0a100b9d30213dbda574b33bd4e7c81db286afd7e",
                Email = "3a078d2efa1e47e",
                Address = "17ff1145751a4913a84841efa1ddbd2a1a9d863",
                IdentityNumber = "858492d7761740d09968e8a6cdc14",
                BankName = "191e2b0fcd21483e80792bde260debe0a0eb7",
                BankAccName = "fbacdb6aa0b84847a2652fab396f389d43cd9b",
                BankAccNumber = "4af82e81106c456591deb36408f4989e977e617c09ca408c996d165f16a84c7b8c20d126788a4d4f94",
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerContactsAppService.UpdateAsync(Guid.Parse("5deb6549-b60f-4dc7-a3b8-b62ec7e734b4"), input);

            // Assert
            var result = await _customerContactRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe(default);
            result.FirstName.ShouldBe("58d5b6a810234b6a92ed5b6981f9f808e45b17f7ac42");
            result.LastName.ShouldBe("c671fe52647a44fd9799e0d9bc12539107c1baee279646509");
            result.Gender.ShouldBe(default);
            result.DateOfBirth.ShouldBe(new DateTime(2020, 11, 24));
            result.Phone.ShouldBe("9d14d747f48c4864ad219b326607b169cd1cb6f2101e4cf8abf39405d0a100b9d30213dbda574b33bd4e7c81db286afd7e");
            result.Email.ShouldBe("3a078d2efa1e47e");
            result.Address.ShouldBe("17ff1145751a4913a84841efa1ddbd2a1a9d863");
            result.IdentityNumber.ShouldBe("858492d7761740d09968e8a6cdc14");
            result.BankName.ShouldBe("191e2b0fcd21483e80792bde260debe0a0eb7");
            result.BankAccName.ShouldBe("fbacdb6aa0b84847a2652fab396f389d43cd9b");
            result.BankAccNumber.ShouldBe("4af82e81106c456591deb36408f4989e977e617c09ca408c996d165f16a84c7b8c20d126788a4d4f94");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerContactsAppService.DeleteAsync(Guid.Parse("5deb6549-b60f-4dc7-a3b8-b62ec7e734b4"));

            // Assert
            var result = await _customerContactRepository.FindAsync(c => c.Id == Guid.Parse("5deb6549-b60f-4dc7-a3b8-b62ec7e734b4"));

            result.ShouldBeNull();
        }
    }
}