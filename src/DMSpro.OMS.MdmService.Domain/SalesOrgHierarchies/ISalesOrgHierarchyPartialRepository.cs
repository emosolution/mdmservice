using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
	public partial interface ISalesOrgHierarchyRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);

		Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);

		Task<int> GetCountByCodeAsync(List<string> codes);

		Task<bool> CheckUniqueCodeForUpdate(List<string> codes, List<Guid> ids);

		Task<List<SalesOrgHierarchy>> GetByIdAsync(List<Guid> ids);
	}
}