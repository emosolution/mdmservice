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
                id: Guid.Parse("cc8b6322-3ee7-4a46-bd95-7ba6da256a12"),
                description: "002913fee4694e89a07327b16e51dde91de821e721fb417ab7f98348a0cae6a561c434560a2446749edd9",
                active: true,
                isAvatar: true,
                fileId: Guid.Parse("3ac87840-0546-4c93-b6fb-7ca3a5a22623"),
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _employeeImageRepository.InsertAsync(new EmployeeImage
            (
                id: Guid.Parse("2b8951d4-26ce-444d-8457-8f584b4344ee"),
                description: "8242eafc26cc453ba20182a2ea3a31f1a03ae1916ad643329f4341cc8fdfacde877ee926f4db4",
                active: true,
                isAvatar: true,
                fileId: Guid.Parse("bbcd19ca-ac73-4a63-8cb7-cc4da40ae214"),
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}