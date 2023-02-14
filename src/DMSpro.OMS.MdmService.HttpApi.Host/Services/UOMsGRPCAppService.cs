using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.UOMs;
using System.Collections.Generic;
using System.Linq;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.UOMs;

public class UOMsGRPCAppService : UOMsProtoAppService.UOMsProtoAppServiceBase
{
    private readonly IUOMRepository _repository;
    private readonly IUOMGroupRepository _groupRepository;
    private readonly IUOMGroupDetailRepository _detailRepository;
    private readonly ICurrentTenant _currentTenant;

    public UOMsGRPCAppService(IUOMRepository repository,
        IUOMGroupRepository groupRepository,
        IUOMGroupDetailRepository detailRepository,
        ICurrentTenant currentTenant)
    {
        _repository = repository;
        _groupRepository = groupRepository;
        _detailRepository = detailRepository;
        _currentTenant = currentTenant;
    }

    private async Task<List<DMSpro.OMS.Shared.Protos.MdmService.UOMs.UOM>> GetListUOMFromIds(
        IEnumerable<string> ids, string tenantIdString)
    {
        Guid? tenantId = string.IsNullOrEmpty(tenantIdString) ? null : new(tenantIdString);
        using (_currentTenant.Change(tenantId))
        {
            List<DMSpro.OMS.Shared.Protos.MdmService.UOMs.UOM> result = new();
            IQueryable<UOM> queryable = await _repository.GetQueryableAsync();
            var query = from item in queryable
                        where ids.Contains(item.Id.ToString()) &&
                            item.TenantId == tenantId
                        select item;
            List<UOM> entities = query.ToList();
            foreach (UOM entity in entities)
            {
                result.Add(new DMSpro.OMS.Shared.Protos.MdmService.UOMs.UOM()
                {
                    UOMId = entity.Id.ToString(),
                    TenantId = tenantIdString,
                    Code = entity.Code,
                    Name = entity.Name,
                });
            }
            return result;
        }
    }

    public override async Task<ListUOMResponse> GetListUOM(GetListUOMRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
            List<DMSpro.OMS.Shared.Protos.MdmService.UOMs.UOM> uoms = await GetListUOMFromIds(
            request.UOMIds, request.TenantId);
            ListUOMResponse response = new();
            response.UOMs.Add(uoms);
            return response;
        }
    }

    public override async Task<ListUOMGroupResponse> GetListUOMGroup(GetListUOMGroupRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
            IQueryable<MdmService.UOMGroups.UOMGroup> queryable = await _groupRepository.GetQueryableAsync();
            var query = from item in queryable
                        where request.UOMGroupIds.Contains(item.Id.ToString()) &&
                            item.TenantId == tenantId
                        select item;
            List<MdmService.UOMGroups.UOMGroup> groupEntities = query.ToList();
            ListUOMGroupResponse response = new();
            List<DMSpro.OMS.Shared.Protos.MdmService.UOMs.UOMGroup> groups = new();
            List<string> uomIds = new();
            foreach (var groupEntity in groupEntities)
            {
                DMSpro.OMS.Shared.Protos.MdmService.UOMs.UOMGroup group = new()
                {
                    UOMGroupId = groupEntity.Id.ToString(),
                    TenantId = request.TenantId,
                    Code = groupEntity.Code,
                    Name = groupEntity.Name,
                    BaseUOMDetailId = "",
                };
                IQueryable<MdmService.UOMGroupDetails.UOMGroupDetail> detailQueryable = await _detailRepository.GetQueryableAsync();
                var detailQuery = from item in detailQueryable
                                  where item.UOMGroupId == groupEntity.Id &&
                                    item.TenantId == tenantId
                                  select item;
                List<UOMGroupDetail> detailEntities = detailQuery.ToList();
                foreach (var detailEntity in detailEntities)
                {
                    UOMDetail detail = new()
                    {
                        UOMDetailId = detailEntity.Id.ToString(),
                        TenantId = request.TenantId,
                        AltQty = (int)detailEntity.AltQty,
                        AltUomId = detailEntity.AltUOMId.ToString(),
                        BaseQty = (int)detailEntity.BaseQty,
                        BaseUomId = detailEntity.BaseUOMId.ToString(),
                        Active = detailEntity.Active,
                        UOMGroupId = groupEntity.Id.ToString(),
                    };
                    if (detail.AltQty == 1 && detail.BaseQty == 1)
                    {
                        if (!string.IsNullOrEmpty(group.BaseUOMDetailId))
                        {
                            return response;
                        }
                        group.BaseUOMDetailId = detail.UOMDetailId;
                    }
                    group.UOMDetails.Add(detail);
                    if (!uomIds.Contains(detail.AltUomId))
                    {
                        uomIds.Add(detail.AltUomId);
                    }
                    if (!uomIds.Contains(detail.BaseUomId))
                    {
                        uomIds.Add(detail.BaseUomId);
                    }
                }
                groups.Add(group);
            }
            response.UOMGroups.Add(groups);
            List<DMSpro.OMS.Shared.Protos.MdmService.UOMs.UOM> uoms =
                await GetListUOMFromIds(uomIds, request.TenantId);
            response.UOMs.Add(uoms);
            return response;
        }
    }
}