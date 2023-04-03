using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{

    [Authorize(MdmServicePermissions.SalesOrgHeaders.Default)]
    public partial class SalesOrgHeadersAppService
    {

        public virtual async Task<SalesOrgHeaderDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(await _salesOrgHeaderRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Create)]
        public virtual async Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input)
        {
            Check.NotNullOrWhiteSpace(input.Code, nameof(input.Code));
            Check.NotNullOrWhiteSpace(input.Name, nameof(input.Name));
            Check.Length(input.Code, nameof(input.Code), SalesOrgHeaderConsts.CodeMaxLength, SalesOrgHeaderConsts.CodeMinLength);
            Check.Length(input.Name, nameof(input.Name), SalesOrgHeaderConsts.NameMaxLength);
            
            await CheckCodeUniqueness(input.Code);

            var salesOrgHeader = new SalesOrgHeader(
                GuidGenerator.Create(),
                input.Code, input.Name, true, Status.OPEN);

            var record = await _salesOrgHeaderRepository.InsertAsync(salesOrgHeader);

            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(record);
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Edit)]
        public virtual async Task<SalesOrgHeaderDto> ReleaseAsync(Guid id)
        {
            var header = await _salesOrgHeaderRepository.GetAsync(x => x.Id == id);
            if (header.Status != Status.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHeadersAppService:550"], code: "0");
            }
            header.Status = Status.RELEASED;
            var record = await _salesOrgHeaderRepository.UpdateAsync(header);

            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(record);
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Edit)]
        public virtual async Task<SalesOrgHeaderDto> InactiveAsync(Guid id)
        {
            var header = await _salesOrgHeaderRepository.GetAsync(x => x.Id == id);
            if (header.Status != Status.RELEASED)
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHeadersAppService:551"], code: "0");
            }
            header.Status = Status.INACTIVE;
            var record = await _salesOrgHeaderRepository.UpdateAsync(header);

            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(record);
        }
    }
}