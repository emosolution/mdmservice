// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.HolidayDetails;
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
namespace DMSpro.OMS.MdmService.Controllers.HolidayDetails;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IHolidayDetailsAppService), typeof(HolidayDetailClientProxy))]
public partial class HolidayDetailClientProxy : ClientProxyBase<IHolidayDetailsAppService>, IHolidayDetailsAppService
{
    public virtual async Task<HolidayDetailDto> GetAsync(Guid id)
    {
        return await RequestAsync<HolidayDetailDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<HolidayDetailDto> CreateAsync(HolidayDetailCreateDto input)
    {
        return await RequestAsync<HolidayDetailDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(HolidayDetailCreateDto), input }
        });
    }

    public virtual async Task<HolidayDetailDto> UpdateAsync(Guid id, HolidayDetailUpdateDto input)
    {
        return await RequestAsync<HolidayDetailDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(HolidayDetailUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
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
