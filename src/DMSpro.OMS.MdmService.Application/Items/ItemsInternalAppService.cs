using DMSpro.OMS.MdmService.Localization;
using DMSpro.OMS.Shared.Protos.Shared.Items;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemsInternalAppService : ApplicationService, IItemsInternalAppService
    {
        private readonly IItemsAppService _itemsAppService;

        private readonly IConfiguration _settingProvider;
        private readonly ICurrentTenant _currentTenant;

        public ItemsInternalAppService(IItemsAppService itemsAppService,
            IConfiguration settingProvider,
            ICurrentTenant currentTenant)
        {
            _itemsAppService = itemsAppService;

            _settingProvider = settingProvider;
            _currentTenant = currentTenant;

            LocalizationResource = typeof(MdmServiceResource);
        }

        public virtual async Task<bool> CheckCanBeUpdatedAsync(Guid id)
        {
            var item = await _itemsAppService.GetAsync(id);
            if (!item.CanUpdate) { return false; }

            CheckItemUsedRequest request = new()
            {
                TenantId = _currentTenant.Id.ToString(),
                ItemId = id.ToString(),
            };
            using (GrpcChannel channel =
                GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:OrderServiceUrl"]))
            {
                var client =
                    new ItemsProtoAppService.ItemsProtoAppServiceClient(channel);
               
                var response = await client.CheckItemUsedInOrderServiceAsync(request);
                if (!response.Success)
                {
                    throw new UserFriendlyException(message: response.Error, code: "1");
                }
                if (response.ItemUsed)
                {
                    return false;
                }
            }

            using (GrpcChannel channel =
                GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:InventoryServiceUrl"]))
            {
                var client =
                    new ItemsProtoAppService.ItemsProtoAppServiceClient(channel);
                var response = await client.CheckItemUsedInInventoryServiceAsync(request);
                if (!response.Success)
                {
                    throw new UserFriendlyException(message: response.Error, code: "1");
                }
                if (response.ItemUsed)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
