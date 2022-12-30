using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroups;
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
using DMSpro.OMS.MdmService.CustomerGroupByAtts;
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
namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{

    [Authorize(MdmServicePermissions.CustomerGroupByAtts.Default)]
    public class CustomerGroupByAttsAppService : ApplicationService, ICustomerGroupByAttsAppService
    {
        private readonly IDistributedCache<CustomerGroupByAttExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerGroupByAttRepository _customerGroupByAttRepository;
        private readonly CustomerGroupByAttManager _customerGroupByAttManager;
        private readonly IRepository<CustomerGroup, Guid> _customerGroupRepository;
        private readonly IRepository<CusAttributeValue, Guid> _cusAttributeValueRepository;

        public CustomerGroupByAttsAppService(ICustomerGroupByAttRepository customerGroupByAttRepository, CustomerGroupByAttManager customerGroupByAttManager, IDistributedCache<CustomerGroupByAttExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<CustomerGroup, Guid> customerGroupRepository, IRepository<CusAttributeValue, Guid> cusAttributeValueRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerGroupByAttRepository = customerGroupByAttRepository;
            _customerGroupByAttManager = customerGroupByAttManager; _customerGroupRepository = customerGroupRepository;
            _cusAttributeValueRepository = cusAttributeValueRepository;
        }

        public virtual async Task<PagedResultDto<CustomerGroupByAttWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByAttsInput input)
        {
            var totalCount = await _customerGroupByAttRepository.GetCountAsync(input.FilterText, input.ValueCode, input.ValueName, input.CustomerGroupId, input.CusAttributeValueId);
            var items = await _customerGroupByAttRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ValueCode, input.ValueName, input.CustomerGroupId, input.CusAttributeValueId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerGroupByAttWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerGroupByAttWithNavigationProperties>, List<CustomerGroupByAttWithNavigationPropertiesDto>>(items)
            };
        }


        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _customerGroupByAttRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<CustomerGroupByAtt>, IEnumerable<CustomerGroupByAttDto>>(results.data.Cast<CustomerGroupByAtt>());
            
            return results;
                
        }

        public virtual async Task<CustomerGroupByAttWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupByAttWithNavigationProperties, CustomerGroupByAttWithNavigationPropertiesDto>
                (await _customerGroupByAttRepository.GetWithNavigationPropertiesAsync(id));

        }

        public virtual async Task<CustomerGroupByAttDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupByAtt, CustomerGroupByAttDto>(await _customerGroupByAttRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CustomerGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerGroup>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCusAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _cusAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CusAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CusAttributeValue>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.CustomerGroupByAtts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerGroupByAttRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroupByAtts.Create)]
        public virtual async Task<CustomerGroupByAttDto> CreateAsync(CustomerGroupByAttCreateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.CusAttributeValueId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CusAttributeValue"]]);
            }

            var customerGroupByAtt = await _customerGroupByAttManager.CreateAsync(
            input.CustomerGroupId, input.CusAttributeValueId, input.ValueCode, input.ValueName
            );

            return ObjectMapper.Map<CustomerGroupByAtt, CustomerGroupByAttDto>(customerGroupByAtt);
        }

        [Authorize(MdmServicePermissions.CustomerGroupByAtts.Edit)]
        public virtual async Task<CustomerGroupByAttDto> UpdateAsync(Guid id, CustomerGroupByAttUpdateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.CusAttributeValueId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CusAttributeValue"]]);
            }

            var customerGroupByAtt = await _customerGroupByAttManager.UpdateAsync(
            id,
            input.CustomerGroupId, input.CusAttributeValueId, input.ValueCode, input.ValueName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerGroupByAtt, CustomerGroupByAttDto>(customerGroupByAtt);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByAttExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerGroupByAttRepository.GetListAsync(input.FilterText, input.ValueCode, input.ValueName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerGroupByAtt>, List<CustomerGroupByAttExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerGroupByAtts.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerGroupByAttExcelDownloadTokenCacheItem { Token = token },
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