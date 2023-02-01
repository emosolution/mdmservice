using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
	public partial class EfCoreHolidayDetailRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}