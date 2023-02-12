// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
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
namespace DMSpro.OMS.MdmService.Controllers.CompanyIdentityUserAssignments;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ICompanyIdentityUserAssignmentsAppService), typeof(CompanyIdentityUserAssignmentClientProxy))]
public partial class CompanyIdentityUserAssignmentClientProxy : ClientProxyBase<ICompanyIdentityUserAssignmentsAppService>, ICompanyIdentityUserAssignmentsAppService
{
    public virtual async Task<PagedResultDto<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>> GetListAsync(GetCompanyIdentityUserAssignmentsInput input)
    {
        return await RequestAsync<PagedResultDto<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetCompanyIdentityUserAssignmentsInput), input }
        });
    }

    public virtual async Task<CompanyIdentityUserAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<CompanyIdentityUserAssignmentDto> GetAsync(Guid id)
    {
        return await RequestAsync<CompanyIdentityUserAssignmentDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetCompanyLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<CompanyIdentityUserAssignmentDto> CreateAsync(CompanyIdentityUserAssignmentCreateDto input)
    {
        return await RequestAsync<CompanyIdentityUserAssignmentDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CompanyIdentityUserAssignmentCreateDto), input }
        });
    }

    public virtual async Task<CompanyIdentityUserAssignmentDto> UpdateAsync(Guid id, CompanyIdentityUserAssignmentUpdateDto input)
    {
        return await RequestAsync<CompanyIdentityUserAssignmentDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(CompanyIdentityUserAssignmentUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyIdentityUserAssignmentExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CompanyIdentityUserAssignmentExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }

    public virtual async Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev)
    {
        return await RequestAsync<LoadResult>(nameof(GetListCompanyByCurrentUserAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DataLoadOptionDevextreme), inputDev }
        });
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
