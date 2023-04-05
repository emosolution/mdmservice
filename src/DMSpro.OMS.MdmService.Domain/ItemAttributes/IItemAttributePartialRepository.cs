using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
	public partial interface IItemAttributeRepository : IRepository<ItemAttribute, Guid>
    {
        Task<Guid?> GetIdByCodeAsync(string code);

        Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);

        Task<int> GetCountByCodeAsync(List<string> codes);

        Task<bool> CheckUniqueCodeForUpdate(List<string> codes, List<Guid> ids);

        Task<List<ItemAttribute>> GetByIdAsync(List<Guid> ids);
    }
}