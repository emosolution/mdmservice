using System;
using System.Threading.Tasks;
using System.Linq;
namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
	public partial interface IUOMGroupDetailRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);

		Task<IQueryable<UOMGroupDetailWithNavigationProperties>> GetQueryAbleForNavigationPropertiesAsync();
	}
}