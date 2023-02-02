// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.ItemAttributes;
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
namespace DMSpro.OMS.MdmService.Controllers.ItemAttributes;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IItemAttributesAppService), typeof(ItemAttributeClientProxy))]
public partial class ItemAttributeClientProxy : ClientProxyBase<IItemAttributesAppService>, IItemAttributesAppService
{
    public virtual async Task<PagedResultDto<ItemAttributeDto>> GetListAsync(GetItemAttributesInput input)
    {
        return await RequestAsync<PagedResultDto<ItemAttributeDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetItemAttributesInput), input }
        });
    }

    public virtual async Task<ItemAttributeDto> GetAsync(Guid id)
    {
        return await RequestAsync<ItemAttributeDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<ItemAttributeDto> CreateAsync(ItemAttributeCreateDto input)
    {
        return await RequestAsync<ItemAttributeDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemAttributeCreateDto), input }
        });
    }

    public virtual async Task<ItemAttributeDto> UpdateAsync(Guid id, ItemAttributeUpdateDto input)
    {
        return await RequestAsync<ItemAttributeDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(ItemAttributeUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttributeExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemAttributeExcelDownloadDto), input }
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