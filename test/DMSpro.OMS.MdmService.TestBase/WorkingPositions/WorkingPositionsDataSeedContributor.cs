using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.WorkingPositions;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IWorkingPositionRepository _workingPositionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public WorkingPositionsDataSeedContributor(IWorkingPositionRepository workingPositionRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _workingPositionRepository = workingPositionRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _workingPositionRepository.InsertAsync(new WorkingPosition
            (
                id: Guid.Parse("f7942152-dbeb-42ad-9935-c3064738dc4e"),
                code: "5bc59e9653624df6aa70",
                name: "ccef596cb06c457cace6e5b9b914947212259f2b8d644608a4d22464371c38f184c6a0fcfae541f1aec2f099be92",
                description: "1d6c99b36e7",
                active: true
            ));

            await _workingPositionRepository.InsertAsync(new WorkingPosition
            (
                id: Guid.Parse("25e2ff5f-c900-44f8-9b22-b2d338c1b632"),
                code: "79de8f9dd2534096b86c",
                name: "1917671cf2684827b2b44c7b21929ae80ffa12",
                description: "4d1bd92937d84310820d58780",
                active: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}