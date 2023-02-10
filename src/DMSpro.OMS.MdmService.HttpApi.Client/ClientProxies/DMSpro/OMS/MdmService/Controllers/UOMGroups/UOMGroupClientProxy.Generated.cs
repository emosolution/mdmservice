// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.UOMGroups;
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
namespace DMSpro.OMS.MdmService.Controllers.UOMGroups;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IPartialAppService), typeof(UOMGroupClientProxy))]
public partial class UOMGroupClientProxy : ClientProxyBase<IPartialAppService>, IPartialAppService
{
    public virtual async Task<PagedResultDto<UOMGroupDto>> GetListAsync(GetUOMGroupsInput input)
    {
        return await RequestAsync<PagedResultDto<UOMGroupDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetUOMGroupsInput), input }
        });
    }

    public virtual async Task<UOMGroupDto> GetAsync(Guid id)
    {
        return await RequestAsync<UOMGroupDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<UOMGroupDto> CreateAsync(UOMGroupCreateDto input)
    {
        return await RequestAsync<UOMGroupDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(UOMGroupCreateDto), input }
        });
    }

    public virtual async Task<UOMGroupDto> UpdateAsync(Guid id, UOMGroupUpdateDto input)
    {
        return await RequestAsync<UOMGroupDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(UOMGroupUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMGroupExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(UOMGroupExcelDownloadDto), input }
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
