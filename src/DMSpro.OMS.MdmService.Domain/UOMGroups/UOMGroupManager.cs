using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    public class UOMGroupManager : DomainService
    {
        private readonly IUOMGroupRepository _uOMGroupRepository;

        public UOMGroupManager(IUOMGroupRepository uOMGroupRepository)
        {
            _uOMGroupRepository = uOMGroupRepository;
        }

        public async Task<UOMGroup> CreateAsync(
        string code, string name)
        {
            var uOMGroup = new UOMGroup(
             GuidGenerator.Create(),
             code, name
             );

            return await _uOMGroupRepository.InsertAsync(uOMGroup);
        }

        public async Task<UOMGroup> UpdateAsync(
            Guid id,
            string code, string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _uOMGroupRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var uOMGroup = await AsyncExecuter.FirstOrDefaultAsync(query);

            uOMGroup.Code = code;
            uOMGroup.Name = name;

            uOMGroup.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _uOMGroupRepository.UpdateAsync(uOMGroup);
        }

    }
}