using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
	public partial interface IUOMGroupDetailRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}