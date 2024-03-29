// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.PricelistAssignments;
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
namespace DMSpro.OMS.MdmService.Controllers.PricelistAssignments;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IPricelistAssignmentsAppService), typeof(PricelistAssignmentClientProxy))]
public partial class PricelistAssignmentClientProxy : ClientProxyBase<IPricelistAssignmentsAppService>, IPricelistAssignmentsAppService
{
    public virtual async Task<PricelistAssignmentDto> GetAsync(Guid id)
    {
        return await RequestAsync<PricelistAssignmentDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PricelistAssignmentDto> CreateAsync(PricelistAssignmentCreateDto input)
    {
        return await RequestAsync<PricelistAssignmentDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(PricelistAssignmentCreateDto), input }
        });
    }

    public virtual async Task<PricelistAssignmentDto> UpdateAsync(Guid id, PricelistAssignmentUpdateDto input)
    {
        return await RequestAsync<PricelistAssignmentDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(PricelistAssignmentUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<PricelistAssignmentDto> ReleaseAsync(Guid id)
    {
        return await RequestAsync<PricelistAssignmentDto>(nameof(ReleaseAsync), new ClientProxyRequestTypeValue
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
