using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using System.Linq;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using System.Collections.Generic;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{

    [Authorize(MdmServicePermissions.CustomerAttributes.Default)]
    public partial class CustomerAttributesAppService
    {
        private readonly List<string> _reservedNames = CustomerAttributeConsts.GenerateReservedNames();

        public virtual async Task<CustomerAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAttribute, CustomerAttributeDto>(await _customerAttributeRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Delete)]
        public virtual async Task<LoadResult> DeleteAsync()
        {
            var lastAttribute = await GetLastActiveAttribute();
            if (lastAttribute == null)
            {
                throw new UserFriendlyException(message: L["Error:CustomerAttributesAppService:553"], code: "1");
            }
            if (!(await _customerAttributeValueRepository.AnyAsync(x => x.CustomerAttributeId == lastAttribute.Id)))
            {
                throw new UserFriendlyException(message: L["Error:CustomerAttributesAppService:552"], code: "0");
            }
            lastAttribute.Active = false;
            lastAttribute.AttrName = $"{CustomerAttributeConsts.DefaultAttributeNamePrefix}{lastAttribute.AttrNo}";
            await _customerAttributeRepository.UpdateAsync(lastAttribute);
            return await GetListDevextremesAsync();
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Create)]
        public virtual async Task<LoadResult> CreateAsync(CustomerAttributeCreateDto input)
        {
            await CheckNameUniqueness(input.AttrName);
            int attrNo = 0;
            var lastAttribute = await GetLastActiveAttribute();
            if (lastAttribute != null)
            {
                attrNo = lastAttribute.AttrNo + 1;
            }
            if (attrNo >= CustomerAttributeConsts.NumberOfAttribute)
            {
                throw new UserFriendlyException(message: L["Error:CustomerAttributesAppService:554"], code: "0");
            }
            var attribute = await _customerAttributeRepository.GetAsync(x => x.AttrNo == attrNo);
            attribute.AttrName = input.AttrName;
            attribute.Active = true;
            await _customerAttributeRepository.UpdateAsync(attribute);
            return await GetListDevextremesAsync();
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Edit)]
        public virtual async Task<LoadResult> UpdateAsync(Guid id, CustomerAttributeUpdateDto input)
        {
            await CheckNameUniqueness(input.AttrName, id);

            var attribute = await _customerAttributeRepository.GetAsync(id);
            attribute.AttrName = input.AttrName;
            attribute.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _customerAttributeRepository.UpdateAsync(attribute);
            return await GetListDevextremesAsync();
        }

        private async Task<LoadResult> GetListDevextremesAsync()
        {
            var devExtremeInput = new DataLoadOptionDevextreme { Take = 20, Skip = 0 };
            return await base.GetListDevextremesAsync(devExtremeInput);
        }

        private async Task CheckNameUniqueness(string attrName, Guid? id = null)
        {
            if (_reservedNames.Contains(attrName))
            {
                throw new UserFriendlyException(message: L["Error:CustomerAttributesAppService:551"], code: "0");
            }
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName),
                CustomerAttributeConsts.AttrNameMaxLength, CustomerAttributeConsts.AttrNameMinLength);

            var existingAttribute =
                await _customerAttributeRepository.FirstOrDefaultAsync(x => x.AttrName == attrName);
            if (existingAttribute != null && existingAttribute.Id != id)
            {
                throw new UserFriendlyException(message: L["Error:CustomerAttributesAppService:550"], code: "0");
            }
        }

        private async Task<CustomerAttribute> GetLastActiveAttribute()
        {
            return (await _customerAttributeRepository.GetListAsync(x => x.Active == true))
              .OrderByDescending(x => x.AttrNo).FirstOrDefault();
        }
    }
}