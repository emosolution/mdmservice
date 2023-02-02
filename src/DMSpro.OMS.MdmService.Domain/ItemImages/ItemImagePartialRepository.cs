using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemImages
{
	public partial interface IItemImageRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}