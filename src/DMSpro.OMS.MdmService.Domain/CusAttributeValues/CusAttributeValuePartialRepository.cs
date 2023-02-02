using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
	public partial interface ICusAttributeValueRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}