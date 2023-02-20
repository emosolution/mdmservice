using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using System.Threading.Tasks;
using DMSpro.OMS.Shared.Lib.Parser;
using System;
using System.Data;
using System.IO;
using System.Linq;
namespace DMSpro.OMS.MdmService.CompanyInZones{
    public partial class CompanyInZonesAppService{
        public virtual async Task<LoadResult> GetListDevextremesWithNavigationAsync(DataLoadOptionDevextreme inputDev)
		{
			var items = await _companyInZoneRepository.GetQueryableWithNavigationPropertiesAsync();
			var base_dataloadoption = new DataSourceLoadOptionsBase();
			DataLoadParser.Parse(base_dataloadoption,inputDev);
			LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
			results.data = ObjectMapper.Map<IEnumerable<CompanyInZone>, IEnumerable<CompanyInZoneDto>>(results.data.Cast<CompanyInZone>());
			return results;
		}
    }
}