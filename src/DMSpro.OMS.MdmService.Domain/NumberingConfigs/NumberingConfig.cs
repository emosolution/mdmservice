using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public partial class NumberingConfig : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int StartNumber { get; set; }

        [CanBeNull]
        public virtual string Prefix { get; set; }

        [CanBeNull]
        public virtual string Suffix { get; set; }

        public virtual int Length { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? SystemDataId { get; set; }

        public virtual Company Company { get; set; }
        public virtual SystemData SystemData { get; set; }
        public NumberingConfig()
        {

        }

        public NumberingConfig(Guid id, Guid? companyId, Guid? systemDataId, int startNumber, string prefix, string suffix, int length)
        {

            Id = id;
            StartNumber = startNumber;
            Prefix = prefix;
            Suffix = suffix;
            Length = length;
            CompanyId = companyId;
            SystemDataId = systemDataId;
        }

    }
}