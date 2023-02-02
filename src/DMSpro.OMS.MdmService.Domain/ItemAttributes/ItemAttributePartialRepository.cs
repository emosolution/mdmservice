using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
	public partial interface IItemAttributeRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}