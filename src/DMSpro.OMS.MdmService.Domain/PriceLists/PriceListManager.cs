using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListManager : DomainService
    {
        private readonly IPriceListRepository _priceListRepository;

        public PriceListManager(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<PriceList> CreateAsync(
            Guid? basePriceListId, string code, string name, bool active, 
            bool isBase, bool isDefault, 
            ArithmeticOperator? arithmeticOperation = null, 
            int? arithmeticFactor = null, 
            ArithmeticFactorType? arithmeticFactorType = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), PriceListConsts.CodeMaxLength, PriceListConsts.CodeMinLength);
            Check.Length(name, nameof(name), PriceListConsts.NameMaxLength);

            var priceList = new PriceList(
             GuidGenerator.Create(),
             basePriceListId, code, name, active, 
             isBase, isDefault, false, arithmeticOperation, arithmeticFactor, arithmeticFactorType, null);

            return await _priceListRepository.InsertAsync(priceList);
        }

        public async Task<PriceList> UpdateAsync(
            Guid id,
            Guid? basePriceListId, string code, string name, bool active, bool isDefault, 
            ArithmeticOperator? arithmeticOperation = null, int? arithmeticFactor = null, 
            ArithmeticFactorType? arithmeticFactorType = null,  
            [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), PriceListConsts.CodeMaxLength, PriceListConsts.CodeMinLength);
            Check.Length(name, nameof(name), PriceListConsts.NameMaxLength);

            var priceList = await _priceListRepository.GetAsync(id);

            priceList.BasePriceListId = basePriceListId;
            priceList.Code = code;
            priceList.Name = name;
            priceList.Active = active;
            priceList.IsDefault = isDefault;
            priceList.ArithmeticOperation = arithmeticOperation;
            priceList.ArithmeticFactor = arithmeticFactor;
            priceList.ArithmeticFactorType = arithmeticFactorType;

            priceList.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _priceListRepository.UpdateAsync(priceList);
        }

    }
}