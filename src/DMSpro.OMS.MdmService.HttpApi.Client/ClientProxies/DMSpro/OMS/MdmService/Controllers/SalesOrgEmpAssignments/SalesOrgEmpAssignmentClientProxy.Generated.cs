// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
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
namespace DMSpro.OMS.MdmService.Controllers.SalesOrgEmpAssignments;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IPartialAppService), typeof(SalesOrgEmpAssignmentClientProxy))]
public partial class SalesOrgEmpAssignmentClientProxy : ClientProxyBase<IPartialAppService>, IPartialAppService
{
    public virtual async Task<PagedResultDto<SalesOrgEmpAssignmentWithNavigationPropertiesDto>> GetListAsync(GetSalesOrgEmpAssignmentsInput input)
    {
        return await RequestAsync<PagedResultDto<SalesOrgEmpAssignmentWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetSalesOrgEmpAssignmentsInput), input }
        });
    }

    public virtual async Task<SalesOrgEmpAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<SalesOrgEmpAssignmentWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<SalesOrgEmpAssignmentDto> GetAsync(Guid id)
    {
        return await RequestAsync<SalesOrgEmpAssignmentDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
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

    public virtual async Task<SalesOrgEmpAssignmentDto> CreateAsync(SalesOrgEmpAssignmentCreateDto input)
    {
        return await RequestAsync<SalesOrgEmpAssignmentDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(SalesOrgEmpAssignmentCreateDto), input }
        });
    }

    public virtual async Task<SalesOrgEmpAssignmentDto> UpdateAsync(Guid id, SalesOrgEmpAssignmentUpdateDto input)
    {
        return await RequestAsync<SalesOrgEmpAssignmentDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(SalesOrgEmpAssignmentUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgEmpAssignmentExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(SalesOrgEmpAssignmentExcelDownloadDto), input }
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
