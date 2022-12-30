using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

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
            var queryable = await _geoMasterRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var geoMaster = await AsyncExecuter.FirstOrDefaultAsync(query);

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