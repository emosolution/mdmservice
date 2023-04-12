using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
	public partial interface IPriceUpdateDetailRepository : IRepository<PriceUpdateDetail, Guid>
    {
		Task<List<PriceUpdateDetail>> GetByIdAsync(List<Guid> ids);
    }
}