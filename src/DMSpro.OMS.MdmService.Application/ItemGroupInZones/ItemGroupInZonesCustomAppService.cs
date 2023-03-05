using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using System.Threading.Tasks;
using System.Linq;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    [Authorize(MdmServicePermissions.ItemGroupInZones.Default)]
    public partial class ItemGroupInZonesAppService
    {
        public virtual async Task<LoadResult> GetListDevextremesWithNavigationAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _itemGroupInZoneRepository.GetQueryableWithNavigationPropertiesAsync();
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            results.data = ObjectMapper.Map<IEnumerable<ItemGroupInZone>, IEnumerable<ItemGroupInZoneDto>>(results.data.Cast<ItemGroupInZone>());
            return results;
        }
    }
}