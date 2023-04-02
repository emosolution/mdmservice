// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.VisitPlans;
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
namespace DMSpro.OMS.MdmService.Controllers.VisitPlans;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IVisitPlansAppService), typeof(VisitPlanClientProxy))]
public partial class VisitPlanClientProxy : ClientProxyBase<IVisitPlansAppService>, IVisitPlansAppService
{
    public virtual async Task<VisitPlanDto> GetAsync(Guid id)
    {
        return await RequestAsync<VisitPlanDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
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

    public virtual async Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
    {
        return await RequestAsync<List<VisitPlanDto>>(nameof(GenerateWithPermissionAsync), new ClientProxyRequestTypeValue
        {
            { typeof(VisitPlanGenerationInputDto), input }
        });
    }

    public virtual async Task<List<VisitPlanDto>> UpdateMultipleAsync(List<Guid> ids, DateTime newDate)
    {
        return await RequestAsync<List<VisitPlanDto>>(nameof(UpdateMultipleAsync), new ClientProxyRequestTypeValue
        {
            { typeof(List<Guid>), ids },
            { typeof(DateTime), newDate }
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
