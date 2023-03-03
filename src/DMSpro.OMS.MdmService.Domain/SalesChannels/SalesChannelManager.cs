using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
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
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SalesChannelConsts.CodeMaxLength, SalesChannelConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), SalesChannelConsts.NameMaxLength, SalesChannelConsts.NameMinLength);
            Check.Length(description, nameof(description), SalesChannelConsts.DescriptionMaxLength);

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
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SalesChannelConsts.CodeMaxLength, SalesChannelConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), SalesChannelConsts.NameMaxLength, SalesChannelConsts.NameMinLength);
            Check.Length(description, nameof(description), SalesChannelConsts.DescriptionMaxLength);

            var salesChannel = await _salesChannelRepository.GetAsync(id);

            salesChannel.Code = code;
            salesChannel.Name = name;
            salesChannel.Description = description;
            salesChannel.Active = active;

            salesChannel.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _salesChannelRepository.UpdateAsync(salesChannel);
        }

    }
}