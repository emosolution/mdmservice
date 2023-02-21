// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.CustomerAttachments;
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
namespace DMSpro.OMS.MdmService.Controllers.CustomerAttachments;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ICustomerAttachmentsAppService), typeof(CustomerAttachmentClientProxy))]
public partial class CustomerAttachmentClientProxy : ClientProxyBase<ICustomerAttachmentsAppService>, ICustomerAttachmentsAppService
{
    public virtual async Task<PagedResultDto<CustomerAttachmentWithNavigationPropertiesDto>> GetListAsync(GetCustomerAttachmentsInput input)
    {
        return await RequestAsync<PagedResultDto<CustomerAttachmentWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetCustomerAttachmentsInput), input }
        });
    }

    public virtual async Task<CustomerAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<CustomerAttachmentWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<CustomerAttachmentDto> GetAsync(Guid id)
    {
        return await RequestAsync<CustomerAttachmentDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetCustomerLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<CustomerAttachmentDto> CreateAsync(CustomerAttachmentCreateDto input)
    {
        return await RequestAsync<CustomerAttachmentDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CustomerAttachmentCreateDto), input }
        });
    }

    public virtual async Task<CustomerAttachmentDto> UpdateAsync(Guid id, CustomerAttachmentUpdateDto input)
    {
        return await RequestAsync<CustomerAttachmentDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(CustomerAttachmentUpdateDto), input }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttachmentExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CustomerAttachmentExcelDownloadDto), input }
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

    public virtual async Task<IRemoteStreamContent> GetFile(Guid id)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetFile), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id}
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
