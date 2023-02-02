using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
	public partial class EfCoreEmployeeImageRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}