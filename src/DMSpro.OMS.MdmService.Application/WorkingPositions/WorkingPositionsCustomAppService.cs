using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.WorkingPositions
{

    [Authorize(MdmServicePermissions.WorkingPositions.Default)]
    public partial class WorkingPositionsAppService 
    {
        public virtual async Task<WorkingPositionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<WorkingPosition, WorkingPositionDto>(await _workingPositionRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.WorkingPositions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            if (await _employeeProfileRepository.AnyAsync(x => x.WorkingPositionId == id)) {
                throw new UserFriendlyException(message: L["Error:General:DeleteContraint:550"], code: "0");
            }
            await _workingPositionRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.WorkingPositions.Create)]
        public virtual async Task<WorkingPositionDto> CreateAsync(WorkingPositionCreateDto input)
        {
            Check.NotNullOrWhiteSpace(input.Code, nameof(input.Code));
            Check.Length(input.Code, nameof(input.Code), WorkingPositionConsts.CodeMaxLength, WorkingPositionConsts.CodeMinLength);
            Check.Length(input.Name, nameof(input.Name), WorkingPositionConsts.NameMaxLength);
            Check.Length(input.Description, nameof(input.Description), WorkingPositionConsts.DescriptionMaxLength);

            await CheckCodeUniqueness(input.Code);

            WorkingPosition workingPosition = new(GuidGenerator.Create(), input.Code, input.Name,
                input.Description, input.Active);
            await _workingPositionRepository.InsertAsync(workingPosition);

            return ObjectMapper.Map<WorkingPosition, WorkingPositionDto>(workingPosition);
        }

        [Authorize(MdmServicePermissions.WorkingPositions.Edit)]
        public virtual async Task<WorkingPositionDto> UpdateAsync(Guid id, WorkingPositionUpdateDto input)
        {
            Check.Length(input.Name, nameof(input.Name), WorkingPositionConsts.NameMaxLength);
            Check.Length(input.Description, nameof(input.Description), WorkingPositionConsts.DescriptionMaxLength);

            var workingPosition = await _workingPositionRepository.GetAsync(id);

            workingPosition.Name = input.Name;
            workingPosition.Description = input.Description;
            workingPosition.Active = input.Active;

            workingPosition.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _workingPositionRepository.UpdateAsync(workingPosition);
            return ObjectMapper.Map<WorkingPosition, WorkingPositionDto>(workingPosition);
        }
    }
}