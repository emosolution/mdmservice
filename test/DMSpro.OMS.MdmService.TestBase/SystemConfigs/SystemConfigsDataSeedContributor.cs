using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.SystemConfigs;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISystemConfigRepository _systemConfigRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SystemConfigsDataSeedContributor(ISystemConfigRepository systemConfigRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _systemConfigRepository = systemConfigRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _systemConfigRepository.InsertAsync(new SystemConfig
            (
                id: Guid.Parse("ff279c6a-2798-4c3f-9686-75427f1fe625"),
                code: "9adca84dd6104c47a260",
                description: "1c857e2871eb44eaac98713b317b6ccbc2b97d4df3934132bb013fb6249f417041486b9a8bef49dd89f097c2690c0808a037fbac9ea54c6ca891855e95222115524b2323d925415494b3c58c44508c12430e41eb42e0480d97bde9ec7fe6051fb208402f3a7e4137a1c4a9823f24f8f530285019848142e498f47f76c5227c6cf8f146f488134729b80b91a93ca08bf3764c2ec67e9342eaabc3e04ee09d3a44f6c1e22a7dd34c789444a2dda881656bbfbeab932ed94fde8b89c325d474870bee94b6aa9c2d4ab3bf438cd5a3d9965a16b1b54b6da943fb8da42f5164bbd30b36d3b283fe014a43b902050c48fcc2a823f0c3bacea94420aa82",
                value: "475aa39ea90045899a8c371574828c1e90586db498314f2ab6e2d0112da7245cbab92411a8b144b990dea031b98454ebb7d98d1da2444764b97c638d4243d22b28dc864f13754d7185707064b8ca995c8e9b06e1a50244b08127064878089475df24427841bd46cdb2a49ba2b3ebd3f40f7feec77094454193ed5d16352e0db",
                defaultValue: "a294db1d4ef347f58856bd766543c2895911e7229e1d4e708356e8d8820dce331b3223e8749f469bb4fc8f087b6965ed74889cac2b694fb1b1749800788f5e34c79da604c95841bab4da18f209eb9cda3597a3ce8eea448094f9e9cc73b1bd0640c6b2c0afde459aa735449faf3b521b8fd36a15199845fbb86ef672f8fd8cc",
                editableByTenant: true,
                controlType: default,
                dataSource: "d44a92fc643941e5880aa66f089cf789e5b8f07d53564ff0bce6c3f9f1b4b7a8099ed2529"
            ));

            await _systemConfigRepository.InsertAsync(new SystemConfig
            (
                id: Guid.Parse("4df34775-c209-4a95-8cbc-4e5f3ac276f1"),
                code: "63a817ff1cfb453b9013",
                description: "89bbefe330884d3e961f0727b3725f1ebdeed5368c184f0ca749ddb1eff33854c318e4a769d04cc5bd0729a7dddba8eebfef0c2212f845c9b948b8662afa770a856f6ed6441a44218391ae5dd11029d8baeeae46b52f4275a4b497855e89572f839651dbe8cc493e91ee56eae5af7faf8f0f44dfef054485ab5aa45a58f4cdbeeeae869e3d0349249388f6f31414e6976ff39a513eea49acafd6da6d0cc00d415a8f6428194c44e99e495d4f670f697ea136f042b77f47a99bc5f80fe454b38ef73e7749de344ed2879956c030104807ab9e5d80ea664bce8c2ba891622a8c3a35a498f22d9e4534869544c4d9212830fe8f6278921f4a7fbbe6",
                value: "ab7268a49c7a44a8a7de0f3fe23d03e37a7f4682b6d545378c4fef1f3c748979480e423b020349dd9b4a7b559100150bcf74c99d4fea4a3182f9ac9fca245ff4f6be4a5b4b0d4f9f8c9d6031f25b566ae315dc3d7b04402dabcfc6fc8231fe4bfa5bbdaeab5f4df7af335243f6e5e90d6c01429c7b6947b480eea3b80fc8807",
                defaultValue: "068fa5703d6d49a8a9ef622401e13efaf8d092316cd94897b33875bdeb5c96534bfef5d967ce41da967514acdea59b9d0fce254b03b54a988a8b59c361f860f894c64e85469841d7b14cd03badead0f8df32585f802f4edfbdfd315175f2cc0273fc8ef5482c4813a9bf7e14ac57fedd0cebac897196491f881689c52fd48e0",
                editableByTenant: true,
                controlType: default,
                dataSource: "67731958ad3d48c"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}