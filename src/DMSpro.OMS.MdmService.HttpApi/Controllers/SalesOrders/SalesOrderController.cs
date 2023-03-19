using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;
using DMSpro.OMS.MdmService.SalesOrders;
using System.Threading.Tasks;
using System;

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
        public virtual async Task<string> GetInfoSOAsync(GetInfoSODto input)
        {
            try
            {
                return await _salesOrdersAppService.GetInfoSOAsync(input);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }
    }
}
