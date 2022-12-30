using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.UOMs;

namespace DMSpro.OMS.MdmService.UOMs
{
    public class UOMsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IUOMRepository _uOMRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UOMsDataSeedContributor(IUOMRepository uOMRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _uOMRepository = uOMRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _uOMRepository.InsertAsync(new UOM
            (
                id: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                code: "ae697faeeda74e08a939",
                name: "aeaedb79c78940f4a6d98be0ee7aceefd1bfc94df57e48e4b9b960ea86c7d4af6f58e5a4870f4118966463ca1bc0e7e765f294ab7baf4e74b1984c4f5f9b938807faf21c3abd411faa067debf32ef73fc81dabd97c6248e4ae3ba46b8e13a4b5d7ea2312"
            ));

            await _uOMRepository.InsertAsync(new UOM
            (
                id: Guid.Parse("b6b100a3-92b1-47c9-9005-be037cb23a51"),
                code: "f5990c9776d4470eb38c",
                name: "c7862d933af640a393967ce035506860acb2ae023b46413aa07565a862fbd9c176d3cd76d30142aab4155e5e8d9bf8640e83be24f4fb44db9c897d8bf8961fc6671ef89f270d4f2c9ef83ed0eeed7c479bc3cac0ceda47b3bd8564fe069ac10573b88f89"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}