// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.EmployeeAttachments;
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
namespace DMSpro.OMS.MdmService.Controllers.EmployeeAttachments;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IEmployeeAttachmentsAppService), typeof(EmployeeAttachmentClientProxy))]
public partial class EmployeeAttachmentClientProxy : ClientProxyBase<IEmployeeAttachmentsAppService>, IEmployeeAttachmentsAppService
{
    public virtual async Task<PagedResultDto<EmployeeAttachmentWithNavigationPropertiesDto>> GetListAsync(GetEmployeeAttachmentsInput input)
    {
        return await RequestAsync<PagedResultDto<EmployeeAttachmentWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetEmployeeAttachmentsInput), input }
        });
    }

    public virtual async Task<EmployeeAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<EmployeeAttachmentWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<EmployeeAttachmentDto> GetAsync(Guid id)
    {
        return await RequestAsync<EmployeeAttachmentDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetEmployeeProfileLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeAttachmentExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(EmployeeAttachmentExcelDownloadDto), input }
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

    public virtual async Task<EmployeeAttachmentDto> CreateAsync(Guid employeeId, IRemoteStreamContent inputFile, string description, bool active)
    {
        return await RequestAsync<EmployeeAttachmentDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), employeeId },
            { typeof(IRemoteStreamContent), inputFile },
            { typeof(string), description },
            { typeof(bool), active }
        });
    }

    public virtual async Task<EmployeeAttachmentDto> UpdateAsync(Guid id, Guid employeeId, IRemoteStreamContent inputFile, string description, bool active)
    {
        return await RequestAsync<EmployeeAttachmentDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(Guid), employeeId },
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
