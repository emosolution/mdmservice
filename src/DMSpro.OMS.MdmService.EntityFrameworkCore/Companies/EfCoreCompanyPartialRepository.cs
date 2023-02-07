using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Companies
{
	public partial class EfCoreCompanyRepository
	{
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
                Guid id = item.Id;
                string code = item.Code;    
                result.Add(code, id);
            }
            return result;
        }
    }
}