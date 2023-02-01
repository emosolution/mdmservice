using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Companies
{
	public partial interface ICompanyRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}