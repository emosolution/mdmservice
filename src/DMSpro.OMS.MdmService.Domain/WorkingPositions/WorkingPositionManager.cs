using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionManager : DomainService
    {
        private readonly IWorkingPositionRepository _workingPositionRepository;

        public WorkingPositionManager(IWorkingPositionRepository workingPositionRepository)
        {
            _workingPositionRepository = workingPositionRepository;
        }

        public async Task<WorkingPosition> CreateAsync(
        string code, string name, string description, bool active)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), WorkingPositionConsts.CodeMaxLength, WorkingPositionConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var workingPosition = new WorkingPosition(
             GuidGenerator.Create(),
             code, name, description, active
             );

            return await _workingPositionRepository.InsertAsync(workingPosition);
        }

        public async Task<WorkingPosition> UpdateAsync(
            Guid id,
            string code, string name, string description, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), WorkingPositionConsts.CodeMaxLength, WorkingPositionConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var queryable = await _workingPositionRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var workingPosition = await AsyncExecuter.FirstOrDefaultAsync(query);

            workingPosition.Code = code;
            workingPosition.Name = name;
            workingPosition.Description = description;
            workingPosition.Active = active;

            workingPosition.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _workingPositionRepository.UpdateAsync(workingPosition);
        }

    }
}