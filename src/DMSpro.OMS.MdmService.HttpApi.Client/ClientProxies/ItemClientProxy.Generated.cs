// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.Items;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.MdmService.Shared;
using Volo.Abp.Content;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.Items.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IItemsAppService), typeof(ItemClientProxy))]
public partial class ItemClientProxy : ClientProxyBase<IItemsAppService>, IItemsAppService
{
    public virtual async Task<PagedResultDto<ItemWithNavigationPropertiesDto>> GetListAsync(GetItemsInput input)
    {
        return await RequestAsync<PagedResultDto<ItemWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetItemsInput), input }
        });
    }

    public virtual async Task<ItemWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<ItemWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
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

    public virtual async Task<ItemDto> GetAsync(Guid id)
    {
        return await RequestAsync<ItemDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetSystemDataLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetVATLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetVATLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetUOMGroupLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupDetailLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetUOMGroupDetailLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid?>>>(nameof(GetItemAttributeValueLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
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

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}