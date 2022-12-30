using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerGroupByAtts;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.CustomerGroupByAtts
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerGroupByAtt")]
    [Route("api/mdm-service/customer-group-by-atts")]
    public class CustomerGroupByAttController : AbpController, ICustomerGroupByAttsAppService
    {
        private readonly ICustomerGroupByAttsAppService _customerGroupByAttsAppService;

        public CustomerGroupByAttController(ICustomerGroupByAttsAppService customerGroupByAttsAppService)
        {
            _customerGroupByAttsAppService = customerGroupByAttsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerGroupByAttWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByAttsInput input)
        {
            return _customerGroupByAttsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _customerGroupByAttsAppService.GetListDevextremesAsync(inputDev);
        }
        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerGroupByAttWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerGroupByAttsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerGroupByAttDto> GetAsync(Guid id)
        {
            return _customerGroupByAttsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
        {
            return _customerGroupByAttsAppService.GetCustomerGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("cus-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCusAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _customerGroupByAttsAppService.GetCusAttributeValueLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CustomerGroupByAttDto> CreateAsync(CustomerGroupByAttCreateDto input)
        {
            return _customerGroupByAttsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerGroupByAttDto> UpdateAsync(Guid id, CustomerGroupByAttUpdateDto input)
        {
            return _customerGroupByAttsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerGroupByAttsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByAttExcelDownloadDto input)
        {
            return _customerGroupByAttsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerGroupByAttsAppService.GetDownloadTokenAsync();
        }
    }
}