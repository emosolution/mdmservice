using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
	public partial interface ICustomerAttributeRepository : IRepository<CustomerAttribute, Guid>
    {
        Task<Guid?> GetIdByCodeAsync(string code);

        Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);

        Task<int> GetCountByCodeAsync(List<string> codes);

        Task<bool> CheckUniqueCodeForUpdate(List<string> codes, List<Guid> ids);

        Task<List<CustomerAttribute>> GetByIdAsync(List<Guid> ids);
    }
}