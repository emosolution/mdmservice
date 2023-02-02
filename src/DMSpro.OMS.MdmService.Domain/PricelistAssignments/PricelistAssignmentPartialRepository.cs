using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
	public partial interface IPricelistAssignmentRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}