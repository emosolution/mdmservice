// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.CustomerGroupByAtts;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.ClientProxying;
using Volo.Abp.Http.Modeling;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.CustomerGroupByAtts;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ICustomerGroupByAttsAppService), typeof(CustomerGroupByAttClientProxy))]
public partial class CustomerGroupByAttClientProxy : ClientProxyBase<ICustomerGroupByAttsAppService>, ICustomerGroupByAttsAppService
{
    public virtual async Task<PagedResultDto<CustomerGroupByAttWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByAttsInput input)
    {
        return await RequestAsync<PagedResultDto<CustomerGroupByAttWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetCustomerGroupByAttsInput), input }
        });
    }

    public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
    {
        return await RequestAsync<LoadResult>(nameof(GetListDevextremesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DataLoadOptionDevextreme), inputDev }
        });
    }

    public virtual async Task<CustomerGroupByAttWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<CustomerGroupByAttWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<CustomerGroupByAttDto> GetAsync(Guid id)
    {
        return await RequestAsync<CustomerGroupByAttDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
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

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCusAttributeValueLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetCusAttributeValueLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<CustomerGroupByAttDto> CreateAsync(CustomerGroupByAttCreateDto input)
    {
        return await RequestAsync<CustomerGroupByAttDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CustomerGroupByAttCreateDto), input }
        });
    }

    public virtual async Task<CustomerGroupByAttDto> UpdateAsync(Guid id, CustomerGroupByAttUpdateDto input)
    {
        return await RequestAsync<CustomerGroupByAttDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(CustomerGroupByAttUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByAttExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CustomerGroupByAttExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}
