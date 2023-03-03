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

    public virtual async Task<EmployeeImageDto> CreateAsync(EmployeeImageCreateDto input, IRemoteStreamContent file)
    {
        return await RequestAsync<EmployeeImageDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(EmployeeImageCreateDto), input },
            { typeof(IRemoteStreamContent), file }
        });
    }

    public virtual async Task<EmployeeImageDto> UpdateAsync(Guid id, EmployeeImageUpdateDto input, IRemoteStreamContent file)
    {
        return await RequestAsync<EmployeeImageDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(EmployeeImageUpdateDto), input },
            { typeof(IRemoteStreamContent), file }
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

    public virtual async Task<EmployeeImageDto> CreateAvatarAsync(EmployeeImageCreateDto input, IRemoteStreamContent file)
    {
        return await RequestAsync<EmployeeImageDto>(nameof(CreateAvatarAsync), new ClientProxyRequestTypeValue
        {
            { typeof(EmployeeImageCreateDto), input },
            { typeof(IRemoteStreamContent), file }
        });
    }

    public virtual async Task<EmployeeImageDto> UpdateAvatarAsync(EmployeeImageUpdateDto input, IRemoteStreamContent file)
    {
        return await RequestAsync<EmployeeImageDto>(nameof(UpdateAvatarAsync), new ClientProxyRequestTypeValue
        {
            { typeof(EmployeeImageUpdateDto), input },
            { typeof(IRemoteStreamContent), file }
        });
    }

    public virtual async Task<EmployeeImageDto> TestCreateAvatarAsync(Guid id, IRemoteStreamContent file)
    {
        return await RequestAsync<EmployeeImageDto>(nameof(TestCreateAvatarAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(IRemoteStreamContent), file }
        });
    }

    public virtual async Task<EmployeeImageDto> TestCreateAvatarOnlyFileAsync(IRemoteStreamContent file)
    {
        return await RequestAsync<EmployeeImageDto>(nameof(TestCreateAvatarOnlyFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(IRemoteStreamContent), file }
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
