// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using DMSpro.OMS.MdmService.Shared;
using Volo.Abp.Content;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.ItemGroupAttributes.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IItemGroupAttributesAppService), typeof(ItemGroupAttributeClientProxy))]
public partial class ItemGroupAttributeClientProxy : ClientProxyBase<IItemGroupAttributesAppService>, IItemGroupAttributesAppService
{
    public virtual async Task<PagedResultDto<ItemGroupAttributeWithNavigationPropertiesDto>> GetListAsync(GetItemGroupAttributesInput input)
    {
        return await RequestAsync<PagedResultDto<ItemGroupAttributeWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetItemGroupAttributesInput), input }
        });
    }

    public virtual async Task<ItemGroupAttributeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<ItemGroupAttributeWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<ItemGroupAttributeDto> GetAsync(Guid id)
    {
        return await RequestAsync<ItemGroupAttributeDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetItemGroupLookupAsync), new ClientProxyRequestTypeValue
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

    public virtual async Task<ItemGroupAttributeDto> CreateAsync(ItemGroupAttributeCreateDto input)
    {
        return await RequestAsync<ItemGroupAttributeDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemGroupAttributeCreateDto), input }
        });
    }

    public virtual async Task<ItemGroupAttributeDto> UpdateAsync(Guid id, ItemGroupAttributeUpdateDto input)
    {
        return await RequestAsync<ItemGroupAttributeDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(ItemGroupAttributeUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupAttributeExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemGroupAttributeExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}
