using DMSpro.OMS.MdmService.EmployeeProfiles;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.EmployeeAttachments;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public class EmployeeAttachmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IEmployeeAttachmentRepository _employeeAttachmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly EmployeeProfilesDataSeedContributor _employeeProfilesDataSeedContributor;

        public EmployeeAttachmentsDataSeedContributor(IEmployeeAttachmentRepository employeeAttachmentRepository, IUnitOfWorkManager unitOfWorkManager, EmployeeProfilesDataSeedContributor employeeProfilesDataSeedContributor)
        {
            _employeeAttachmentRepository = employeeAttachmentRepository;
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

            await _employeeAttachmentRepository.InsertAsync(new EmployeeAttachment
            (
                id: Guid.Parse("5b7a3183-f7e9-4c78-b345-63ad7f123f61"),
                url: "206cf36bd3e04993ba656893752a6ef089d4872b2a40402dad9eca74",
                description: "6f9435",
                active: true,
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _employeeAttachmentRepository.InsertAsync(new EmployeeAttachment
            (
                id: Guid.Parse("d96a176e-8c9c-4e13-835e-278d09fd5edc"),
                url: "1f90d0ccdcbe4569af2338336044715984974",
                description: "523351348e834e219b17",
                active: true,
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}