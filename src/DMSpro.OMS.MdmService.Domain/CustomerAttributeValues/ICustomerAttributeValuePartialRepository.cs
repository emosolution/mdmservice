using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public interface ICustomerAttributeValueRepository : IRepository<CustomerAttributeValue, Guid>
    {
        Task<Guid?> GetIdByCodeAsync(string code);

        Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);

        Task<int> GetCountByCodeAsync(List<string> codes);

        Task<bool> CheckUniqueCodeForUpdate(List<string> codes, List<Guid> ids);

        Task<List<CustomerAttributeValue>> GetByIdAsync(List<Guid> ids);
    }
}