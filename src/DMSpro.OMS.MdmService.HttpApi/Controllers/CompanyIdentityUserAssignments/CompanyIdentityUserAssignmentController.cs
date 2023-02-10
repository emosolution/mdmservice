using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using Volo.Abp.Content;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DMSpro.OMS.MdmService.Controllers.Partial;

namespace DMSpro.OMS.MdmService.Controllers.CompanyIdentityUserAssignments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CompanyIdentityUserAssignment")]
    [Route("api/mdm-service/company-identity-user-assignments")]
    public partial class CompanyIdentityUserAssignmentController :  
        ICompanyIdentityUserAssignmentsAppService
    {
        private readonly ICompanyIdentityUserAssignmentsAppService _companyIdentityUserAssignmentsAppService;

        public CompanyIdentityUserAssignmentController(ICompanyIdentityUserAssignmentsAppService appService)
        {
            _companyIdentityUserAssignmentsAppService = appService;
        }



        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _companyIdentityUserAssignmentsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpPost]
        [Route("update-from-excel")]
        public virtual async Task<int> UpdateFromExcelAsync(IFormFile file)
        {
            try
            {
                return await _companyIdentityUserAssignmentsAppService.UpdateFromExcelAsync(file);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }

        [HttpPost]
        [Route("insert-from-excel")]
        public virtual async Task<int> InsertFromExcelAsync(IFormFile file)
        {
            try
            {
                return await _companyIdentityUserAssignmentsAppService.InsertFromExcelAsync(file);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }

        [HttpGet]
        public Task<PagedResultDto<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>> GetListAsync(GetCompanyIdentityUserAssignmentsInput input)
        {
            return _companyIdentityUserAssignmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CompanyIdentityUserAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _companyIdentityUserAssignmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CompanyIdentityUserAssignmentDto> GetAsync(Guid id)
        {
            return _companyIdentityUserAssignmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _companyIdentityUserAssignmentsAppService.GetCompanyLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CompanyIdentityUserAssignmentDto> CreateAsync(CompanyIdentityUserAssignmentCreateDto input)
        {
            return _companyIdentityUserAssignmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CompanyIdentityUserAssignmentDto> UpdateAsync(Guid id, CompanyIdentityUserAssignmentUpdateDto input)
        {
            return _companyIdentityUserAssignmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _companyIdentityUserAssignmentsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyIdentityUserAssignmentExcelDownloadDto input)
        {
            return _companyIdentityUserAssignmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _companyIdentityUserAssignmentsAppService.GetDownloadTokenAsync();
        }
    }
}