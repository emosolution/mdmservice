using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerGroups
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupsAppService
    {
        public virtual async Task<PagedResultDto<CustomerGroupDto>> GetListAsync(GetCustomerGroupsInput input)
        {
            var totalCount = await _customerGroupRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.GroupBy, input.Status);
            var items = await _customerGroupRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.GroupBy, input.Status, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerGroupDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerGroup>, List<CustomerGroupDto>>(items)
            };
        }

        public virtual async Task<CustomerGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroup, CustomerGroupDto>(await _customerGroupRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerGroupRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Create)]
        public virtual async Task<CustomerGroupDto> CreateAsync(CustomerGroupCreateDto input)
        {

            var customerGroup = await _customerGroupManager.CreateAsync(
            input.Code, input.Name, input.Active, input.GroupBy, input.Status, input.EffectiveDate
            );

            return ObjectMapper.Map<CustomerGroup, CustomerGroupDto>(customerGroup);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Edit)]
        public virtual async Task<CustomerGroupDto> UpdateAsync(Guid id, CustomerGroupUpdateDto input)
        {

            var customerGroup = await _customerGroupManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Active, input.GroupBy, input.Status, input.EffectiveDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerGroup, CustomerGroupDto>(customerGroup);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerGroupRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.GroupBy, input.Status);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerGroup>, List<CustomerGroupExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerGroups.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerGroupExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}