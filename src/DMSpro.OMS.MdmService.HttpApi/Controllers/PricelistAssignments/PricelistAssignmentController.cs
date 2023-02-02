using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.PricelistAssignments;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.PricelistAssignments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("PricelistAssignment")]
    [Route("api/mdm-service/pricelist-assignments")]
    public partial class PricelistAssignmentController : AbpController, IPricelistAssignmentsAppService
    {
        private readonly IPricelistAssignmentsAppService _pricelistAssignmentsAppService;

        public PricelistAssignmentController(IPricelistAssignmentsAppService pricelistAssignmentsAppService)
        {
            _pricelistAssignmentsAppService = pricelistAssignmentsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<PricelistAssignmentWithNavigationPropertiesDto>> GetListAsync(GetPricelistAssignmentsInput input)
        {
            return _pricelistAssignmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<PricelistAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _pricelistAssignmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PricelistAssignmentDto> GetAsync(Guid id)
        {
            return _pricelistAssignmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("price-list-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            return _pricelistAssignmentsAppService.GetPriceListLookupAsync(input);
        }

        [HttpGet]
        [Route("customer-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
        {
            return _pricelistAssignmentsAppService.GetCustomerGroupLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<PricelistAssignmentDto> CreateAsync(PricelistAssignmentCreateDto input)
        {
            return _pricelistAssignmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PricelistAssignmentDto> UpdateAsync(Guid id, PricelistAssignmentUpdateDto input)
        {
            return _pricelistAssignmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _pricelistAssignmentsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PricelistAssignmentExcelDownloadDto input)
        {
            return _pricelistAssignmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _pricelistAssignmentsAppService.GetDownloadTokenAsync();
        }
    }
}