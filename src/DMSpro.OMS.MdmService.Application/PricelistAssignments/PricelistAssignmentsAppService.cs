using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{

    [Authorize(MdmServicePermissions.PriceListAssignments.Default)]
    public partial class PricelistAssignmentsAppService
    {
        public virtual async Task<PricelistAssignmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PricelistAssignment, PricelistAssignmentDto>(await _pricelistAssignmentRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.PriceListAssignments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _pricelistAssignmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceListAssignments.Create)]
        public virtual async Task<PricelistAssignmentDto> CreateAsync(PricelistAssignmentCreateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }

            Check.NotNull(input.PriceListId, nameof(input.PriceListId));
            Check.NotNull(input.CustomerGroupId, nameof(input.CustomerGroupId));
            Check.Length(input.Description, nameof(input.Description), PricelistAssignmentConsts.DescriptionMaxLength);

            var pricelistAssignment = new PricelistAssignment(
                GuidGenerator.Create(),
                input.PriceListId, input.CustomerGroupId, input.Description, null
             );

            var newRecord = await _pricelistAssignmentRepository.InsertAsync(pricelistAssignment);

            return ObjectMapper.Map<PricelistAssignment, PricelistAssignmentDto>(newRecord);
        }

        [Authorize(MdmServicePermissions.PriceListAssignments.Edit)]
        public virtual async Task<PricelistAssignmentDto> UpdateAsync(Guid id, PricelistAssignmentUpdateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }

            Check.NotNull(input.PriceListId, nameof(input.PriceListId));
            Check.NotNull(input.CustomerGroupId, nameof(input.CustomerGroupId));
            Check.Length(input.Description, nameof(input.Description), PricelistAssignmentConsts.DescriptionMaxLength);

            var pricelistAssignment = await _pricelistAssignmentRepository.GetAsync(id);

            pricelistAssignment.PriceListId = input.PriceListId;
            pricelistAssignment.CustomerGroupId = input.CustomerGroupId;
            pricelistAssignment.Description = input.Description;

            pricelistAssignment.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            var updatedRecord = await _pricelistAssignmentRepository.UpdateAsync(pricelistAssignment);

            return ObjectMapper.Map<PricelistAssignment, PricelistAssignmentDto>(updatedRecord);
        }
    }
}