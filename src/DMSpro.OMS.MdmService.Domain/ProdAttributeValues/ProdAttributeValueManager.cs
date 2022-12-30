using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValueManager : DomainService
    {
        private readonly IProdAttributeValueRepository _prodAttributeValueRepository;

        public ProdAttributeValueManager(IProdAttributeValueRepository prodAttributeValueRepository)
        {
            _prodAttributeValueRepository = prodAttributeValueRepository;
        }

        public async Task<ProdAttributeValue> CreateAsync(
        Guid prodAttributeId, Guid? parentProdAttributeValueId, string attrValName)
        {
            Check.NotNull(prodAttributeId, nameof(prodAttributeId));
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), ProdAttributeValueConsts.AttrValNameMaxLength, ProdAttributeValueConsts.AttrValNameMinLength);

            var prodAttributeValue = new ProdAttributeValue(
             GuidGenerator.Create(),
             prodAttributeId, parentProdAttributeValueId, attrValName
             );

            return await _prodAttributeValueRepository.InsertAsync(prodAttributeValue);
        }

        public async Task<ProdAttributeValue> UpdateAsync(
            Guid id,
            Guid prodAttributeId, Guid? parentProdAttributeValueId, string attrValName, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(prodAttributeId, nameof(prodAttributeId));
            Check.NotNullOrWhiteSpace(attrValName, nameof(attrValName));
            Check.Length(attrValName, nameof(attrValName), ProdAttributeValueConsts.AttrValNameMaxLength, ProdAttributeValueConsts.AttrValNameMinLength);

            var queryable = await _prodAttributeValueRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var prodAttributeValue = await AsyncExecuter.FirstOrDefaultAsync(query);

            prodAttributeValue.ProdAttributeId = prodAttributeId;
            prodAttributeValue.ParentProdAttributeValueId = parentProdAttributeValueId;
            prodAttributeValue.AttrValName = attrValName;

            prodAttributeValue.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _prodAttributeValueRepository.UpdateAsync(prodAttributeValue);
        }

    }
}