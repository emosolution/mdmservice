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
using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.Items;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{

    [Authorize(MdmServicePermissions.CustomerAttributes.Default)]
    public partial class CustomerAttributesAppService 
    {
        public virtual async Task<PagedResultDto<CustomerAttributeDto>> GetListAsync(GetCustomerAttributesInput input)
        {
            var totalCount = await _customerAttributeRepository.GetCountAsync(input.FilterText, input.AttrNoMin, input.AttrNoMax, input.AttrName, input.HierarchyLevelMin, input.HierarchyLevelMax, input.Active);
            var items = await _customerAttributeRepository.GetListAsync(input.FilterText, input.AttrNoMin, input.AttrNoMax, input.AttrName, input.HierarchyLevelMin, input.HierarchyLevelMax, input.Active, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerAttributeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAttribute>, List<CustomerAttributeDto>>(items)
            };
        }

        public virtual async Task<CustomerAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAttribute, CustomerAttributeDto>(await _customerAttributeRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerAttributeRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Create)]
        public virtual async Task<CustomerAttributeDto> CreateAsync(CustomerAttributeCreateDto input)
        {

            var customerAttribute = await _customerAttributeManager.CreateAsync(
            input.AttrNo, input.AttrName, input.Active, input.HierarchyLevel
            );

            return ObjectMapper.Map<CustomerAttribute, CustomerAttributeDto>(customerAttribute);
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Edit)]
        public virtual async Task<CustomerAttributeDto> UpdateAsync(Guid id, CustomerAttributeUpdateDto input)
        {
            var attribute = await _customerAttributeRepository.GetAsync(id);

            if (!input.Active)
            {
                if (await _customerRepository.AnyAsync(x =>
                    (attribute.AttrNo == 0 && x.Attribute0Id.HasValue) ||
                    (attribute.AttrNo == 1 && x.Attribute1Id.HasValue) ||
                    (attribute.AttrNo == 2 && x.Attribute2Id.HasValue) ||
                    (attribute.AttrNo == 3 && x.Attribute3Id.HasValue) ||
                    (attribute.AttrNo == 4 && x.Attribute4Id.HasValue) ||
                    (attribute.AttrNo == 5 && x.Attribute5Id.HasValue) ||
                    (attribute.AttrNo == 6 && x.Attribute6Id.HasValue) ||
                    (attribute.AttrNo == 7 && x.Attribute7Id.HasValue) ||
                    (attribute.AttrNo == 8 && x.Attribute8Id.HasValue) ||
                    (attribute.AttrNo == 9 && x.Attribute9Id.HasValue) ||
                    (attribute.AttrNo == 10 && x.Attribute10Id.HasValue) ||
                    (attribute.AttrNo == 11 && x.Attribute11Id.HasValue) ||
                    (attribute.AttrNo == 12 && x.Attribute12Id.HasValue) ||
                    (attribute.AttrNo == 13 && x.Attribute13Id.HasValue) ||
                    (attribute.AttrNo == 14 && x.Attribute14Id.HasValue) ||
                    (attribute.AttrNo == 15 && x.Attribute15Id.HasValue) ||
                    (attribute.AttrNo == 16 && x.Attribute16Id.HasValue) ||
                    (attribute.AttrNo == 17 && x.Attribute17Id.HasValue) ||
                    (attribute.AttrNo == 18 && x.Attribute18Id.HasValue) ||
                    (attribute.AttrNo == 19 && x.Attribute19Id.HasValue)
                    ))
                {
                    throw new UserFriendlyException(L["Error:General:UpdateContraint:550"]);
                }
            }
            else
            {
                if (attribute.AttrNo > 0 && _customerAttributeRepository.FirstOrDefaultAsync(x => x.AttrNo == attribute.AttrNo - 1).Result?.Active != true)
                {
                    throw new UserFriendlyException(L["Error:General:UpdateContraint:550"]);
                }
            }

            var customerAttribute = await _customerAttributeManager.UpdateAsync(
            id,
            input.AttrNo, input.AttrName, input.Active, input.HierarchyLevel, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerAttribute, CustomerAttributeDto>(customerAttribute);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttributeExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerAttributeRepository.GetListAsync(input.FilterText, input.AttrNoMin, input.AttrNoMax, input.AttrName, input.HierarchyLevelMin, input.HierarchyLevelMax, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerAttribute>, List<CustomerAttributeExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerAttributes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerAttributeExcelDownloadTokenCacheItem { Token = token },
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