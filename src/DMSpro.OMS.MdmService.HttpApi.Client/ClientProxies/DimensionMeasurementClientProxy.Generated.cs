// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using DMSpro.OMS.MdmService.DimensionMeasurements;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

// ReSharper disable once CheckNamespace
namespace DMSpro.OMS.MdmService.Controllers.DimensionMeasurements.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IDimensionMeasurementsAppService), typeof(DimensionMeasurementClientProxy))]
public partial class DimensionMeasurementClientProxy : ClientProxyBase<IDimensionMeasurementsAppService>, IDimensionMeasurementsAppService
{
    public virtual async Task<PagedResultDto<DimensionMeasurementDto>> GetListAsync(GetDimensionMeasurementsInput input)
    {
        return await RequestAsync<PagedResultDto<DimensionMeasurementDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetDimensionMeasurementsInput), input }
        });
    }

    public virtual async Task<DimensionMeasurementDto> GetAsync(Guid id)
    {
        return await RequestAsync<DimensionMeasurementDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<DimensionMeasurementDto> CreateAsync(DimensionMeasurementCreateDto input)
    {
        return await RequestAsync<DimensionMeasurementDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DimensionMeasurementCreateDto), input }
        });
    }

    public virtual async Task<DimensionMeasurementDto> UpdateAsync(Guid id, DimensionMeasurementUpdateDto input)
    {
        return await RequestAsync<DimensionMeasurementDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(DimensionMeasurementUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DimensionMeasurementExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DimensionMeasurementExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}
