// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.VisitPlans;
using DMSpro.OMS.MdmService.Shared;
using Volo.Abp.Content;
using System.Collections.Generic;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.VisitPlans.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IVisitPlansAppService), typeof(VisitPlanClientProxy))]
public partial class VisitPlanClientProxy : ClientProxyBase<IVisitPlansAppService>, IVisitPlansAppService
{
    public virtual async Task<PagedResultDto<VisitPlanWithNavigationPropertiesDto>> GetListAsync(GetVisitPlansInput input)
    {
        return await RequestAsync<PagedResultDto<VisitPlanWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetVisitPlansInput), input }
        });
    }

    public virtual async Task<VisitPlanWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<VisitPlanWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<VisitPlanDto> GetAsync(Guid id)
    {
        return await RequestAsync<VisitPlanDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetMCPDetailLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetMCPDetailLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetCustomerLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetSalesOrgHierarchyLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetCompanyLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetItemGroupLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid?>>>(nameof(GetItemGroupLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<VisitPlanDto> CreateAsync(VisitPlanCreateDto input)
    {
        return await RequestAsync<VisitPlanDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(VisitPlanCreateDto), input }
        });
    }

    public virtual async Task<VisitPlanDto> UpdateAsync(Guid id, VisitPlanUpdateDto input)
    {
        return await RequestAsync<VisitPlanDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(VisitPlanUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VisitPlanExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(VisitPlanExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }

    public virtual async Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
    {
        return await RequestAsync<List<VisitPlanDto>>(nameof(GenerateWithPermissionAsync), new ClientProxyRequestTypeValue
        {
            { typeof(VisitPlanGenerationInputDto), input }
        });
    }

    public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
    {
        return await RequestAsync<LoadResult>(nameof(GetListDevextremesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DataLoadOptionDevextreme), inputDev }
        });
    }
}