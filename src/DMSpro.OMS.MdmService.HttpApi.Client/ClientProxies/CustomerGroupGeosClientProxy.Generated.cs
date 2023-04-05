using DMSpro.OMS.MdmService.Shared;
// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.CustomerGroupGeos;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.CustomerGroupGeos.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ICustomerGroupGeosAppService), typeof(CustomerGroupGeosClientProxy))]
public partial class CustomerGroupGeosClientProxy : ClientProxyBase<ICustomerGroupGeosAppService>, ICustomerGroupGeosAppService
{
    public virtual async Task<PagedResultDto<CustomerGroupGeoWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupGeosInput input)
    {
        return await RequestAsync<PagedResultDto<CustomerGroupGeoWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetCustomerGroupGeosInput), input }
        });
    }
    public virtual async Task<CustomerGroupGeoWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<CustomerGroupGeoWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<CustomerGroupGeoDto> GetAsync(Guid id)
    {
        return await RequestAsync<CustomerGroupGeoDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetCustomerGroupLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetGeoMasterLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetGeoMasterLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<CustomerGroupGeoDto> CreateAsync(CustomerGroupGeoCreateDto input)
    {
        return await RequestAsync<CustomerGroupGeoDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CustomerGroupGeoCreateDto), input }
        });
    }

    public virtual async Task<CustomerGroupGeoDto> UpdateAsync(Guid id, CustomerGroupGeoUpdateDto input)
    {
        return await RequestAsync<CustomerGroupGeoDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(CustomerGroupGeoUpdateDto), input }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupGeoExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CustomerGroupGeoExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}