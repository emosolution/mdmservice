using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemGroups
{
	public partial interface IItemGroupRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}