using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
	public partial interface IItemGroupListRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}