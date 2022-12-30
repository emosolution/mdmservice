using DMSpro.OMS.MdmService.Shared;
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
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using static DMSpro.OMS.MdmService.Permissions.MdmServicePermissions;
using DMSpro.OMS.MdmService.Customers;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.CustomerContacts
{

    [Authorize(MdmServicePermissions.Customers.Default)]
    public class CustomerContactsAppService : ApplicationService, ICustomerContactsAppService
    {
        private readonly IDistributedCache<CustomerContactExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerContactRepository _customerContactRepository;
        private readonly CustomerContactManager _customerContactManager;
        private readonly IRepository<Customer, Guid> _customerRepository;

        public CustomerContactsAppService(ICustomerContactRepository customerContactRepository, CustomerContactManager customerContactManager,
            IRepository<Customer, Guid> customerRepository,
            IDistributedCache<CustomerContactExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerContactRepository = customerContactRepository;
            _customerContactManager = customerContactManager; 
            _customerRepository = customerRepository;
        }

        public virtual async Task<PagedResultDto<CustomerContactWithNavigationPropertiesDto>> GetListAsync(GetCustomerContactsInput input)
        {
            var totalCount = await _customerContactRepository.GetCountAsync(input.FilterText, input.Title, input.FirstName, input.LastName, input.Gender, input.DateOfBirthMin, input.DateOfBirthMax, input.Phone, input.Email, input.Address, input.IdentityNumber, input.BankName, input.BankAccName, input.BankAccNumber, input.CustomerId);
            var items = await _customerContactRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Title, input.FirstName, input.LastName, input.Gender, input.DateOfBirthMin, input.DateOfBirthMax, input.Phone, input.Email, input.Address, input.IdentityNumber, input.BankName, input.BankAccName, input.BankAccNumber, input.CustomerId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerContactWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerContactWithNavigationProperties>, List<CustomerContactWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerContactWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerContactWithNavigationProperties, CustomerContactWithNavigationPropertiesDto>
                (await _customerContactRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _customerContactRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<CustomerContact>, IEnumerable<CustomerContactDto>>(results.data.Cast<CustomerContact>());
            
            return results;
                
        }

        public virtual async Task<CustomerContactDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerContact, CustomerContactDto>(await _customerContactRepository.GetAsync(id));
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
            await _customerContactRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Customers.Create)]
        public virtual async Task<CustomerContactDto> CreateAsync(CustomerContactCreateDto input)
        {
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }

            var customerContact = await _customerContactManager.CreateAsync(
            input.CustomerId, input.FirstName, input.LastName, input.Gender, input.Phone, input.Email, input.Address, input.IdentityNumber, input.BankName, input.BankAccName, input.BankAccNumber, input.Title, input.DateOfBirth
            );

            return ObjectMapper.Map<CustomerContact, CustomerContactDto>(customerContact);
        }

        [Authorize(MdmServicePermissions.Customers.Edit)]
        public virtual async Task<CustomerContactDto> UpdateAsync(Guid id, CustomerContactUpdateDto input)
        {
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }

            var customerContact = await _customerContactManager.UpdateAsync(
            id,
            input.CustomerId, input.FirstName, input.LastName, input.Gender, input.Phone, input.Email, input.Address, input.IdentityNumber, input.BankName, input.BankAccName, input.BankAccNumber, input.Title, input.DateOfBirth, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerContact, CustomerContactDto>(customerContact);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerContactExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerContactRepository.GetListAsync(input.FilterText, input.Title, input.FirstName, input.LastName, input.Gender, input.DateOfBirthMin, input.DateOfBirthMax, input.Phone, input.Email, input.Address, input.IdentityNumber, input.BankName, input.BankAccName, input.BankAccNumber);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerContact>, List<CustomerContactExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerContacts.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerContactExcelDownloadTokenCacheItem { Token = token },
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