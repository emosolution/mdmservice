using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SalesChannels
{
    public class SalesChannelManager : DomainService
    {
        private readonly ISalesChannelRepository _salesChannelRepository;

        public SalesChannelManager(ISalesChannelRepository salesChannelRepository)
        {
            _salesChannelRepository = salesChannelRepository;
        }

        public async Task<SalesChannel> CreateAsync(
        string code, string name, string description, bool active)
        {
            var salesChannel = new SalesChannel(
             GuidGenerator.Create(),
             code, name, description, active
             );

            return await _salesChannelRepository.InsertAsync(salesChannel);
        }

        public async Task<SalesChannel> UpdateAsync(
            Guid id,
            string code, string name, string description, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _salesChannelRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var salesChannel = await AsyncExecuter.FirstOrDefaultAsync(query);

            salesChannel.Code = code;
            salesChannel.Name = name;
            salesChannel.Description = description;
            salesChannel.Active = active;

            salesChannel.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _salesChannelRepository.UpdateAsync(salesChannel);
        }

    }
}