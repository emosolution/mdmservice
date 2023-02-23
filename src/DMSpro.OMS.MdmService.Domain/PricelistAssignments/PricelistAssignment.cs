using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public partial class PricelistAssignment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid CustomerGroupId { get; set; }

        public virtual PriceList PriceList { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }

        public PricelistAssignment()
        {

        }

        public PricelistAssignment(Guid id, Guid priceListId, Guid customerGroupId, string description)
        {

            Id = id;
            Description = description;
            PriceListId = priceListId;
            CustomerGroupId = customerGroupId;
        }

    }
}