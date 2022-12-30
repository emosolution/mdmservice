using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SSHistoryInZones
{
    public class SSHistoryInZoneManager : DomainService
    {
        private readonly ISSHistoryInZoneRepository _sSHistoryInZoneRepository;

        public SSHistoryInZoneManager(ISSHistoryInZoneRepository sSHistoryInZoneRepository)
        {
            _sSHistoryInZoneRepository = sSHistoryInZoneRepository;
        }

        public async Task<SSHistoryInZone> CreateAsync(
        Guid salesOrgHierarchyId, Guid employeeId, DateTime effectiveDate, DateTime endDate)
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.NotNull(endDate, nameof(endDate));

            var ssHistoryInZone = new SSHistoryInZone(
             GuidGenerator.Create(),
             salesOrgHierarchyId, employeeId, effectiveDate, endDate
             );

            return await _sSHistoryInZoneRepository.InsertAsync(ssHistoryInZone);
        }

        public async Task<SSHistoryInZone> UpdateAsync(
            Guid id,
            Guid salesOrgHierarchyId, Guid employeeId, DateTime effectiveDate, DateTime endDate, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.NotNull(endDate, nameof(endDate));

            var queryable = await _sSHistoryInZoneRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var ssHistoryInZone = await AsyncExecuter.FirstOrDefaultAsync(query);

            ssHistoryInZone.SalesOrgHierarchyId = salesOrgHierarchyId;
            ssHistoryInZone.EmployeeId = employeeId;
            ssHistoryInZone.EffectiveDate = effectiveDate;
            ssHistoryInZone.EndDate = endDate;

            ssHistoryInZone.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _sSHistoryInZoneRepository.UpdateAsync(ssHistoryInZone);
        }

    }
}