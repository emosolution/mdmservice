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
        Guid employeeProfileId, string url, string description, bool active)
        {
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            var employeeAttachment = new EmployeeAttachment(
             GuidGenerator.Create(),
             employeeProfileId, url, description, active
             );

            return await _employeeAttachmentRepository.InsertAsync(employeeAttachment);
        }

        public async Task<EmployeeAttachment> UpdateAsync(
            Guid id,
            Guid employeeProfileId, string url, string description, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            var queryable = await _employeeAttachmentRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var employeeAttachment = await AsyncExecuter.FirstOrDefaultAsync(query);

            employeeAttachment.EmployeeProfileId = employeeProfileId;
            employeeAttachment.url = url;
            employeeAttachment.Description = description;
            employeeAttachment.Active = active;

            employeeAttachment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _employeeAttachmentRepository.UpdateAsync(employeeAttachment);
        }

    }
}