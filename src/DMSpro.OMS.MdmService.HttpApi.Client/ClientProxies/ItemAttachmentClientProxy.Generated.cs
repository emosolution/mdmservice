// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.ItemAttachments;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.MdmService.Shared;
using Volo.Abp.Content;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.ItemAttachments.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IItemAttachmentsAppService), typeof(ItemAttachmentClientProxy))]
public partial class ItemAttachmentClientProxy : ClientProxyBase<IItemAttachmentsAppService>, IItemAttachmentsAppService
{
    public virtual async Task<PagedResultDto<ItemAttachmentWithNavigationPropertiesDto>> GetListAsync(GetItemAttachmentsInput input)
    {
        return await RequestAsync<PagedResultDto<ItemAttachmentWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetItemAttachmentsInput), input }
        });
    }

    public virtual async Task<ItemAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<ItemAttachmentWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
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

    public virtual async Task<ItemAttachmentDto> GetAsync(Guid id)
    {
        return await RequestAsync<ItemAttachmentDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetItemLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<ItemAttachmentDto> CreateAsync(ItemAttachmentCreateDto input)
    {
        return await RequestAsync<ItemAttachmentDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemAttachmentCreateDto), input }
        });
    }

    public virtual async Task<ItemAttachmentDto> UpdateAsync(Guid id, ItemAttachmentUpdateDto input)
    {
        return await RequestAsync<ItemAttachmentDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(ItemAttachmentUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttachmentExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ItemAttachmentExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}