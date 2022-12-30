using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.SystemDatas;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public class SystemDatasDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISystemDataRepository _systemDataRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SystemDatasDataSeedContributor(ISystemDataRepository systemDataRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _systemDataRepository = systemDataRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _systemDataRepository.InsertAsync(new SystemData
            (
                id: Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"),
                code: "cfabd310ae",
                valueCode: "c7d5a642e3",
                valueName: "593efc875800423e854ff83923878c221b3149124fed49599e41130748a900562a7ab61e468b45a68755888f05ab5a8ed97c"
            ));

            await _systemDataRepository.InsertAsync(new SystemData
            (
                id: Guid.Parse("18db414e-24df-47a9-b033-e96bac671dfb"),
                code: "64fb608af5",
                valueCode: "d1ccb117e5",
                valueName: "c8d8e4421ebe4daebacc2091b30fafecb46079221c1e4ef4b6b8bbe8980ff6e452f0b87455c345bba867999177d425f82704"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}