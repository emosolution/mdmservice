// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.SystemDatas;
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
namespace DMSpro.OMS.MdmService.Controllers.SystemDatas;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ISystemDatasAppService), typeof(SystemDataClientProxy))]
public partial class SystemDataClientProxy : ClientProxyBase<ISystemDatasAppService>, ISystemDatasAppService
{
    public virtual async Task<PagedResultDto<SystemDataDto>> GetListAsync(GetSystemDatasInput input)
    {
        return await RequestAsync<PagedResultDto<SystemDataDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetSystemDatasInput), input }
        });
    }

    public virtual async Task<SystemDataDto> GetAsync(Guid id)
    {
        return await RequestAsync<SystemDataDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<SystemDataDto> CreateAsync(SystemDataCreateDto input)
    {
        return await RequestAsync<SystemDataDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(SystemDataCreateDto), input }
        });
    }

    public virtual async Task<SystemDataDto> UpdateAsync(Guid id, SystemDataUpdateDto input)
    {
        return await RequestAsync<SystemDataDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(SystemDataUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SystemDataExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(SystemDataExcelDownloadDto), input }
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
