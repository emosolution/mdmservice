// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.SalesOrgHeaders.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ISalesOrgHeadersAppService), typeof(SalesOrgHeaderClientProxy))]
public partial class SalesOrgHeaderClientProxy : ClientProxyBase<ISalesOrgHeadersAppService>, ISalesOrgHeadersAppService
{
    public virtual async Task<PagedResultDto<SalesOrgHeaderDto>> GetListAsync(GetSalesOrgHeadersInput input)
    {
        return await RequestAsync<PagedResultDto<SalesOrgHeaderDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetSalesOrgHeadersInput), input }
        });
    }

    public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
    {
        return await RequestAsync<LoadResult>(nameof(GetListDevextremesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DataLoadOptionDevextreme), inputDev }
        });
    }

    public virtual async Task<SalesOrgHeaderDto> GetAsync(Guid id)
    {
        return await RequestAsync<SalesOrgHeaderDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input)
    {
        return await RequestAsync<SalesOrgHeaderDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(SalesOrgHeaderCreateDto), input }
        });
    }

    public virtual async Task<SalesOrgHeaderDto> UpdateAsync(Guid id, SalesOrgHeaderUpdateDto input)
    {
        return await RequestAsync<SalesOrgHeaderDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(SalesOrgHeaderUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHeaderExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(SalesOrgHeaderExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}