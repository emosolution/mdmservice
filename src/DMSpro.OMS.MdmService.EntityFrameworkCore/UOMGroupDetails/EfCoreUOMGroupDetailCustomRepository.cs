using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public partial class EfCoreUOMGroupDetailRepository
    {
        public virtual async Task<IQueryable<UOMGroupDetailWithNavigationProperties>> GetQueryAbleForNavigationPropertiesAsync()
        {
            return from uOMGroupDetail in (await GetDbSetAsync())
                   join uOMGroup in (await GetDbContextAsync()).UOMGroups on uOMGroupDetail.UOMGroupId equals uOMGroup.Id into uOMGroups
                   from uOMGroup in uOMGroups.DefaultIfEmpty()
                   join altUOM in (await GetDbContextAsync()).UOMs on uOMGroupDetail.AltUOMId equals altUOM.Id into uOMs
                   from altUOM in uOMs.DefaultIfEmpty()
                   join baseUOM in (await GetDbContextAsync()).UOMs on uOMGroupDetail.BaseUOMId equals baseUOM.Id into uOMs1
                   from baseUOM in uOMs1.DefaultIfEmpty()

                   select new UOMGroupDetailWithNavigationProperties
                   {
                       UOMGroupDetail = uOMGroupDetail,
                       UOMGroup = uOMGroup,
                       AltUOM = altUOM,
                       BaseUOM = baseUOM
                   };
        }
    }
}