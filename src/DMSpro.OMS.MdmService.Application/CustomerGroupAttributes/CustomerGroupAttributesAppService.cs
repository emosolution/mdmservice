using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Caching;
using static DMSpro.OMS.MdmService.Permissions.MdmServicePermissions;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public class CustomerGroupAttributesAppService : ApplicationService, ICustomerGroupAttributesAppService
    {
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;
        private readonly CustomerGroupAttributeManager _customerGroupAttributeManager;

        public CustomerGroupAttributesAppService(ICustomerGroupAttributeRepository customerGroupAttributeRepository, CustomerGroupAttributeManager customerGroupAttributeManager, IDistributedCache<CustomerGroupAttributeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<CustomerGroup, Guid> customerGroupRepository, IRepository<CustomerAttributeValue, Guid> customerAttributeValueRepository)
        {
            _customerGroupAttributeRepository = customerGroupAttributeRepository;
            _customerGroupAttributeManager = customerGroupAttributeManager; 
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerGroupAttributeRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Create)]
        public virtual async Task<CustomerGroupAttributeDto> CreateAsync(CustomerGroupAttributeCreateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }

            var customerGroupAttribute = await _customerGroupAttributeManager.CreateAsync(
            input.CustomerGroupId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Description
            );

            return ObjectMapper.Map<CustomerGroupAttribute, CustomerGroupAttributeDto>(customerGroupAttribute);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Edit)]
        public virtual async Task<CustomerGroupAttributeDto> UpdateAsync(Guid id, CustomerGroupAttributeUpdateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }

            var customerGroupAttribute = await _customerGroupAttributeManager.UpdateAsync(
            id,
            input.CustomerGroupId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerGroupAttribute, CustomerGroupAttributeDto>(customerGroupAttribute);
        }

        public async Task<CustomerGroupAttributeDto> GetAsync(Guid id)
        {
            var record = await _customerGroupAttributeRepository.GetAsync(x => x.Id == id);
            return ObjectMapper.Map<CustomerGroupAttribute, CustomerGroupAttributeDto>(record);
        }
    }
}