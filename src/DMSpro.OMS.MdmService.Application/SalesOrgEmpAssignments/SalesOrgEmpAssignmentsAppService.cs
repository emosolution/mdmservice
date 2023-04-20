using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using System.Linq;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{

    [Authorize(MdmServicePermissions.SalesOrgEmpAssignments.Default)]
    public partial class SalesOrgEmpAssignmentsAppService
    {
        public virtual async Task<SalesOrgEmpAssignmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgEmpAssignment, SalesOrgEmpAssignmentDto>(await _salesOrgEmpAssignmentRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SalesOrgEmpAssignments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _salesOrgEmpAssignmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SalesOrgEmpAssignments.Create)]
        public virtual async Task<SalesOrgEmpAssignmentDto> CreateAsync(SalesOrgEmpAssignmentCreateDto input)
        {
            Check.NotNull(input.SalesOrgHierarchyId, nameof(input.SalesOrgHierarchyId));
            Check.NotNull(input.EmployeeProfileId, nameof(input.EmployeeProfileId));
            Check.NotNull(input.EffectiveDate, nameof(input.EffectiveDate));

            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            var salesOrgHierarchy = await _salesOrgHierarchyRepository.GetAsync(input.SalesOrgHierarchyId);
            Check.Length(salesOrgHierarchy.HierarchyCode, nameof(salesOrgHierarchy.HierarchyCode),
                SalesOrgEmpAssignmentConsts.HierarchyCodeMaxLength);
            if (input.EmployeeProfileId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }
            await _employeeProfileRepository.GetAsync(input.EmployeeProfileId);

            CheckEffectiveDate(input.EffectiveDate, input.EndDate);
            await CheckEmployeeASsignmentPeriod(input.EmployeeProfileId, input.SalesOrgHierarchyId,
                input.EffectiveDate, input.EndDate, null);
            await HandleIsBase(input.IsBase, input.SalesOrgHierarchyId);

            var salesOrgEmpAssignment = new SalesOrgEmpAssignment(
                GuidGenerator.Create(),
                input.SalesOrgHierarchyId, input.EmployeeProfileId, input.IsBase,
                input.EffectiveDate, salesOrgHierarchy.HierarchyCode, input.EndDate);
            await _salesOrgEmpAssignmentRepository.InsertAsync(salesOrgEmpAssignment);
            return ObjectMapper.Map<SalesOrgEmpAssignment, SalesOrgEmpAssignmentDto>(salesOrgEmpAssignment);
        }

        [Authorize(MdmServicePermissions.SalesOrgEmpAssignments.Edit)]
        public virtual async Task<SalesOrgEmpAssignmentDto> UpdateAsync(Guid id, SalesOrgEmpAssignmentUpdateDto input)
        {
            Check.NotNull(input.EffectiveDate, nameof(input.EffectiveDate));
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);

            var assignment = await _salesOrgEmpAssignmentRepository.GetAsync(id);
            await CheckEmployeeASsignmentPeriod(assignment.EmployeeProfileId, assignment.SalesOrgHierarchyId,
                input.EffectiveDate, input.EndDate, id);
            await HandleIsBase(input.IsBase, assignment.SalesOrgHierarchyId);
            assignment.IsBase = input.IsBase;
            assignment.EffectiveDate = input.EffectiveDate;
            assignment.EndDate = input.EndDate;
            assignment.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _salesOrgEmpAssignmentRepository.UpdateAsync(assignment);

            return ObjectMapper.Map<SalesOrgEmpAssignment, SalesOrgEmpAssignmentDto>(assignment);
        }

        private async Task CheckEmployeeASsignmentPeriod(Guid employeeId, Guid salesOrgHierarchyId,
            DateTime effectiveDate, DateTime? endDate, Guid? id)
        {
            var assignments = await _salesOrgEmpAssignmentRepository.GetListAsync(
                x => x.EmployeeProfileId == employeeId &&
                x.SalesOrgHierarchyId == salesOrgHierarchyId &&
                x.Id != id);
            if (endDate == null && (assignments.Any(x => x.EndDate == null || (x.EndDate != null && x.EndDate >= effectiveDate))))
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgEmpAssignmentsAppService:550"], code: "0");
            }
            if (assignments.Any(x => (x.EndDate == null && x.EffectiveDate <= endDate) ||
                (x.EndDate != null &&
                    (x.EffectiveDate >= effectiveDate && x.EffectiveDate <= endDate) ||
                    (x.EndDate >= effectiveDate && x.EndDate <= endDate) ||
                    (x.EffectiveDate <= effectiveDate && x.EndDate >= endDate))))
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgEmpAssignmentsAppService:550"], code: "0");
            }
        }

        private async Task HandleIsBase(bool isBase, Guid salesOrgHierarchyId)
        {
            if (!isBase)
            {
                return;
            }
            var assignments = await _salesOrgEmpAssignmentRepository.GetListAsync(
                x => x.SalesOrgHierarchyId == salesOrgHierarchyId);
            assignments.ForEach(x => { x.IsBase = false; });
            await _salesOrgEmpAssignmentRepository.UpdateManyAsync(assignments);
        }
    }
}