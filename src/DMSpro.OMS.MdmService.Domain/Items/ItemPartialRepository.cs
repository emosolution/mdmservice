using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Items
{
	public partial interface IItemRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}