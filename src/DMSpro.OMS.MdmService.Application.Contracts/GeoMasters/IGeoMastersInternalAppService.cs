using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using static DMSpro.OMS.MdmService.Permissions.MdmServicePermissions;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public interface IGeoMastersInternalAppService : IApplicationService
    {
        Task<(
            GeoMasterDto, GeoMasterDto, GeoMasterDto, GeoMasterDto, GeoMasterDto
            )> CheckAllGeoInputs(Guid? geo0Id, Guid? geo1Id, Guid? geo2Id, Guid? geo3Id, Guid? geo4Id);
    }
}
