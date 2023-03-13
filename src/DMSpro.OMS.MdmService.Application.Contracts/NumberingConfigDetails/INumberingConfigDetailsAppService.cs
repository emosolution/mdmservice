using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial interface INumberingConfigDetailsAppService : IApplicationService
    {
        Task<NumberingConfigDetailDto> GetAsync(Guid id);

        Task<NumberingConfigDetailDto> CreateAsync(NumberingConfigDetailCreateDto input);

        Task<NumberingConfigDetailDto> UpdateAsync(Guid id, NumberingConfigDetailUpdateDto input);
    }
}