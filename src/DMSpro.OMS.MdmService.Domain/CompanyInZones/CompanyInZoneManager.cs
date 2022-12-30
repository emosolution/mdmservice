using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public class CompanyInZoneManager : DomainService
    {
        private readonly ICompanyInZoneRepository _companyInZoneRepository;

        public CompanyInZoneManager(ICompanyInZoneRepository companyInZoneRepository)
        {
            _companyInZoneRepository = companyInZoneRepository;
        }

        public async Task<CompanyInZone> CreateAsync(
        Guid salesOrgHierarchyId, Guid companyId, DateTime effectiveDate, DateTime? endDate = null)
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var companyInZone = new CompanyInZone(
             GuidGenerator.Create(),
             salesOrgHierarchyId, companyId, effectiveDate, endDate
             );

            return await _companyInZoneRepository.InsertAsync(companyInZone);
        }

        public async Task<CompanyInZone> UpdateAsync(
            Guid id,
            Guid salesOrgHierarchyId, Guid companyId, DateTime effectiveDate, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var queryable = await _companyInZoneRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var companyInZone = await AsyncExecuter.FirstOrDefaultAsync(query);

            companyInZone.SalesOrgHierarchyId = salesOrgHierarchyId;
            companyInZone.CompanyId = companyId;
            companyInZone.EffectiveDate = effectiveDate;
            companyInZone.EndDate = endDate;

            companyInZone.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _companyInZoneRepository.UpdateAsync(companyInZone);
        }

    }
}