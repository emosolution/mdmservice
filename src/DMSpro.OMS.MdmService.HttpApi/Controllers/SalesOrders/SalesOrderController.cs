using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;
using DMSpro.OMS.MdmService.SalesOrders;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.SalesOrders
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SalesOrder")]
    [Route("api/mdm-service/sales-orders")]
    public class SalesOrderController : AbpController, ISalesOrdersAppService
    {
        private readonly ISalesOrdersAppService _salesOrdersAppService;
        public SalesOrderController(ISalesOrdersAppService salesOrdersAppService)
        {
            _salesOrdersAppService = salesOrdersAppService;
        }

        [HttpGet]
        [Route("info-so")]
        public virtual Task<string> GetInfoSOAsync(GetInfoSODto input)
        {
            return _salesOrdersAppService.GetInfoSOAsync(input);
        }
    }
}
