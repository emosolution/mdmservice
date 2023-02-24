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
        public virtual async Task SetEndDateAsync(Guid id, DateTime endDate)
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
        public virtual async Task<MCPDto> CreateMCPAsync(MCPCreateDto mcpCreateDto)
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
                endDate: headerCreateDto.EndDate
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

        [Authorize(MdmServicePermissions.MCPs.Edit)]
        public virtual async Task<MCPDto> UpdateMCPAsync(Guid headerId, MCPUpdateDto mcpUpdateDto)
        {
            MCPHeaderUpdateDto headerUpdateDto = mcpUpdateDto.MCPHeaderDto;
            MCPHeader header = await _mCPHeaderManager.UpdateAsync(id: headerId,
                routeId: headerUpdateDto.RouteId,
                companyId: headerUpdateDto.CompanyId,
                itemGroupId: headerUpdateDto.ItemGroupId,
                code: headerUpdateDto.Code,
                name: headerUpdateDto.Name,
                effectiveDate: headerUpdateDto.EffectiveDate,
                endDate: headerUpdateDto.EndDate,
                concurrencyStamp: headerUpdateDto.ConcurrencyStamp);

            var queryable = (await _mCPDetailRepository.GetQueryableAsync()).Where(x => x.MCPHeaderId == headerId);
            List<Guid> currentDetailIds = queryable.Select(x => x.Id).ToList();
            Dictionary<Guid, MCPDetail> toBeDeleted = queryable.ToDictionary(x => x.Id);
            List<MCPDetail> toBeUpdated = new();
            List<MCPDetail> toBeCreated = new();
            List<MCPDetailDto> detailDtos = new();
            foreach (MCPDetailUpdateWithIdDto mcpDetailUpdateDto in mcpUpdateDto.MCPDetails)
            {
                Guid? id = mcpDetailUpdateDto.Id;
                if (id == null)
                {
                    MCPDetail detail = new()
                    {
                        #region INPUT PARAMS
                        Code = mcpDetailUpdateDto.Code,
                        EffectiveDate = mcpDetailUpdateDto.EffectiveDate,
                        EndDate = mcpDetailUpdateDto.EndDate,
                        Distance = mcpDetailUpdateDto.Distance,
                        VisitOrder = mcpDetailUpdateDto.VisitOrder,
                        Monday = mcpDetailUpdateDto.Monday,
                        Tuesday = mcpDetailUpdateDto.Tuesday,
                        Wednesday = mcpDetailUpdateDto.Wednesday,
                        Thursday = mcpDetailUpdateDto.Thursday,
                        Friday = mcpDetailUpdateDto.Friday,
                        Saturday = mcpDetailUpdateDto.Saturday,
                        Sunday = mcpDetailUpdateDto.Sunday,
                        Week1 = mcpDetailUpdateDto.Week1,
                        Week2 = mcpDetailUpdateDto.Week2,
                        Week3 = mcpDetailUpdateDto.Week3,
                        Week4 = mcpDetailUpdateDto.Week4,
                        CustomerId = mcpDetailUpdateDto.CustomerId,
                        MCPHeaderId = headerId,
                        #endregion
                    };
                    detail.SetId(GuidGenerator.Create());
                    toBeCreated.Add(detail);
                    MCPDetailDto detailDto = ObjectMapper.Map<MCPDetail, MCPDetailDto>(detail);
                    detailDtos.Add(detailDto);
                }
                else if (currentDetailIds.Contains((Guid)id))
                {
                    #region INPUT PARAMS
                    var entity = toBeDeleted[(Guid)id];
                    entity.Code = mcpDetailUpdateDto.Code;
                    entity.EffectiveDate = mcpDetailUpdateDto.EffectiveDate;
                    entity.EndDate = mcpDetailUpdateDto.EndDate;
                    entity.Distance = mcpDetailUpdateDto.Distance;
                    entity.VisitOrder = mcpDetailUpdateDto.VisitOrder;
                    entity.Monday = mcpDetailUpdateDto.Monday;
                    entity.Tuesday = mcpDetailUpdateDto.Tuesday;
                    entity.Wednesday = mcpDetailUpdateDto.Wednesday;
                    entity.Thursday = mcpDetailUpdateDto.Thursday;
                    entity.Friday = mcpDetailUpdateDto.Friday;
                    entity.Saturday = mcpDetailUpdateDto.Saturday;
                    entity.Sunday = mcpDetailUpdateDto.Sunday;
                    entity.Week1 = mcpDetailUpdateDto.Week1;
                    entity.Week2 = mcpDetailUpdateDto.Week2;
                    entity.Week3 = mcpDetailUpdateDto.Week3;
                    entity.Week4 = mcpDetailUpdateDto.Week4;
                    entity.CustomerId = mcpDetailUpdateDto.CustomerId;
                    entity.MCPHeaderId = headerId;
                    #endregion
                    toBeUpdated.Add(entity);
                    toBeDeleted.Remove((Guid)id);
                    MCPDetailDto detailDto = ObjectMapper.Map<MCPDetail, MCPDetailDto>(entity);
                    detailDtos.Add(detailDto);
                }
            }
            await _mCPDetailRepository.DeleteManyAsync(toBeDeleted.Keys);
            await _mCPDetailRepository.InsertManyAsync(toBeCreated);
            await _mCPDetailRepository.UpdateManyAsync(toBeUpdated);

            MCPDto mcpDto = new()
            {
                MCPHeaderDto = ObjectMapper.Map<MCPHeader, MCPHeaderDto>(header),
                MCPDetails = detailDtos,
            };
            return mcpDto;
        }
    }
}
