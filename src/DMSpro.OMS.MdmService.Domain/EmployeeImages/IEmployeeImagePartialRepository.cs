using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
	public partial interface IEmployeeImageRepository
	{
		Task<List<EmployeeImage>> GetByIdAsync(List<Guid> ids);
    }
}