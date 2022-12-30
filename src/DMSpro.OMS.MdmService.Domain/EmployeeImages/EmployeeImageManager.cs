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
        Guid employeeProfileId, string description, string url, bool active, bool isAvatar)
        {
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            var employeeImage = new EmployeeImage(
             GuidGenerator.Create(),
             employeeProfileId, description, url, active, isAvatar
             );

            return await _employeeImageRepository.InsertAsync(employeeImage);
        }

        public async Task<EmployeeImage> UpdateAsync(
            Guid id,
            Guid employeeProfileId, string description, string url, bool active, bool isAvatar, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            var queryable = await _employeeImageRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var employeeImage = await AsyncExecuter.FirstOrDefaultAsync(query);

            employeeImage.EmployeeProfileId = employeeProfileId;
            employeeImage.Description = description;
            employeeImage.url = url;
            employeeImage.Active = active;
            employeeImage.IsAvatar = isAvatar;

            employeeImage.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _employeeImageRepository.UpdateAsync(employeeImage);
        }

    }
}