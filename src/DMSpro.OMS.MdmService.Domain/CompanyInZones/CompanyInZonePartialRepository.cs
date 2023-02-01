using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
	public partial interface ICompanyInZoneRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}