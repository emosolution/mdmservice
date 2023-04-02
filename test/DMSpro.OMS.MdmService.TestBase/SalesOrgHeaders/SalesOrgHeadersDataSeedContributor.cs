using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.SalesOrgHeaders;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeadersDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SalesOrgHeadersDataSeedContributor(ISalesOrgHeaderRepository salesOrgHeaderRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _salesOrgHeaderRepository = salesOrgHeaderRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHeaderRepository.InsertAsync(new SalesOrgHeader
            (
                id: Guid.Parse("2d65cdf9-4242-44e8-8583-a57e4f3ee7f7"),
                code: "925254bf7cdd4f4289f6",
                name: "cb8f4279b130459a9d040b9741fdcc854b5c3ff7a24d4c0fbbde46a6c2538755e5ae34766daf4a54b2651b5b8909865eb151395347a844139ae7c9379289d0ebd5e1720062fd4de4bd6adc7d76f56c8c531cc76155a846f897c0d40dc502a4fcc43db22fcb904c64a16831338995faa6541ee47ca80c477c913bab546669eb2",
                active: true,
                status: default
            ));

            await _salesOrgHeaderRepository.InsertAsync(new SalesOrgHeader
            (
                id: Guid.Parse("29fd816b-9258-4c5d-94c4-3727fe82e559"),
                code: "fc394a97d2154f76aac6",
                name: "0667fb4e08804af7bc899c22b513f54cac2b76b4e63e4c239e41234fc671bcec5a472498a6b442ff97e564906d26a9841cfd793d5841462ea6436fd1b5d4ca1f6ba8271bb6654ecf8f52facfcfebe9602cb00b93c7724814ab7c68cb42ecaa9004ab0644170f4c748a49de7da34e9aecfc0b0c33d6b14eb1be95a4adbaca3a8",
                active: true,
                status: default
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}