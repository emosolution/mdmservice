// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.ItemAttachments;
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
namespace DMSpro.OMS.MdmService.Controllers.ItemAttachments;

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

    public virtual async Task DeleteManyAsync(List<Guid> ids)
    {
        await RequestAsync(nameof(DeleteManyAsync), new ClientProxyRequestTypeValue
        {
            { typeof(List<Guid>), ids }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetFileAsync(Guid id)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<ItemAttachmentDto> CreateAsync(Guid itemId, IRemoteStreamContent inputFile, string description, bool active)
    {
        return await RequestAsync<ItemAttachmentDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), itemId },
            { typeof(IRemoteStreamContent), inputFile },
            { typeof(string), description },
            { typeof(bool), active }
        });
    }

    public virtual async Task<ItemAttachmentDto> UpdateAsync(Guid id, Guid itemId, IRemoteStreamContent inputFile, string description, bool active)
    {
        return await RequestAsync<ItemAttachmentDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(Guid), itemId },
            { typeof(IRemoteStreamContent), inputFile },
            { typeof(string), description },
            { typeof(bool), active }
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
