using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentManager : DomainService
    {
        private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;

        public SalesOrgEmpAssignmentManager(ISalesOrgEmpAssignmentRepository salesOrgEmpAssignmentRepository)
        {
            _salesOrgEmpAssignmentRepository = salesOrgEmpAssignmentRepository;
        }

        public async Task<SalesOrgEmpAssignment> CreateAsync(
        Guid salesOrgHierarchyId, Guid employeeProfileId, bool isBase, DateTime effectiveDate, string hierarchyCode, DateTime? endDate = null)
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.Length(hierarchyCode, nameof(hierarchyCode), SalesOrgEmpAssignmentConsts.HierarchyCodeMaxLength);

            var salesOrgEmpAssignment = new SalesOrgEmpAssignment(
             GuidGenerator.Create(),
             salesOrgHierarchyId, employeeProfileId, isBase, effectiveDate, hierarchyCode, endDate
             );

            return await _salesOrgEmpAssignmentRepository.InsertAsync(salesOrgEmpAssignment);
        }

        public async Task<SalesOrgEmpAssignment> UpdateAsync(
            Guid id,
            Guid salesOrgHierarchyId, Guid employeeProfileId, bool isBase, DateTime effectiveDate, string hierarchyCode, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(salesOrgHierarchyId, nameof(salesOrgHierarchyId));
            Check.NotNull(employeeProfileId, nameof(employeeProfileId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.Length(hierarchyCode, nameof(hierarchyCode), SalesOrgEmpAssignmentConsts.HierarchyCodeMaxLength);

            var salesOrgEmpAssignment = await _salesOrgEmpAssignmentRepository.GetAsync(id);

            salesOrgEmpAssignment.SalesOrgHierarchyId = salesOrgHierarchyId;
            salesOrgEmpAssignment.EmployeeProfileId = employeeProfileId;
            salesOrgEmpAssignment.IsBase = isBase;
            salesOrgEmpAssignment.EffectiveDate = effectiveDate;
            salesOrgEmpAssignment.HierarchyCode = hierarchyCode;
            salesOrgEmpAssignment.EndDate = endDate;

            salesOrgEmpAssignment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _salesOrgEmpAssignmentRepository.UpdateAsync(salesOrgEmpAssignment);
        }

    }
}