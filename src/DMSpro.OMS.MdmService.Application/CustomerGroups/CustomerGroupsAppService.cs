using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.CustomerGroups;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.CustomerGroups
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public class CustomerGroupsAppService : ApplicationService, ICustomerGroupsAppService
    {
        private readonly IDistributedCache<CustomerGroupExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly CustomerGroupManager _customerGroupManager;

        public CustomerGroupsAppService(ICustomerGroupRepository customerGroupRepository, CustomerGroupManager customerGroupManager, IDistributedCache<CustomerGroupExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerGroupRepository = customerGroupRepository;
            _customerGroupManager = customerGroupManager;
        }

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

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _customerGroupRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<CustomerGroup>, IEnumerable<CustomerGroupDto>>(results.data.Cast<CustomerGroup>());
            
            return results;
                
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