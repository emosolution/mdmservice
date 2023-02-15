using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
	public partial interface ICompanyInZoneRepository
	{
		Task<List<CompanyInZone>> GetByIdAsync(List<Guid> ids);
    }
}