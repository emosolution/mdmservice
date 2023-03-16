using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SalesOrders
{
    public interface ISalesOrdersAppService : IApplicationService
    {
        Task<string> GetSOInfoAsync(Guid companyId,
            DateTime postingDate, DateTime? lastUpdateDate);
    }
}
