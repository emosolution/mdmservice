using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.UOMGroups;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    public class UOMGroupsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IUOMGroupRepository _uOMGroupRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UOMGroupsDataSeedContributor(IUOMGroupRepository uOMGroupRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _uOMGroupRepository = uOMGroupRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _uOMGroupRepository.InsertAsync(new UOMGroup
            (
                id: Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                code: "fdc8d6d0927642d28947",
                name: "17f43074276d4213b83ba431d17d1a87abb40aaeb990415a9bf6ccb1f4796a886d038ec0ecef41bda52d7955cf761e7eb6cf4614d3824e8da5263f2c3b312da35c3e49b576e448b7b7024532d17e4c73d1c263ad866b4c8b98009d1d1ba35721ea62c78c"
            ));

            await _uOMGroupRepository.InsertAsync(new UOMGroup
            (
                id: Guid.Parse("16091d7b-9646-487e-be19-36b00a1f6d0b"),
                code: "62997ebacc7046a490e3",
                name: "25124ebd5e4647ee992432d5a79214c0f43997d9ff104538965c4f2acaa3403a8e9011df94414ea2be63709fbea495a4f9b4f17c73914b2d9c8b302220a145012fec540019724b0ea89ba487785ef965ef667641fb4a47809a4946ad2d958226085ffea5"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}