using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
	public partial interface IEmployeeImageRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}