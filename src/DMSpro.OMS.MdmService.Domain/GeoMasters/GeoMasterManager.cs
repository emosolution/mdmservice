using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public class GeoMasterManager : DomainService
    {
        private readonly IGeoMasterRepository _geoMasterRepository;

        public GeoMasterManager(IGeoMasterRepository geoMasterRepository)
        {
            _geoMasterRepository = geoMasterRepository;
        }

        public async Task<GeoMaster> CreateAsync(
        Guid? parentId, string code, string erpCode, string name, int level)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(erpCode, nameof(erpCode), GeoMasterConsts.ERPCodeMaxLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), GeoMasterConsts.NameMaxLength, GeoMasterConsts.NameMinLength);

            var geoMaster = new GeoMaster(
             GuidGenerator.Create(),
             parentId, code, erpCode, name, level
             );

            return await _geoMasterRepository.InsertAsync(geoMaster);
        }

        public async Task<GeoMaster> UpdateAsync(
            Guid id,
            Guid? parentId, string code, string erpCode, string name, int level, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(erpCode, nameof(erpCode), GeoMasterConsts.ERPCodeMaxLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), GeoMasterConsts.NameMaxLength, GeoMasterConsts.NameMinLength);

            var geoMaster = await _geoMasterRepository.GetAsync(id);

            geoMaster.ParentId = parentId;
            geoMaster.Code = code;
            geoMaster.ERPCode = erpCode;
            geoMaster.Name = name;
            geoMaster.Level = level;

            geoMaster.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _geoMasterRepository.UpdateAsync(geoMaster);
        }

    }
}