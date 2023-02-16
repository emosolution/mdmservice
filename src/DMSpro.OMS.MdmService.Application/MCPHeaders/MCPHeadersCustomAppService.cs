using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public partial class MCPHeadersAppService
    {
        [Authorize(MdmServicePermissions.MCPs.Edit)]
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

        [Authorize(MdmServicePermissions.MCPs.Create)]
        public virtual async Task<MCPDto> CreateMCP(MCPCreateDto mcpCreateDto)
        {
            MCPHeaderCreateDto headerCreateDto = mcpCreateDto.MCPHeaderDto;
            MCPHeader header = await _mCPHeaderManager.CreateAsync(
                #region INPUT PARAMS
                routeId: headerCreateDto.RouteId,
                companyId: headerCreateDto.CompanyId,
                itemGroupId: headerCreateDto.ItemGroupId,
                code: headerCreateDto.Code,
                name: headerCreateDto.Name,
                effectiveDate: headerCreateDto.EffectiveDate,
                endDate: headerCreateDto.EffectiveDate
                #endregion
            );

            List<MCPDetail> details = new();
            List<MCPDetailDto> detailDtos = new();
            foreach (MCPDetailCreateDto mcpDetailCreateDto in mcpCreateDto.MCPDetails)
            {
                MCPDetail detail = new()
                {
                    #region INPUT PARAMS
                    Code = mcpDetailCreateDto.Code,
                    EffectiveDate = mcpDetailCreateDto.EffectiveDate,
                    EndDate = mcpDetailCreateDto.EndDate,
                    Distance = mcpDetailCreateDto.Distance,
                    VisitOrder = mcpDetailCreateDto.VisitOrder,
                    Monday = mcpDetailCreateDto.Monday,
                    Tuesday = mcpDetailCreateDto.Tuesday,
                    Wednesday = mcpDetailCreateDto.Wednesday,
                    Thursday = mcpDetailCreateDto.Thursday,
                    Friday = mcpDetailCreateDto.Friday,
                    Saturday = mcpDetailCreateDto.Saturday,
                    Sunday = mcpDetailCreateDto.Sunday,
                    Week1 = mcpDetailCreateDto.Week1,
                    Week2 = mcpDetailCreateDto.Week2,
                    Week3 = mcpDetailCreateDto.Week3,
                    Week4 = mcpDetailCreateDto.Week4,
                    CustomerId = mcpDetailCreateDto.CustomerId,
                    MCPHeaderId = header.Id,
                    #endregion
                };
                detail.SetId(GuidGenerator.Create());
                details.Add(detail);
                MCPDetailDto detailDto = ObjectMapper.Map<MCPDetail, MCPDetailDto>(detail);
                detailDtos.Add(detailDto);
            }
            await _mCPDetailRepository.InsertManyAsync(details);
            MCPDto mcpDto = new()
            {
                MCPHeaderDto = ObjectMapper.Map<MCPHeader, MCPHeaderDto>(header),
                MCPDetails = detailDtos,
            };
            return mcpDto;
        }
    }
}
