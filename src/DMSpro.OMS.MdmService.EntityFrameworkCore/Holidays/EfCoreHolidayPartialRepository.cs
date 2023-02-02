using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Holidays
{
	public partial class EfCoreHolidayRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}