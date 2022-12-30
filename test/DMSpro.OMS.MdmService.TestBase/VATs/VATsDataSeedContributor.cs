using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.VATs;

namespace DMSpro.OMS.MdmService.VATs
{
    public class VATsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVATRepository _vATRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public VATsDataSeedContributor(IVATRepository vATRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _vATRepository = vATRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _vATRepository.InsertAsync(new VAT
            (
                id: Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                code: "47a28fbd2e7f499b917d",
                name: "a09078b4dcb446afa68accd69d3297aaf7fad3001b1f4ba2a001c375e4db1437ece485dc8bb5453c86ea4afa53baa4b91843",
                rate: 57762
            ));

            await _vATRepository.InsertAsync(new VAT
            (
                id: Guid.Parse("a0228ab7-7881-4112-a1e3-844a94951144"),
                code: "69272ab7432d4e27950e",
                name: "956b3c0b94f74cc09141737b09fe5bc023e0da39b3804fb6b338748183b15b8fe719b9c798d8488c89a4ef8c848f2dca3108",
                rate: 35452
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}