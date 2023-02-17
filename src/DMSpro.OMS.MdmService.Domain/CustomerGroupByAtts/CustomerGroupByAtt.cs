using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CusAttributeValues;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public partial class CustomerGroupByAtt : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string ValueCode { get; set; }

        [CanBeNull]
        public virtual string ValueName { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CusAttributeValueId { get; set; }

        public CustomerGroupByAtt()
        {

        }

        public CustomerGroupByAtt(Guid id, Guid customerGroupId, Guid cusAttributeValueId, string valueCode, string valueName)
        {

            Id = id;
            ValueCode = valueCode;
            ValueName = valueName;
            CustomerGroupId = customerGroupId;
            CusAttributeValueId = cusAttributeValueId;
        }

    }
}