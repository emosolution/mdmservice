using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.SalesChannels;

namespace DMSpro.OMS.MdmService.SalesChannels
{
    public class SalesChannelsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISalesChannelRepository _salesChannelRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SalesChannelsDataSeedContributor(ISalesChannelRepository salesChannelRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _salesChannelRepository = salesChannelRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesChannelRepository.InsertAsync(new SalesChannel
            (
                id: Guid.Parse("c902f45b-606e-4505-8504-bd23485c7bc0"),
                code: "a96a7b625838424eb98f",
                name: "0d0c49c23fdc4e10b86374d3196c7ad17dc8f357eaa24d7ca1d416a7b941ae623d0493d36e3b4cc2a33899545064db2cac5d1d59e28e4b9ca1f8b0957d8224eb2637f00e84d242c5a346d8c67af313a7906262bf51b8446493054ea5a417a355f562d5b1",
                description: "da10e61e889549aba01adb487b22b428be98097756f044399e88525788619f8729e3d9c30aa84d41ad0806c20c523b8fcc197ce8a9664eda94b3bb1f71a7066b83864ce65def4b86977b267184b1b333715b6241d3104fe1a61fc96810879902331ae6871c4540d89f80a9f5784b7e0e9825837cad734f8faff03f8c937c31a88f4f88de7e2f4964aa90acecadeecb3fd365bd66449749efb3595282f58bdd8fd94db71c00994dee977ae5400b14dc934f6420ff023d45598e4b03647fec4e5196d524050f2a4db8a6dbb30949198fc6e187ce13acd04b1cba3aef726888dfad77c21de2b0c043feaf09517ed70344fa298cd181360c431a9e34",
                active: true
            ));

            await _salesChannelRepository.InsertAsync(new SalesChannel
            (
                id: Guid.Parse("0e013b93-f1f3-4257-937e-84f3c03a4748"),
                code: "e72750e18f384568a5d3",
                name: "3a8c1c4fc840497396bdee4ee13ed52aadb751971cf44dbb87d94fa0fbd5411058681b61eb964c97ba44d4d3ab3cce5699313491dfd3469f990024dfd74861875770bae0b3244d2fb91cb6a3533691e98ea71c6d7a364fd4a30570d6e99619776de461d3",
                description: "321d63e7936f432a9ad995a2cc19c03ec4ff6f7972c542e5a4ac9e5e12339a8bbe1724e9c6ce41749f35cbb18341a34684584560488e4eb58e10f491e111f2c0e19d137b5cfe4246a13bbbd502568d9aa6372e36a4264caa97f26a3fdb390ec4274d89c23f194d41bf39c3cf52de7397ac1929edff9245e8bc2ba5932dcfd905103c9081ad9e490ab842e3e3e676fcac2fd148d8b0e149668faa3149150fbb140e95caba76f84408aab0154fe4c8e6874a3adaa63158403ba0258183b9382e74b79afc85ce5948c5993e16f74f9ce3118f8c379d9e174890ba8c78064293d2dd6f5b4988dca24a2c98af846cbf5f6efca00b4f6f35da4fec8427",
                active: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}