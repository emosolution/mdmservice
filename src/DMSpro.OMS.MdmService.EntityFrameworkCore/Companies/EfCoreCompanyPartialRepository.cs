using System;
using System.Linq;
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
    }
}