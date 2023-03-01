using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public class EmployeeAttachmentManager : DomainService
    {
        private readonly IEmployeeAttachmentRepository _employeeAttachmentRepository;

        public EmployeeAttachmentManager(IEmployeeAttachmentRepository employeeAttachmentRepository)
        {
            _employeeAttachmentRepository = employeeAttachmentRepository;
        }

        public async Task<EmployeeAttachment> CreateAsync(
        Guid employeeProfileId, string description, bool active, Guid fileId)
        {
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));
            Check.Length(description, nameof(description), EmployeeAttachmentConsts.DescriptionMaxLength);

            var employeeAttachment = new EmployeeAttachment(
             GuidGenerator.Create(),
             employeeProfileId, description, active, fileId
             );

            return await _employeeAttachmentRepository.InsertAsync(employeeAttachment);
        }

        public async Task<EmployeeAttachment> UpdateAsync(
            Guid id,
            Guid employeeProfileId, string description, bool active, Guid fileId, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));
            Check.Length(description, nameof(description), EmployeeAttachmentConsts.DescriptionMaxLength);

            var employeeAttachment = await _employeeAttachmentRepository.GetAsync(id);

            employeeAttachment.EmployeeProfileId = employeeProfileId;
            employeeAttachment.Description = description;
            employeeAttachment.Active = active;
            employeeAttachment.FileId = fileId;

            employeeAttachment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _employeeAttachmentRepository.UpdateAsync(employeeAttachment);
        }

    }
}