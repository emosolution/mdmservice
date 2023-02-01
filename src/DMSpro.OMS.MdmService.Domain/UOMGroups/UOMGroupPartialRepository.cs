using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.UOMGroups
{
	public partial interface IUOMGroupRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}