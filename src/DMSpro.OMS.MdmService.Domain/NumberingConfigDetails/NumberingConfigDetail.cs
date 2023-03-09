using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.Companies;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial class NumberingConfigDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool Active { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }
        public Guid NumberingConfigId { get; set; }
        public Guid CompanyId { get; set; }

        public virtual Company Company {get;set;}
        public virtual NumberingConfig NumberingConfig {get;set;}

        public NumberingConfigDetail()
        {

        }

        public NumberingConfigDetail(Guid id, Guid numberingConfigId, Guid companyId, bool active, string description)
        {

            Id = id;
            Check.Length(description, nameof(description), NumberingConfigDetailConsts.DescriptionMaxLength, 0);
            Active = active;
            Description = description;
            NumberingConfigId = numberingConfigId;
            CompanyId = companyId;
        }

    }
}