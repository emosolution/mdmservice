using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public partial class MCPHeadersAppService
    {
        [Authorize(MdmServicePermissions.MCPHeaders.Edit)]
        public virtual async Task SetEndDate(Guid id, DateTime endDate)
        {
            DateTime today = DateTime.Now.Date;
            if (endDate.Date <= today)
            {
                throw new BusinessException(message: L["Error:MCPHeader:550"], code: "0");
            }

            MCPHeader header = await _repository.GetAsync(x => x.Id == id);
            header.EndDate = endDate;
            await _repository.UpdateAsync(header);
            DateTime visitDateToDelete = endDate.Date.AddDays(1);

            var idsOfDetailsInHeader = (await _mCPDetailRepository.GetQueryableAsync())
                .Where(x => x.MCPHeaderId == id)
                .Select(x => x.Id).ToList();
            
             var visitPlansToBeDeleted = (await _visitPlanRepository.GetQueryableAsync()).
                Where(x => idsOfDetailsInHeader.Contains(x.Id) && x.DateVisit >= visitDateToDelete).ToList();
            await _visitPlanRepository.DeleteManyAsync(visitPlansToBeDeleted);
        }
    }
}
