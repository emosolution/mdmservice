using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceUpdates;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.PriceUpdateDetails;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPriceUpdateDetailRepository _priceUpdateDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PriceUpdatesDataSeedContributor _priceUpdatesDataSeedContributor;

        private readonly PriceListDetailsDataSeedContributor _priceListDetailsDataSeedContributor;

        public PriceUpdateDetailsDataSeedContributor(IPriceUpdateDetailRepository priceUpdateDetailRepository, IUnitOfWorkManager unitOfWorkManager, PriceUpdatesDataSeedContributor priceUpdatesDataSeedContributor, PriceListDetailsDataSeedContributor priceListDetailsDataSeedContributor)
        {
            _priceUpdateDetailRepository = priceUpdateDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _priceUpdatesDataSeedContributor = priceUpdatesDataSeedContributor; _priceListDetailsDataSeedContributor = priceListDetailsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _priceUpdatesDataSeedContributor.SeedAsync(context);
            await _priceListDetailsDataSeedContributor.SeedAsync(context);

            await _priceUpdateDetailRepository.InsertAsync(new PriceUpdateDetail
            (
                id: Guid.Parse("16c1cd30-16ac-4c30-89b6-87018568cb87"),
                priceBeforeUpdate: 2097380020,
                newPrice: 1318016596,
                updatedDate: new DateTime(2001, 11, 18),
                priceUpdateId: Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"),
                priceListDetailId: Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46")
            ));

            await _priceUpdateDetailRepository.InsertAsync(new PriceUpdateDetail
            (
                id: Guid.Parse("d0e186ba-69f1-4963-805f-e5eea4faef91"),
                priceBeforeUpdate: 374071104,
                newPrice: 675470205,
                updatedDate: new DateTime(2006, 6, 27),
                priceUpdateId: Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"),
                priceListDetailId: Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}