// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Vendors;
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
namespace DMSpro.OMS.MdmService.Controllers.Vendors;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IVendorsAppService), typeof(VendorClientProxy))]
public partial class VendorClientProxy : ClientProxyBase<IVendorsAppService>, IVendorsAppService
{
    public virtual async Task<PagedResultDto<VendorWithNavigationPropertiesDto>> GetListAsync(GetVendorsInput input)
    {
        return await RequestAsync<PagedResultDto<VendorWithNavigationPropertiesDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetVendorsInput), input }
        });
    }

    public virtual async Task<VendorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return await RequestAsync<VendorWithNavigationPropertiesDto>(nameof(GetWithNavigationPropertiesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<VendorDto> GetAsync(Guid id)
    {
        return await RequestAsync<VendorDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid>>>(nameof(GetPriceListLookupAsync), new ClientProxyRequestTypeValue
        {
            { typeof(LookupRequestDto), input }
        });
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetGeoMasterLookupAsync(LookupRequestDto input)
    {
        return await RequestAsync<PagedResultDto<LookupDto<Guid?>>>(nameof(GetGeoMasterLookupAsync), new ClientProxyRequestTypeValue
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

    public virtual async Task<VendorDto> CreateAsync(VendorCreateDto input)
    {
        return await RequestAsync<VendorDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(VendorCreateDto), input }
        });
    }

    public virtual async Task<VendorDto> UpdateAsync(Guid id, VendorUpdateDto input)
    {
        return await RequestAsync<VendorDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(VendorUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(VendorExcelDownloadDto), input }
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
