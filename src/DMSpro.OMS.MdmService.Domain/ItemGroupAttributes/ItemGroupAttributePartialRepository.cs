using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
	public partial interface IItemGroupAttributeRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}