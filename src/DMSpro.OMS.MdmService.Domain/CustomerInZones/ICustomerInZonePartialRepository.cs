using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using System.Linq;
namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public partial interface ICustomerInZoneRepository
    {
        Task<IQueryable<CustomerInZoneWithNavigationProperties>> GetQueryableWithNavigationPropertiesAsync();
    }
	public partial interface ICustomerInZoneRepository
	{
		Task<List<CustomerInZone>> GetByIdAsync(List<Guid> ids);
    }
}