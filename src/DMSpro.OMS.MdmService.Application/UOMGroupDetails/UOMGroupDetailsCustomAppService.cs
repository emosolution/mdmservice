using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public partial class UOMGroupDetailsAppService
    {
        public virtual async Task<LoadResult> GetListDevextremeswithNavigationAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _uOMGroupDetailRepository.GetQueryAbleForNavigationPropertiesAsync();
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            results.data = ObjectMapper.Map<IEnumerable<UOMGroupDetailWithNavigationProperties>, 
                IEnumerable<UOMGroupDetailWithNavigationPropertiesDto>>(results.data.Cast<UOMGroupDetailWithNavigationProperties>());

            return results;

        }
    }
}