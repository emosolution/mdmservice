// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.MdmService.Shared;
using Volo.Abp.Content;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.SalesOrgHierarchies.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ISalesOrgHierarchiesAppService), typeof(SalesOrgHierarchyClientProxy))]
public partial class SalesOrgHierarchyClientProxy : ClientProxyBase<ISalesOrgHierarchiesAppService>, ISalesOrgHierarchiesAppService
{
    public virtual async Task<PagedResultDto<SalesOrgHierarchyWithNavigationPropertiesDto>> GetListAsync(GetSalesOrgHierarchiesInput input)
    {
        return await RequestAsync<PagedResultDto<SalesOrgHierarchyWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetSalesOrgHierarchiesInput), input }
        });
    }

    public virtual async Task<SalesOrgHierarchyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<SalesOrgHierarchyWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
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

    public virtual async Task<SalesOrgHierarchyDto> GetAsync(Guid id)
    {
        return await RequestAsync<SalesOrgHierarchyDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHeaderLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetSalesOrgHeaderLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid?>>>(nameof(GetSalesOrgHierarchyLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<SalesOrgHierarchyDto> CreateAsync(SalesOrgHierarchyCreateDto input)
    {
        return await RequestAsync<SalesOrgHierarchyDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(SalesOrgHierarchyCreateDto), input }
        });
    }

    public virtual async Task<SalesOrgHierarchyDto> UpdateAsync(Guid id, SalesOrgHierarchyUpdateDto input)
    {
        return await RequestAsync<SalesOrgHierarchyDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(SalesOrgHierarchyUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHierarchyExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(SalesOrgHierarchyExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}