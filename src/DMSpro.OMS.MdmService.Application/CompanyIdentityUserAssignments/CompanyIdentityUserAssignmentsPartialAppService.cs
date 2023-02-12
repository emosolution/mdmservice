using System.Collections.Generic;
using DMSpro.OMS.MdmService.Companies;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using System.Threading.Tasks;
using System.Linq;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial class CompanyIdentityUserAssignmentsAppService :
        PartialAppService<CompanyIdentityUserAssignment, CompanyIdentityUserAssignmentDto, ICompanyIdentityUserAssignmentRepository>,
        ICompanyIdentityUserAssignmentsAppService
    {
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;
        private readonly IDistributedCache<CompanyIdentityUserAssignmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly CompanyIdentityUserAssignmentManager _companyIdentityUserAssignmentManager;
        private readonly ICompanyRepository _companyRepository;

        public CompanyIdentityUserAssignmentsAppService(ICurrentTenant currentTenant,
            ICompanyIdentityUserAssignmentRepository repository,
            CompanyIdentityUserAssignmentManager companyIdentityUserAssignmentManager,
            IConfiguration settingProvider,
            ICompanyRepository companyRepository,
            IDistributedCache<CompanyIdentityUserAssignmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _companyIdentityUserAssignmentRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _companyIdentityUserAssignmentManager = companyIdentityUserAssignmentManager;
            _companyRepository = companyRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        }

        public override async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _companyIdentityUserAssignmentRepository.GetQueryAbleForNavigationPropertiesAsync(null);
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            if (inputDev.Group == null)
            {
                results.data = ObjectMapper.Map<IEnumerable<CompanyIdentityUserAssignmentWithNavigationProperties>, IEnumerable<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>>(results.data.Cast<CompanyIdentityUserAssignmentWithNavigationProperties>());
            }
            return results;
        }
	}
}