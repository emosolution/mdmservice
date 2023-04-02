using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{

    [Authorize(MdmServicePermissions.SalesOrgHeaders.Default)]
    public partial class SalesOrgHeadersAppService
    {

        public virtual async Task<SalesOrgHeaderDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(await _salesOrgHeaderRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _salesOrgHeaderRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Create)]
        public virtual async Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input)
        {

            var salesOrgHeader = await _salesOrgHeaderManager.CreateAsync(
            input.Code, input.Name, input.Active, Status.Open);

            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(salesOrgHeader);
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Edit)]
        public virtual async Task<SalesOrgHeaderDto> UpdateAsync(Guid id, SalesOrgHeaderUpdateDto input)
        {

            var salesOrgHeader = await _salesOrgHeaderManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Active, input.Status, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(salesOrgHeader);
        }
    }
}