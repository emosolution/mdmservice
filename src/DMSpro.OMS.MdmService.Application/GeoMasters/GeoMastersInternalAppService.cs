using DMSpro.OMS.MdmService.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public class GeoMastersInternalAppService : ApplicationService, IGeoMastersInternalAppService
    {
        private readonly IGeoMasterRepository _geoMasterRepository;

        public GeoMastersInternalAppService(
            IGeoMasterRepository geoMasterRepository)
        {
            _geoMasterRepository = geoMasterRepository;

            LocalizationResource = typeof(MdmServiceResource);
        }

        public IGeoMasterRepository GeoMasterRepository { get; }

        public virtual async Task<(
            GeoMasterDto, GeoMasterDto, GeoMasterDto, GeoMasterDto, GeoMasterDto
            )> CheckAllGeoInputs(Guid? geo0Id, Guid? geo1Id, Guid? geo2Id, Guid? geo3Id, Guid? geo4Id)
        {
            if (geo0Id == null)
            {
                return (null, null, null, null, null);
            }
            var geo0 = await CheckGeoInput(geo0Id, null);
            var geo1 = await CheckGeoInput(geo1Id, geo0);
            var geo2 = await CheckGeoInput(geo2Id, geo1);
            var geo3 = await CheckGeoInput(geo3Id, geo2);
            var geo4 = await CheckGeoInput(geo4Id, geo3);
            return (geo0, geo1, geo2, geo3, geo4);
        }

        private async Task<GeoMasterDto> CheckGeoInput(Guid? geoId, GeoMasterDto parentGeo)
        {
            if (geoId == null)
            {
                return null;
            }
            var geo = await _geoMasterRepository.GetAsync(x => x.Id == geoId);
            int geoLevel = 1;
            if (parentGeo != null)
            {
                geoLevel = parentGeo.Level + 1;
                if (geo.ParentId != parentGeo.Id)
                {
                    throw new UserFriendlyException(message: L["Error:GeoMastersInternalAppService:550", $"{geoLevel}"], code: "1");
                }
            }
            if (geo.Level != geoLevel)
            {
                throw new UserFriendlyException(message: L["Error:GeoMastersInternalAppService:551", $"{geoLevel}"], code: "1");
            }
            return ObjectMapper.Map<GeoMaster, GeoMasterDto>(geo);
        }
    }
}
