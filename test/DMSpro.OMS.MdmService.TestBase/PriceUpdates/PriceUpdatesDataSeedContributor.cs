using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.PriceUpdates;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdatesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPriceUpdateRepository _priceUpdateRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PriceListsDataSeedContributor _priceListsDataSeedContributor;

        public PriceUpdatesDataSeedContributor(IPriceUpdateRepository priceUpdateRepository, IUnitOfWorkManager unitOfWorkManager, PriceListsDataSeedContributor priceListsDataSeedContributor)
        {
            _priceUpdateRepository = priceUpdateRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _priceListsDataSeedContributor = priceListsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _priceListsDataSeedContributor.SeedAsync(context);

            await _priceUpdateRepository.InsertAsync(new PriceUpdate
            (
                id: Guid.Parse("552e1fe9-81ad-4bee-9ade-1f1ae70f867f"),
                code: "7aaf9d5dd32d4689beb8",
                description: "c182524609f7473ebb9df1de920bd07a180ed576692c4c9391db65d63ea9321aa6fb9ec3616644b4a753543502d7b292f6f01487d9544505849323451b0758be371f5e35c1ac4261a2db16e2abe187fc76bc0a1f2e984d53ba844f43f01b29c232a2192a5482434db4ee45eae529c1c3b7ef8b0934df454e815d22097f318fbee9d5be556c2d4319a458a8341473c5642cf50882ba634435a52eb6d98489fd716e19711a171e4843ade308226e18d894c99748cfaf5449f094841e0b54256068472ac38c339441aba9453f6f5e0f295e3cac881c482c462eac93c3a7e5d88f942d87446d602c48049ca1a812e2cb4afe4ed01fcda9a44bed8615",
                effectiveDate: new DateTime(2022, 2, 27),
                endDate: new DateTime(2009, 7, 14),
                status: default,
                isScheduled: true,
                releasedDate: new DateTime(2012, 9, 12),
                cancelledDate: new DateTime(2002, 8, 25),
                completeDate: new DateTime(2011, 11, 20),
                priceListId: Guid.Parse("4758593c-d51b-4934-a996-ee0572f5c083")
            ));

            await _priceUpdateRepository.InsertAsync(new PriceUpdate
            (
                id: Guid.Parse("b788e8cf-068c-4675-abb8-acc5c24ca1f8"),
                code: "24e50de6b901402c9762",
                description: "1b608a82cbe04bd1a5a481f3e198ae3380a793e773904066a5ef210059cf00dd30884d6ffbc848c0994b012f2d1d639955eff0b8600a4479ae88068821cf05d99c52b3557950419e86ffe01aa633163e29fc0b7a4afc4910b27e2592b13514b1861c1eaa74cd435f8ed012770449ffb5e3eb0aac4d314180abdaedd6b1fd93923da9a95a106747709e7f6dd88e18b978f8a10e33c55f4b5fa5f351859f366927ef5aae6e4096479b8e3a6ceffcfaadb3758c9852f2f0454c9af53c81a9e113765bf89743176b4d44a9bdc006fedfae91df567f43edb74b69b5607f1040a0812a192feb3907b24469952544b13a24f3482ed08e664c364e68bcd5",
                effectiveDate: new DateTime(2022, 6, 6),
                endDate: new DateTime(2001, 8, 7),
                status: default,
                isScheduled: true,
                releasedDate: new DateTime(2020, 10, 5),
                cancelledDate: new DateTime(2003, 7, 9),
                completeDate: new DateTime(2016, 9, 25),
                priceListId: Guid.Parse("4758593c-d51b-4934-a996-ee0572f5c083")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}