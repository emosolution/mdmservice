using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class CustomerGroupAttribute : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid? Attr0Id { get; set; }
        public Guid? Attr1Id { get; set; }
        public Guid? Attr2Id { get; set; }
        public Guid? Attr3Id { get; set; }
        public Guid? Attr4Id { get; set; }
        public Guid? Attr5Id { get; set; }
        public Guid? Attr6Id { get; set; }
        public Guid? Attr7Id { get; set; }
        public Guid? Attr8Id { get; set; }
        public Guid? Attr9Id { get; set; }
        public Guid? Attr10Id { get; set; }
        public Guid? Attr11Id { get; set; }
        public Guid? Attr12Id { get; set; }
        public Guid? Attr13Id { get; set; }
        public Guid? Attr14Id { get; set; }
        public Guid? Attr15Id { get; set; }
        public Guid? Attr16Id { get; set; }
        public Guid? Attr17Id { get; set; }
        public Guid? Attr18Id { get; set; }
        public Guid? Attr19Id { get; set; }

        public CustomerGroupAttribute()
        {

        }

        public CustomerGroupAttribute(Guid id, Guid customerGroupId, Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, string description)
        {

            Id = id;
            Check.Length(description, nameof(description), CustomerGroupAttributeConsts.DescriptionMaxLength, 0);
            Description = description;
            CustomerGroupId = customerGroupId;
            Attr0Id = attr0Id;
            Attr1Id = attr1Id;
            Attr2Id = attr2Id;
            Attr3Id = attr3Id;
            Attr4Id = attr4Id;
            Attr5Id = attr5Id;
            Attr6Id = attr6Id;
            Attr7Id = attr7Id;
            Attr8Id = attr8Id;
            Attr9Id = attr9Id;
            Attr10Id = attr10Id;
            Attr11Id = attr11Id;
            Attr12Id = attr12Id;
            Attr13Id = attr13Id;
            Attr14Id = attr14Id;
            Attr15Id = attr15Id;
            Attr16Id = attr16Id;
            Attr17Id = attr17Id;
            Attr18Id = attr18Id;
            Attr19Id = attr19Id;
        }

    }
}