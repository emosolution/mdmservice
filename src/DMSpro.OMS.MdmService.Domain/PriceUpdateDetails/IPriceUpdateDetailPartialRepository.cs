using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
	public partial interface IPriceUpdateDetailRepository
	{
		Task<List<PriceUpdateDetail>> GetByIdAsync(List<Guid> ids);
    }
}