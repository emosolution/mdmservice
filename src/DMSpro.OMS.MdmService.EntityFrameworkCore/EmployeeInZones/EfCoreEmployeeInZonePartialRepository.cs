using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
	public partial class EfCoreEmployeeInZoneRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}