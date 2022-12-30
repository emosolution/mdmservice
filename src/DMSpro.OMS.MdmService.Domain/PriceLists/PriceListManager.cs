using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
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
        Guid? basePriceListId, string code, string name, bool active, bool isFirstPriceList, ArithmeticOperator? arithmeticOperation = null, int? arithmeticFactor = null, ArithmeticFactorType? arithmeticFactorType = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), PriceListConsts.CodeMaxLength, PriceListConsts.CodeMinLength);

            var priceList = new PriceList(
             GuidGenerator.Create(),
             basePriceListId, code, name, active, isFirstPriceList, arithmeticOperation, arithmeticFactor, arithmeticFactorType
             );

            return await _priceListRepository.InsertAsync(priceList);
        }

        public async Task<PriceList> UpdateAsync(
            Guid id,
            Guid? basePriceListId, string code, string name, bool active, bool isFirstPriceList, ArithmeticOperator? arithmeticOperation = null, int? arithmeticFactor = null, ArithmeticFactorType? arithmeticFactorType = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), PriceListConsts.CodeMaxLength, PriceListConsts.CodeMinLength);

            var queryable = await _priceListRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var priceList = await AsyncExecuter.FirstOrDefaultAsync(query);

            priceList.BasePriceListId = basePriceListId;
            priceList.Code = code;
            priceList.Name = name;
            priceList.Active = active;
            priceList.IsFirstPriceList = isFirstPriceList;
            priceList.ArithmeticOperation = arithmeticOperation;
            priceList.ArithmeticFactor = arithmeticFactor;
            priceList.ArithmeticFactorType = arithmeticFactorType;

            priceList.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _priceListRepository.UpdateAsync(priceList);
        }

    }
}