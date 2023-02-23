using System.Collections.Generic;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using System.Threading.Tasks;
using DMSpro.OMS.Shared.Lib.Parser;
using System;
using System.Data;
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