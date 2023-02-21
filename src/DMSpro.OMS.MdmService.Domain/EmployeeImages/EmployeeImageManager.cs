using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImageManager : DomainService
    {
        private readonly IEmployeeImageRepository _employeeImageRepository;

        public EmployeeImageManager(IEmployeeImageRepository employeeImageRepository)
        {
            _employeeImageRepository = employeeImageRepository;
        }

        public async Task<EmployeeImage> CreateAsync(
        Guid employeeProfileId, string description, bool active, bool isAvatar, Guid fileId)
        {
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));

            var employeeImage = new EmployeeImage(
             GuidGenerator.Create(),
             employeeProfileId, description, active, isAvatar, fileId
             );

            return await _employeeImageRepository.InsertAsync(employeeImage);
        }

        public async Task<EmployeeImage> UpdateAsync(
            Guid id,
            Guid employeeProfileId, string description, bool active, bool isAvatar, Guid fileId, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));

            var employeeImage = await _employeeImageRepository.GetAsync(id);

            employeeImage.EmployeeProfileId = employeeProfileId;
            employeeImage.Description = description;
            employeeImage.Active = active;
            employeeImage.IsAvatar = isAvatar;
            employeeImage.FileId = fileId;

            employeeImage.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _employeeImageRepository.UpdateAsync(employeeImage);
        }

    }
}