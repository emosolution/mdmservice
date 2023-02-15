using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
