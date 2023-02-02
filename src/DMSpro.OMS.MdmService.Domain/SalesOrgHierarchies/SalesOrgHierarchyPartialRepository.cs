using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
	public partial interface ISalesOrgHierarchyRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}