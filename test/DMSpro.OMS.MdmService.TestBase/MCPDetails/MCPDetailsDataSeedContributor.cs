using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.Customers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.MCPDetails;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public class MCPDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IMCPDetailRepository _mCPDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        private readonly MCPHeadersDataSeedContributor _mCPHeadersDataSeedContributor;

        public MCPDetailsDataSeedContributor(IMCPDetailRepository mCPDetailRepository, IUnitOfWorkManager unitOfWorkManager, 
            CustomersDataSeedContributor customersDataSeedContributor, 
            MCPHeadersDataSeedContributor mCPHeadersDataSeedContributor)
        {
            _mCPDetailRepository = mCPDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customersDataSeedContributor = customersDataSeedContributor; 
            _mCPHeadersDataSeedContributor = mCPHeadersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customersDataSeedContributor.SeedAsync(context);
            await _mCPHeadersDataSeedContributor.SeedAsync(context);

            await _mCPDetailRepository.InsertAsync(new MCPDetail
            (
                id: Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                code: "02692e47c4144a22bc1e",
                effectiveDate: new DateTime(2019, 4, 11),
                endDate: new DateTime(2008, 6, 27),
                distance: 1594565668,
                visitOrder: 1769718088,
                monday: true,
                tuesday: true,
                wednesday: true,
                thursday: true,
                friday: true,
                saturday: true,
                sunday: true,
                week1: true,
                week2: true,
                week3: true,
                week4: true,
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781"),
                mCPHeaderId: Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730")
            ));

            await _mCPDetailRepository.InsertAsync(new MCPDetail
            (
                id: Guid.Parse("4bf4fc22-2402-4854-a495-0df8c26ae80c"),
                code: "1d126a13ebd34048b761",
                effectiveDate: new DateTime(2000, 6, 27),
                endDate: new DateTime(2014, 6, 7),
                distance: 1807959055,
                visitOrder: 1143785188,
                monday: true,
                tuesday: true,
                wednesday: true,
                thursday: true,
                friday: true,
                saturday: true,
                sunday: true,
                week1: true,
                week2: true,
                week3: true,
                week4: true,
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781"),
                mCPHeaderId: Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}