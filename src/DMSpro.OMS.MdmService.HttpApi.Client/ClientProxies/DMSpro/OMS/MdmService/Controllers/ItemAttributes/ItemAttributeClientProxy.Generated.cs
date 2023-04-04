// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.ItemAttributes;
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
namespace DMSpro.OMS.MdmService.Controllers.ItemAttributes;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IItemAttributesAppService), typeof(ItemAttributeClientProxy))]
public partial class ItemAttributeClientProxy : ClientProxyBase<IItemAttributesAppService>, IItemAttributesAppService
{
    public virtual async Task<ItemAttributeDto> GetAsync(Guid id)
    {
        return await RequestAsync<ItemAttributeDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<LoadResult> UpdateAsync(Guid id, ItemAttributeUpdateDto input)
    {
        return await RequestAsync<LoadResult>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(ItemAttributeUpdateDto), input }
        });
    }

    public virtual async Task<LoadResult> DeleteAsync()
    {
        return await RequestAsync<LoadResult>(nameof(DeleteAsync));
    }

    public virtual async Task<LoadResult> CreateHierarchyAsync(ItemAttributeCreateDto input)
    {
        return await RequestAsync<LoadResult>(nameof(CreateHierarchyAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemAttributeCreateDto), input }
        });
    }

    public virtual async Task<LoadResult> CreateFlatAsync(ItemAttributeCreateDto input)
    {
        return await RequestAsync<LoadResult>(nameof(CreateFlatAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemAttributeCreateDto), input }
        });
    }

    public virtual async Task<LoadResult> ResetAsync()
    {
        return await RequestAsync<LoadResult>(nameof(ResetAsync));
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
