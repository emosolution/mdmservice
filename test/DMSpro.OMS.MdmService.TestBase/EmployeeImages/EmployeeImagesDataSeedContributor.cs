using DMSpro.OMS.MdmService.EmployeeProfiles;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.EmployeeImages;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImagesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IEmployeeImageRepository _employeeImageRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly EmployeeProfilesDataSeedContributor _employeeProfilesDataSeedContributor;

        public EmployeeImagesDataSeedContributor(IEmployeeImageRepository employeeImageRepository, IUnitOfWorkManager unitOfWorkManager, EmployeeProfilesDataSeedContributor employeeProfilesDataSeedContributor)
        {
            _employeeImageRepository = employeeImageRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _employeeProfilesDataSeedContributor = employeeProfilesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _employeeProfilesDataSeedContributor.SeedAsync(context);

            await _employeeImageRepository.InsertAsync(new EmployeeImage
            (
                id: Guid.Parse("8edf5e2e-2a59-410d-962c-2275a580468b"),
                description: "05ea0976e2c043b396c8baa6a524eae6052d04b6",
                url: "d92ca216f96a4123b0448ea2c4f686acc2d450e09d71453081a34e",
                active: true,
                isAvatar: true,
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _employeeImageRepository.InsertAsync(new EmployeeImage
            (
                id: Guid.Parse("d117d5ff-356c-4418-beeb-529d98ce0e36"),
                description: "4b34c82c146e4ae58c8bab953f817b1478cb75ac54b549dd875d86bdeba2a11ebadd81b9b05e4c87a5b9f393810b77a",
                url: "a16b398896d941a1a51e02832f9cc78f7dcd26d6d2d143bfba572583555ab38bf335a4db42cc45eeb852bf37",
                active: true,
                isAvatar: true,
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}