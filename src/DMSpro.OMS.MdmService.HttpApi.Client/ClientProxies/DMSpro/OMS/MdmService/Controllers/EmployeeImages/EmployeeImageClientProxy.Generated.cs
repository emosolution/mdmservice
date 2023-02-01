// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.EmployeeImages;
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
namespace DMSpro.OMS.MdmService.Controllers.EmployeeImages;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IEmployeeImagesAppService), typeof(EmployeeImageClientProxy))]
public partial class EmployeeImageClientProxy : ClientProxyBase<IEmployeeImagesAppService>, IEmployeeImagesAppService
{
    public virtual async Task<PagedResultDto<EmployeeImageWithNavigationPropertiesDto>> GetListAsync(GetEmployeeImagesInput input)
    {
        return await RequestAsync<PagedResultDto<EmployeeImageWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetEmployeeImagesInput), input }
        });
    }

    public virtual async Task<EmployeeImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<EmployeeImageWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
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

    public virtual async Task<EmployeeImageDto> GetAsync(Guid id)
    {
        return await RequestAsync<EmployeeImageDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
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

    public virtual async Task<EmployeeImageDto> CreateAsync(EmployeeImageCreateDto input)
    {
        return await RequestAsync<EmployeeImageDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(EmployeeImageCreateDto), input }
        });
    }

    public virtual async Task<EmployeeImageDto> UpdateAsync(Guid id, EmployeeImageUpdateDto input)
    {
        return await RequestAsync<EmployeeImageDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(EmployeeImageUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeImageExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(EmployeeImageExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}
