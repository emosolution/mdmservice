using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public partial class EfCoreGeoMasterRepository
    {
        public async Task<Guid?> GetIdByCode(string code)
        {
            var geo = (await GetDbSetAsync()).Where(g => g.Code == code).FirstOrDefault();
            return geo == null ? null : geo.Id;
        }
    }
}