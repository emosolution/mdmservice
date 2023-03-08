using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Companies;
using System;
using Volo.Abp;
using System.ComponentModel.Design;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial class CompanyIdentityUserAssignmentsAppService
    {
        public virtual async Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _companyIdentityUserAssignmentRepository.GetQueryAbleForNavigationPropertiesAsync(CurrentUser.Id.Value);
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            results.data = ObjectMapper.Map<IEnumerable<CompanyIdentityUserAssignmentWithNavigationProperties>, 
                IEnumerable<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>>(
                results.data.Cast<CompanyIdentityUserAssignmentWithNavigationProperties>());
            return results;
        }

        public override async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _companyIdentityUserAssignmentRepository.GetQueryAbleForNavigationPropertiesAsync(null);
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            if (inputDev.Group == null)
            {
                results.data = ObjectMapper.Map<IEnumerable<CompanyIdentityUserAssignmentWithNavigationProperties>, 
                    IEnumerable<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>>(
                    results.data.Cast<CompanyIdentityUserAssignmentWithNavigationProperties>());
            }
            return results;
        }

        public virtual async Task<CompanyDto> SetCurrentlySelectedCompanyAsync(Guid companyId)
        {
            var assignments = await _companyIdentityUserAssignmentRepository.GetListAsync(x =>
                x.IdentityUserId == _currentUser.Id);
            var companies = assignments.Distinct().Select(x => x.CompanyId).ToList();
            if (!companies.Contains(companyId))
            {
                throw new BusinessException(message: L["Error:CompanyIdentityUserAssignment:550"], code: "1");
            }
            foreach (var assignment in assignments)
            {
                assignment.CurrentlySelected = false;
                if (assignment.CompanyId == companyId)
                {
                    assignment.CurrentlySelected = true;
                }
            }
            await _companyIdentityUserAssignmentRepository.UpdateManyAsync(assignments);
            var selectedCompany = await _companyRepository.GetAsync(companyId);
            return ObjectMapper.Map<Company, CompanyDto>(selectedCompany);
        }

        public virtual async Task<CompanyDto> GetCurrentlySelectedCompanyAsync()
        {
            var assignments = await _companyIdentityUserAssignmentRepository.GetListAsync(x =>
                x.IdentityUserId == _currentUser.Id);
            if (assignments.Count < 1)
            {
                throw new BusinessException(message: L["Error:CompanyIdentityUserAssignment:551"], code: "1");
            }
            assignments = assignments.OrderBy(x => x.CreationTime).ToList();
            foreach(var assignment in assignments) { 
                if (assignment.CurrentlySelected == true)
                {
                    var selectedCompany = 
                        await _companyRepository.GetAsync(assignment.CompanyId);
                    return ObjectMapper.Map<Company, CompanyDto>(selectedCompany);
                }
            }
            var lastAssignment = assignments.First();
            var lastCreatedCompany =
                        await _companyRepository.GetAsync(lastAssignment.CompanyId);
            return ObjectMapper.Map<Company, CompanyDto>(lastCreatedCompany);
        }
    }
}
