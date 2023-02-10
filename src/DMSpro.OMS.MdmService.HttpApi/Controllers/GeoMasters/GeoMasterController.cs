using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.GeoMasters;
using Volo.Abp.Content;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using DMSpro.OMS.MdmService.Controllers.Partial;

namespace DMSpro.OMS.MdmService.Controllers.GeoMasters
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("GeoMaster")]
    [Route("api/mdm-service/geo-masters")]
    public partial class GeoMasterController :  IGeoMastersAppService
    {
        private readonly IGeoMastersAppService _geoMastersAppService;

        public GeoMasterController(IGeoMastersAppService appService)
        {
            _geoMastersAppService = appService;
        }


        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _geoMastersAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpPost]
        [Route("update-from-excel")]
        public virtual async Task<int> UpdateFromExcelAsync(IFormFile file)
        {
            try
            {
                return await _geoMastersAppService.UpdateFromExcelAsync(file);
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
                return await _geoMastersAppService.InsertFromExcelAsync(file);
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
        public Task<PagedResultDto<GeoMasterWithNavigationPropertiesDto>> GetListAsync(GetGeoMastersInput input)
        {
            return _geoMastersAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<GeoMasterWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _geoMastersAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<GeoMasterDto> GetAsync(Guid id)
        {
            return _geoMastersAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("geo-master-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            return _geoMastersAppService.GetGeoMasterLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<GeoMasterDto> CreateAsync(GeoMasterCreateDto input)
        {
            return _geoMastersAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<GeoMasterDto> UpdateAsync(Guid id, GeoMasterUpdateDto input)
        {
            return _geoMastersAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _geoMastersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(GeoMasterExcelDownloadDto input)
        {
            return _geoMastersAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _geoMastersAppService.GetDownloadTokenAsync();
        }
    }
}