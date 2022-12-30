using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class CusAttributeValueManager : DomainService
    {
        private readonly ICusAttributeValueRepository _cusAttributeValueRepository;

        public CusAttributeValueManager(ICusAttributeValueRepository cusAttributeValueRepository)
        {
            _cusAttributeValueRepository = cusAttributeValueRepository;
        }

        public async Task<CusAttributeValue> CreateAsync(
        Guid customerAttributeId, Guid? parentCusAttributeValueId, string attrValName)
        {
            Check.NotNull(customerAttributeId, nameof(customerAttributeId));
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), CusAttributeValueConsts.AttrValNameMaxLength, CusAttributeValueConsts.AttrValNameMinLength);

            var cusAttributeValue = new CusAttributeValue(
             GuidGenerator.Create(),
             customerAttributeId, parentCusAttributeValueId, attrValName
             );

            return await _cusAttributeValueRepository.InsertAsync(cusAttributeValue);
        }

        public async Task<CusAttributeValue> UpdateAsync(
            Guid id,
            Guid customerAttributeId, Guid? parentCusAttributeValueId, string attrValName, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerAttributeId, nameof(customerAttributeId));
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), CusAttributeValueConsts.AttrValNameMaxLength, CusAttributeValueConsts.AttrValNameMinLength);

            var queryable = await _cusAttributeValueRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var cusAttributeValue = await AsyncExecuter.FirstOrDefaultAsync(query);

            cusAttributeValue.CustomerAttributeId = customerAttributeId;
            cusAttributeValue.ParentCusAttributeValueId = parentCusAttributeValueId;
            cusAttributeValue.AttrValName = attrValName;

            cusAttributeValue.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _cusAttributeValueRepository.UpdateAsync(cusAttributeValue);
        }

    }
}