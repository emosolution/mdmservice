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
                id: Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"),
                code: "b8e4af2d81e64cf9b46f",
                description: "3c12564c303c4379ab3ad530c0ad48b733be62311d974d71a1dc71f1d7274cb",
                effectiveDate: new DateTime(2021, 4, 12),
                status: default,
                updateStatusDate: new DateTime(2014, 10, 25),
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5")
            ));

            await _priceUpdateRepository.InsertAsync(new PriceUpdate
            (
                id: Guid.Parse("f92a255c-7a02-427f-bf29-27bd85f1d3fc"),
                code: "a944fc77cca74ec9bc4f",
                description: "e15c75ae0a074b9e9f81ff83129105e8a02",
                effectiveDate: new DateTime(2005, 2, 7),
                status: default,
                updateStatusDate: new DateTime(2002, 8, 15),
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}