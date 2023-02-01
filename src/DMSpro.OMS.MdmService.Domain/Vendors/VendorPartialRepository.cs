using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Vendors
{
	public partial interface IVendorRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}