using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.Shared.Protos.IdentityService.IdentityUsers;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Volo.Abp.MultiTenancy;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.IdentityUsers
{
    public class IdentityUsersAppService : ApplicationService
    {
        private readonly IConfiguration _settingProvider;
        private readonly ICurrentTenant _currentTenant;

        public IdentityUsersAppService(
            IConfiguration settingProvider,
            ICurrentTenant currentTenant
            )
        {
            _settingProvider = settingProvider;
            _currentTenant = currentTenant;
        }

        public virtual async Task<List<IdentityUser>> GetListByIds(List<Guid> ids)
        {
            GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:IdentiyServiceUrl"]);
            string tenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString();
            GetListIdentityUsersRequest request = new()
            {
                TenantId = tenantId,
            };
            foreach (Guid id in ids)
            {
                request.IdentityUserIds.Add(id.ToString());
            }
            var client = new IdentityUsersProtoAppService.IdentityUsersProtoAppServiceClient(channel);
            GetListIdentityUsersResponse response = await client.GetListIdentityUsersAsync(request);
            return response.IdentityUsers.ToList();
        }
    }
}