using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public class EmployeeInZoneManager : DomainService
    {
        private readonly IEmployeeInZoneRepository _employeeInZoneRepository;

        public EmployeeInZoneManager(IEmployeeInZoneRepository employeeInZoneRepository)
        {
            _employeeInZoneRepository = employeeInZoneRepository;
        }

        public async Task<EmployeeInZone> CreateAsync(
        Guid salesOrgHierarchyId, Guid employeeId, DateTime effectiveDate, Guid? endDate = null)
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var employeeInZone = new EmployeeInZone(
             GuidGenerator.Create(),
             salesOrgHierarchyId, employeeId, effectiveDate, endDate
             );

            return await _employeeInZoneRepository.InsertAsync(employeeInZone);
        }

        public async Task<EmployeeInZone> UpdateAsync(
            Guid id,
            Guid salesOrgHierarchyId, Guid employeeId, DateTime effectiveDate, Guid? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var queryable = await _employeeInZoneRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var employeeInZone = await AsyncExecuter.FirstOrDefaultAsync(query);

            employeeInZone.SalesOrgHierarchyId = salesOrgHierarchyId;
            employeeInZone.EmployeeId = employeeId;
            employeeInZone.EffectiveDate = effectiveDate;
            employeeInZone.EndDate = endDate;

            employeeInZone.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _employeeInZoneRepository.UpdateAsync(employeeInZone);
        }

    }
}