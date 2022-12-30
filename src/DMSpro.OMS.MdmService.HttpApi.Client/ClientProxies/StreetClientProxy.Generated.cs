// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.Streets;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.Streets.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IStreetsAppService), typeof(StreetClientProxy))]
public partial class StreetClientProxy : ClientProxyBase<IStreetsAppService>, IStreetsAppService
{
    public virtual async Task<PagedResultDto<StreetDto>> GetListAsync(GetStreetsInput input)
    {
        return await RequestAsync<PagedResultDto<StreetDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetStreetsInput), input }
        });
    }

    public virtual async Task<StreetDto> GetAsync(Guid id)
    {
        return await RequestAsync<StreetDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<StreetDto> CreateAsync(StreetCreateDto input)
    {
        return await RequestAsync<StreetDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(StreetCreateDto), input }
        });
    }

    public virtual async Task<StreetDto> UpdateAsync(Guid id, StreetUpdateDto input)
    {
        return await RequestAsync<StreetDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(StreetUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(StreetExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(StreetExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}
