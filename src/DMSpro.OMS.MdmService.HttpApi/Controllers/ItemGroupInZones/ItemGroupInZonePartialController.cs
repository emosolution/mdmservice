using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemGroupInZones;
using Volo.Abp.Content;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Controllers.ItemGroupInZones
{
    public partial class ItemGroupInZoneController
    {
        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _itemGroupInZonesAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("GetListDevextremesWithNavigations")]
        public Task<LoadResult> GetListDevextremesWithNavigationAsync(DataLoadOptionDevextreme inputDev)
        {
            return _itemGroupInZonesAppService.GetListDevextremesWithNavigationAsync(inputDev);
        }

        [HttpPost]
        [Route("update-from-excel")]
        public async Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
        {
            try
            {
                return await _itemGroupInZonesAppService.UpdateFromExcelAsync(file);
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
        public async Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
        {
            try
            {
                return await _itemGroupInZonesAppService.InsertFromExcelAsync(file);
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
        [Route("get-excel-template")]
        public virtual async Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
        {
            try
            {
                return await _itemGroupInZonesAppService.GenerateExcelTemplatesAsync();
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
    }
}