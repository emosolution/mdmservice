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
        Guid salesOrgHierarchyId, Guid companyId, Guid itemGroupId, DateTime effectiveDate, DateTime? endDate = null)
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var companyInZone = new CompanyInZone(
             GuidGenerator.Create(),
             salesOrgHierarchyId, companyId, itemGroupId, effectiveDate, endDate
             );

            return await _companyInZoneRepository.InsertAsync(companyInZone);
        }

        public async Task<CompanyInZone> UpdateAsync(
            Guid id,
            Guid salesOrgHierarchyId, Guid companyId, Guid itemGroupId, DateTime effectiveDate, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var companyInZone = await _companyInZoneRepository.GetAsync(id);

            companyInZone.SalesOrgHierarchyId = salesOrgHierarchyId;
            companyInZone.CompanyId = companyId;
            companyInZone.ItemGroupId = itemGroupId;
            companyInZone.EffectiveDate = effectiveDate;
            companyInZone.EndDate = endDate;

            companyInZone.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _companyInZoneRepository.UpdateAsync(companyInZone);
        }

    }
}