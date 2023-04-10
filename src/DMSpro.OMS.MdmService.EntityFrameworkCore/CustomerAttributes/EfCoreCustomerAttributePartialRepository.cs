using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
	public partial class EfCoreCustomerAttributeRepository : EfCoreRepository<MdmServiceDbContext, CustomerAttribute, Guid>, ICustomerAttributeRepository
    {
        public EfCoreCustomerAttributeRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<Guid?> GetIdByCodeAsync(string code)
        {
            var item = (await GetDbSetAsync()).Where(x => x.Code == code).FirstOrDefault();
            return item?.Id;
        }

        public virtual async Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes)
        {
            var items = (await GetDbSetAsync()).Where(x => codes.Contains(x.Code));
            Dictionary<string, Guid> result = new();
            if (items.Count() < 1)
            {
                return result;
            }
            foreach (var item in items)
            {
                if (result.ContainsKey(item.Code))
                {
                    throw new BusinessException(message: "Error:ImportHandler:570", code: "1");
                }
                Guid id = item.Id;
                string code = item.Code;
                result.Add(code, id);
            }
            return result;
        }

        public virtual async Task<int> GetCountByCodeAsync(List<string> codes)
        {
            var items = (await GetDbSetAsync()).Where(x => codes.Contains(x.Code));
            return items.Count();
        }

        public virtual async Task<bool> CheckUniqueCodeForUpdate(List<string> codes,
            List<Guid> ids)
        {
            var items = await (await GetDbSetAsync()).
            Where(x => codes.Contains(x.Code) && !ids.Contains(x.Id)).ToListAsync();
            return items.Count() <= 0 ? true : false;
        }

        public virtual async Task<List<CustomerAttribute>> GetByIdAsync(List<Guid> ids)
        {
            var items = (await GetDbSetAsync()).Where(x => ids.Contains(x.Id));
            return await items.ToListAsync();
        }
    }
}