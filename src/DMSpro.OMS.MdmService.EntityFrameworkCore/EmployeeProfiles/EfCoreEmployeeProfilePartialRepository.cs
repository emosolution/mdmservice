using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
	public partial class EfCoreEmployeeProfileRepository
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
    }
}