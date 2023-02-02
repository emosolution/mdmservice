using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.Items;
using System.Collections.Generic;
using System.Linq;

namespace DMSpro.OMS.MdmService.Items;

public class ItemsGRPCAppService : ItemsProtoAppService.ItemsProtoAppServiceBase
{
    readonly IItemRepository _repository;

    public ItemsGRPCAppService(IItemRepository repository)
    {
        _repository = repository;
    }

    public override async Task<ListItemResponse> GetListItem(GetListItemRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        List<DMSpro.OMS.Shared.Protos.MdmService.Items.Item> Items = new();
        IQueryable<Item> queryable = await _repository.GetQueryableAsync();
        var query = from item in queryable
                    where request.ItemIds.Contains(item.Id.ToString()) && 
                        item.TenantId == tenantId
                    select item;
        List<Item> entities = query.ToList<Item>();
        foreach (Item entity in entities)
        {
            Items.Add(new DMSpro.OMS.Shared.Protos.MdmService.Items.Item()
            {
                ItemId = entity.Id.ToString(),
                TenantId = request.TenantId,
                Code = entity.Code,
                Name = entity.Name,
                ShortName = string.IsNullOrEmpty(entity.ShortName) ? "" : entity.ShortName,
                Purchasable = entity.IsPurchasable,
                Salesable = entity.IsSaleable,
                Inventoriable = entity.IsInventoriable,
                VATId = entity.VatId.ToString(),
                UOMGroupID = entity.UomGroupId.ToString(),
                InventoryUOMId = entity.InventoryUOMId.ToString(),
                PurUOMId = entity.PurUOMId.ToString(),
                SalesUOMId = entity.SalesUOMId.ToString(),
                BasePrice = (float)entity.BasePrice,
                Active = entity.Active,
            });
        }
        ListItemResponse response = new();
        response.Items.Add(Items);
        return response;
    }
}