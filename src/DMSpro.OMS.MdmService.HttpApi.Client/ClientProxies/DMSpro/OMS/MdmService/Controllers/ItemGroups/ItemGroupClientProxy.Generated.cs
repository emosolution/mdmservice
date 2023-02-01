// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.ItemGroups;
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
namespace DMSpro.OMS.MdmService.Controllers.ItemGroups;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IItemGroupsAppService), typeof(ItemGroupClientProxy))]
public partial class ItemGroupClientProxy : ClientProxyBase<IItemGroupsAppService>, IItemGroupsAppService
{
    public virtual async Task<PagedResultDto<ItemGroupDto>> GetListAsync(GetItemGroupsInput input)
    {
        return await RequestAsync<PagedResultDto<ItemGroupDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetItemGroupsInput), input }
        });
    }

    public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
    {
        return await RequestAsync<LoadResult>(nameof(GetListDevextremesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DataLoadOptionDevextreme), inputDev }
        });
    }

    public virtual async Task<ItemGroupDto> GetAsync(Guid id)
    {
        return await RequestAsync<ItemGroupDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<ItemGroupDto> CreateAsync(ItemGroupCreateDto input)
    {
        return await RequestAsync<ItemGroupDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemGroupCreateDto), input }
        });
    }

    public virtual async Task<ItemGroupDto> UpdateAsync(Guid id, ItemGroupUpdateDto input)
    {
        return await RequestAsync<ItemGroupDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(ItemGroupUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemGroupExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}
