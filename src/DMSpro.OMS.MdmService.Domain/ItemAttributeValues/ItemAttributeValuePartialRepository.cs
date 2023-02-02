using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
	public partial interface IItemAttributeValueRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}