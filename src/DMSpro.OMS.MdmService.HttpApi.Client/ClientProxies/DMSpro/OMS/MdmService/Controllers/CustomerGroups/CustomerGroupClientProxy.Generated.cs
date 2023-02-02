// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
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
namespace DMSpro.OMS.MdmService.Controllers.CustomerGroups;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ICustomerGroupsAppService), typeof(CustomerGroupClientProxy))]
public partial class CustomerGroupClientProxy : ClientProxyBase<ICustomerGroupsAppService>, ICustomerGroupsAppService
{
    public virtual async Task<PagedResultDto<CustomerGroupDto>> GetListAsync(GetCustomerGroupsInput input)
    {
        return await RequestAsync<PagedResultDto<CustomerGroupDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetCustomerGroupsInput), input }
        });
    }

    public virtual async Task<CustomerGroupDto> GetAsync(Guid id)
    {
        return await RequestAsync<CustomerGroupDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<CustomerGroupDto> CreateAsync(CustomerGroupCreateDto input)
    {
        return await RequestAsync<CustomerGroupDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CustomerGroupCreateDto), input }
        });
    }

    public virtual async Task<CustomerGroupDto> UpdateAsync(Guid id, CustomerGroupUpdateDto input)
    {
        return await RequestAsync<CustomerGroupDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(CustomerGroupUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CustomerGroupExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }

    public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
    {
        return await RequestAsync<LoadResult>(nameof(GetListDevextremesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DataLoadOptionDevextreme), inputDev }
        });
    }

    public virtual async Task<int> UpdateFromExcelAsync(IFormFile file)
    {
        return await RequestAsync<int>(nameof(UpdateFromExcelAsync), new ClientProxyRequestTypeValue
        {
            { typeof(IFormFile), file }
        });
    }

    public virtual async Task<int> InsertFromExcelAsync(IFormFile file)
    {
        return await RequestAsync<int>(nameof(InsertFromExcelAsync), new ClientProxyRequestTypeValue
        {
            { typeof(IFormFile), file }
        });
    }
}