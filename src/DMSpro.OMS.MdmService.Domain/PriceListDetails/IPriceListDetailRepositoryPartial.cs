using System;
using System.Threading.Tasks;
using System.Linq;
namespace DMSpro.OMS.MdmService.PriceListDetails
{
	public partial interface IPriceListDetailRepository
	{
		Task<IQueryable<PriceListDetailWithNavigationProperties>> GetQueryAbleForNavigationPropertiesAsync();
	}
}
