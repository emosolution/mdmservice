// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.EmployeeInZones;
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
namespace DMSpro.OMS.MdmService.Controllers.EmployeeInZones;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IEmployeeInZonesAppService), typeof(EmployeeInZoneClientProxy))]
public partial class EmployeeInZoneClientProxy : ClientProxyBase<IEmployeeInZonesAppService>, IEmployeeInZonesAppService
{
    public virtual async Task<PagedResultDto<EmployeeInZoneWithNavigationPropertiesDto>> GetListAsync(GetEmployeeInZonesInput input)
    {
        return await RequestAsync<PagedResultDto<EmployeeInZoneWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetEmployeeInZonesInput), input }
        });
    }

    public virtual async Task<EmployeeInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<EmployeeInZoneWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<EmployeeInZoneDto> GetAsync(Guid id)
    {
        return await RequestAsync<EmployeeInZoneDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetSalesOrgHierarchyLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetEmployeeProfileLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<EmployeeInZoneDto> CreateAsync(EmployeeInZoneCreateDto input)
    {
        return await RequestAsync<EmployeeInZoneDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(EmployeeInZoneCreateDto), input }
        });
    }

    public virtual async Task<EmployeeInZoneDto> UpdateAsync(Guid id, EmployeeInZoneUpdateDto input)
    {
        return await RequestAsync<EmployeeInZoneDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(EmployeeInZoneUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeInZoneExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(EmployeeInZoneExcelDownloadDto), input }
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
