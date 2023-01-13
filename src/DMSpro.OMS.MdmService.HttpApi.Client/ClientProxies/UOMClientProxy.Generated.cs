// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.UOMs;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.UOMs.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IUOMsAppService), typeof(UOMClientProxy))]
public partial class UOMClientProxy : ClientProxyBase<IUOMsAppService>, IUOMsAppService
{
    public virtual async Task<PagedResultDto<UOMDto>> GetListAsync(GetUOMsInput input)
    {
        return await RequestAsync<PagedResultDto<UOMDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetUOMsInput), input }
        });
    }

    public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
    {
        return await RequestAsync<LoadResult>(nameof(GetListDevextremesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DataLoadOptionDevextreme), inputDev }
        });
    }

    public virtual async Task<UOMDto> GetAsync(Guid id)
    {
        return await RequestAsync<UOMDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<UOMDto> CreateAsync(UOMCreateDto input)
    {
        return await RequestAsync<UOMDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(UOMCreateDto), input }
        });
    }

    public virtual async Task<UOMDto> UpdateAsync(Guid id, UOMUpdateDto input)
    {
        return await RequestAsync<UOMDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(UOMUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(UOMExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}