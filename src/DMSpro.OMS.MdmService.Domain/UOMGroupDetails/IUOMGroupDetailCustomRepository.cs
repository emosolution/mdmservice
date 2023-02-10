using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public partial interface IUOMGroupDetailRepository
    {
        Task<IQueryable<UOMGroupDetailWithNavigationProperties>> GetQueryAbleForNavigationPropertiesAsync();
    }
}