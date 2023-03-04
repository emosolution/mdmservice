using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.CustomerImages;

namespace DMSpro.OMS.MdmService.Controllers.CustomerImages
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerImage")]
    [Route("api/mdm-service/customer-images")]
    public partial class CustomerImageController : AbpController, ICustomerImagesAppService
    {
        private readonly ICustomerImagesAppService _customerImagesAppService;

        public CustomerImageController(ICustomerImagesAppService customerImagesAppService)
        {
            _customerImagesAppService = customerImagesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerImageWithNavigationPropertiesDto>> GetListAsync(GetCustomerImagesInput input)
        {
            return _customerImagesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerImagesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerImageDto> GetAsync(Guid id)
        {
            return _customerImagesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _customerImagesAppService.GetCustomerLookupAsync(input);
        }

        [HttpGet]
        [Route("item-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input)
        {
            return _customerImagesAppService.GetItemLookupAsync(input);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerImageExcelDownloadDto input)
        {
            return _customerImagesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerImagesAppService.GetDownloadTokenAsync();
        }
    }
}