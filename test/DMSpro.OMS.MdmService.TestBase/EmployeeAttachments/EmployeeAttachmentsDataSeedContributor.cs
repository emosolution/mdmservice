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
                id: Guid.Parse("ecc88372-b838-46e7-acb9-35460da0b2ee"),
                description: "26e777a795bf400aaf17eba2ee40fe6e71d42ca04db94b408b3f",
                active: true,
                fileId: Guid.Parse("8d715ab0-e548-472b-9fba-da58d5114160"),
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _employeeAttachmentRepository.InsertAsync(new EmployeeAttachment
            (
                id: Guid.Parse("16c313cf-ceb0-4326-88df-c18e28dd19c3"),
                description: "4efa62fe5eb94042a5ae95bf11aff2f31f43d664c8d547e9b8b8aa8dcd4cc7b9fe3",
                active: true,
                fileId: Guid.Parse("68e3715d-52ee-492b-8804-4e38b7cc2f36"),
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}