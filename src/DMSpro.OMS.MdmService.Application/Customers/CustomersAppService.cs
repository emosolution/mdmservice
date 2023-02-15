using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.Customers
{

    [Authorize(MdmServicePermissions.Customers.Default)]
    public partial class CustomersAppService 
    {
        public virtual async Task<PagedResultDto<CustomerWithNavigationPropertiesDto>> GetListAsync(GetCustomersInput input)
        {
            var totalCount = await _customerRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Phone1, input.Phone2, input.erpCode, input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.CreditLimitMin, input.CreditLimitMax, input.IsCompany, input.WarehouseId, input.Street, input.Address, input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDateMin, input.LastOrderDateMax, input.PaymentTermId, input.LinkedCompanyId, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Attribute0Id, input.Attribute1Id, input.Attribute2Id, input.Attribute3Id, input.Attribute4Id, input.Attribute5Id, input.Attribute6Id, input.Attribute7Id, input.Attribute8Id, input.Attribute9Id, input.Attribute10Id, input.Attribute11Id, input.Attribute12Id, input.Attribute13Id, input.Attribute14Id, input.Attribute15Id, input.Attribute16Id, input.Attribute17Id, input.Attribute18Id, input.Attribute19Id, input.PaymentId);
            var items = await _customerRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.Phone1, input.Phone2, input.erpCode, input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.CreditLimitMin, input.CreditLimitMax, input.IsCompany, input.WarehouseId, input.Street, input.Address, input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDateMin, input.LastOrderDateMax, input.PaymentTermId, input.LinkedCompanyId, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Attribute0Id, input.Attribute1Id, input.Attribute2Id, input.Attribute3Id, input.Attribute4Id, input.Attribute5Id, input.Attribute6Id, input.Attribute7Id, input.Attribute8Id, input.Attribute9Id, input.Attribute10Id, input.Attribute11Id, input.Attribute12Id, input.Attribute13Id, input.Attribute14Id, input.Attribute15Id, input.Attribute16Id, input.Attribute17Id, input.Attribute18Id, input.Attribute19Id, input.PaymentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerWithNavigationProperties>, List<CustomerWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerWithNavigationProperties, CustomerWithNavigationPropertiesDto>
                (await _customerRepository.GetWithNavigationPropertiesAsync(id));
        }
        public virtual async Task<CustomerDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Customer, CustomerDto>(await _customerRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            var query = (await _systemDataRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.ValueCode != null &&
                         x.ValueCode.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SystemData>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SystemData>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            var query = (await _priceListRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PriceList>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceList>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            var query = (await _geoMasterRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<GeoMaster>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<GeoMaster>, List<LookupDto<Guid>>>(lookupData)
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Customer>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Customer>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.Customers.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Customers.Create)]
        public virtual async Task<CustomerDto> CreateAsync(CustomerCreateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }

            var customer = await _customerManager.CreateAsync(
            input.PaymentTermId, input.LinkedCompanyId, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Attribute0Id, input.Attribute1Id, input.Attribute2Id, input.Attribute3Id, input.Attribute4Id, input.Attribute5Id, input.Attribute6Id, input.Attribute7Id, input.Attribute8Id, input.Attribute9Id, input.Attribute10Id, input.Attribute11Id, input.Attribute12Id, input.Attribute13Id, input.Attribute14Id, input.Attribute15Id, input.Attribute16Id, input.Attribute17Id, input.Attribute18Id, input.Attribute19Id, input.PaymentId, input.Code, input.Name, input.Phone1, input.Phone2, input.erpCode, input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active, input.EffectiveDate, input.IsCompany, input.WarehouseId, input.Street, input.Address, input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDate, input.EndDate, input.CreditLimit
            );

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        [Authorize(MdmServicePermissions.Customers.Edit)]
        public virtual async Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }

            var customer = await _customerManager.UpdateAsync(
            id,
            input.PaymentTermId, input.LinkedCompanyId, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Attribute0Id, input.Attribute1Id, input.Attribute2Id, input.Attribute3Id, input.Attribute4Id, input.Attribute5Id, input.Attribute6Id, input.Attribute7Id, input.Attribute8Id, input.Attribute9Id, input.Attribute10Id, input.Attribute11Id, input.Attribute12Id, input.Attribute13Id, input.Attribute14Id, input.Attribute15Id, input.Attribute16Id, input.Attribute17Id, input.Attribute18Id, input.Attribute19Id, input.PaymentId, input.Code, input.Name, input.Phone1, input.Phone2, input.erpCode, input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active, input.EffectiveDate, input.IsCompany, input.WarehouseId, input.Street, input.Address, input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDate, input.EndDate, input.CreditLimit, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var customers = await _customerRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.Phone1, input.Phone2, input.erpCode, input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.CreditLimitMin, input.CreditLimitMax, input.IsCompany, input.WarehouseId, input.Street, input.Address, input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDateMin, input.LastOrderDateMax);
            var items = customers.Select(item => new
            {
                item.Customer.Code,
                item.Customer.Name,
                item.Customer.Phone1,
                item.Customer.Phone2,
                item.Customer.erpCode,
                item.Customer.License,
                item.Customer.TaxCode,
                item.Customer.vatName,
                item.Customer.vatAddress,
                item.Customer.Active,
                item.Customer.EffectiveDate,
                item.Customer.EndDate,
                item.Customer.CreditLimit,
                item.Customer.IsCompany,
                item.Customer.WarehouseId,
                item.Customer.Street,
                item.Customer.Address,
                item.Customer.Latitude,
                item.Customer.Longitude,
                item.Customer.SFACustomerCode,
                item.Customer.LastOrderDate,

                SystemDataValueCode = item.SystemData?.ValueCode,
                CompanyCode = item.Company?.Code,
                PriceListCode = item.PriceList?.Code,
                GeoMasterCode = item.GeoMaster?.Code,
                GeoMasterCode1 = item.GeoMaster1?.Code,
                GeoMasterCode2 = item.GeoMaster2?.Code,
                GeoMasterCode3 = item.GeoMaster3?.Code,
                GeoMasterCode4 = item.GeoMaster4?.Code,
                CusAttributeValueAttrValName = item.CusAttributeValue?.AttrValName,
                CusAttributeValueAttrValName1 = item.CusAttributeValue1?.AttrValName,
                CusAttributeValueAttrValName2 = item.CusAttributeValue2?.AttrValName,
                CusAttributeValueAttrValName3 = item.CusAttributeValue3?.AttrValName,
                CusAttributeValueAttrValName4 = item.CusAttributeValue4?.AttrValName,
                CusAttributeValueAttrValName5 = item.CusAttributeValue5?.AttrValName,
                CusAttributeValueAttrValName6 = item.CusAttributeValue6?.AttrValName,
                CusAttributeValueAttrValName7 = item.CusAttributeValue7?.AttrValName,
                CusAttributeValueAttrValName8 = item.CusAttributeValue8?.AttrValName,
                CusAttributeValueAttrValName9 = item.CusAttributeValue9?.AttrValName,
                CusAttributeValueAttrValName10 = item.CusAttributeValue10?.AttrValName,
                CusAttributeValueAttrValName11 = item.CusAttributeValue11?.AttrValName,
                CusAttributeValueAttrValName12 = item.CusAttributeValue12?.AttrValName,
                CusAttributeValueAttrValName13 = item.CusAttributeValue13?.AttrValName,
                CusAttributeValueAttrValName14 = item.CusAttributeValue14?.AttrValName,
                CusAttributeValueAttrValName15 = item.CusAttributeValue15?.AttrValName,
                CusAttributeValueAttrValName16 = item.CusAttributeValue16?.AttrValName,
                CusAttributeValueAttrValName17 = item.CusAttributeValue17?.AttrValName,
                CusAttributeValueAttrValName18 = item.CusAttributeValue18?.AttrValName,
                CusAttributeValueAttrValName19 = item.CusAttributeValue19?.AttrValName,
                CustomerCode = item.Customer?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Customers.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerExcelDownloadTokenCacheItem { Token = token },
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