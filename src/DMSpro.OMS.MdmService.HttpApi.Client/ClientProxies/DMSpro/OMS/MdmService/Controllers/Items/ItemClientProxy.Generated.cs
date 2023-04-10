// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.Items;
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
namespace DMSpro.OMS.MdmService.Controllers.Items;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IItemsAppService), typeof(ItemClientProxy))]
public partial class ItemClientProxy : ClientProxyBase<IItemsAppService>, IItemsAppService
{
    public virtual async Task<ItemDto> GetAsync(Guid id)
    {
        return await RequestAsync<ItemDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<ItemDto> CreateAsync(ItemCreateDto input)
    {
        return await RequestAsync<ItemDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemCreateDto), input }
        });
    }

    public virtual async Task<ItemDto> UpdateAsync(Guid id, ItemUpdateDto input)
    {
        return await RequestAsync<ItemDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(ItemUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<ItemProfileDto> GetItemProfileAsync(Guid id)
    {
        return await RequestAsync<ItemProfileDto>(nameof(GetItemProfileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
    {
        return await RequestAsync<LoadResult>(nameof(GetListDevextremesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DataLoadOptionDevextreme), inputDev }
        });
    }

    public virtual async Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
    {
        return await RequestAsync<int>(nameof(UpdateFromExcelAsync), new ClientProxyRequestTypeValue
        {
            { typeof(IRemoteStreamContent), file }
        });
    }

    public virtual async Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
    {
        return await RequestAsync<int>(nameof(InsertFromExcelAsync), new ClientProxyRequestTypeValue
        {
            { typeof(IRemoteStreamContent), file }
        });
    }

    public virtual async Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GenerateExcelTemplatesAsync));
    }
}
