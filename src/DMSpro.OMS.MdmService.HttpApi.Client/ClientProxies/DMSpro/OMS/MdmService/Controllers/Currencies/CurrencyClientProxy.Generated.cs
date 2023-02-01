// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using DMSpro.OMS.MdmService.Currencies;
using DMSpro.OMS.MdmService.Shared;
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
namespace DMSpro.OMS.MdmService.Controllers.Currencies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ICurrenciesAppService), typeof(CurrencyClientProxy))]
public partial class CurrencyClientProxy : ClientProxyBase<ICurrenciesAppService>, ICurrenciesAppService
{
    public virtual async Task<PagedResultDto<CurrencyDto>> GetListAsync(GetCurrenciesInput input)
    {
        return await RequestAsync<PagedResultDto<CurrencyDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetCurrenciesInput), input }
        });
    }

    public virtual async Task<CurrencyDto> GetAsync(Guid id)
    {
        return await RequestAsync<CurrencyDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<CurrencyDto> CreateAsync(CurrencyCreateDto input)
    {
        return await RequestAsync<CurrencyDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CurrencyCreateDto), input }
        });
    }

    public virtual async Task<CurrencyDto> UpdateAsync(Guid id, CurrencyUpdateDto input)
    {
        return await RequestAsync<CurrencyDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(CurrencyUpdateDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CurrencyExcelDownloadDto input)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetListAsExcelFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CurrencyExcelDownloadDto), input }
        });
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return await RequestAsync<DownloadTokenResultDto>(nameof(GetDownloadTokenAsync));
    }
}
