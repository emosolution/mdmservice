using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.GeoMasters
{
	public partial interface IGeoMasterRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}