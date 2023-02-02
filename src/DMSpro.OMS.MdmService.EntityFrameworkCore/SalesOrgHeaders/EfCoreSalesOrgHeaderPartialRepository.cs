using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
	public partial class EfCoreSalesOrgHeaderRepository
	{
		public virtual async Task<Guid?> GetIdByCodeAsync(string code)
        {
            var item = (await GetDbSetAsync()).Where(x => x.Code == code).FirstOrDefault();
            return item?.Id;
        }
    }
}