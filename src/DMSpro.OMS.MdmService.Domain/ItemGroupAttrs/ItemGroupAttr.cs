using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.ProdAttributeValues;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public class ItemGroupAttr : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool Dummy { get; set; }
        public Guid ItemGroupId { get; set; }
        public Guid? Attr0 { get; set; }
        public Guid? Attr1 { get; set; }
        public Guid? Attr2 { get; set; }
        public Guid? Attr3 { get; set; }
        public Guid? Attr4 { get; set; }
        public Guid? Attr5 { get; set; }
        public Guid? Attr6 { get; set; }
        public Guid? Attr7 { get; set; }
        public Guid? Attr8 { get; set; }
        public Guid? Attr9 { get; set; }
        public Guid? Attr10 { get; set; }
        public Guid? Attr11 { get; set; }
        public Guid? Attr12 { get; set; }
        public Guid? Attr13 { get; set; }
        public Guid? Attr14 { get; set; }
        public Guid? Attr15 { get; set; }
        public Guid? Attr16 { get; set; }
        public Guid? Attr17 { get; set; }
        public Guid? Attr18 { get; set; }
        public Guid? Attr19 { get; set; }

        public ItemGroupAttr()
        {

        }

        public ItemGroupAttr(Guid id, Guid itemGroupId, Guid? attr0, Guid? attr1, Guid? attr2, Guid? attr3, Guid? attr4, Guid? attr5, Guid? attr6, Guid? attr7, Guid? attr8, Guid? attr9, Guid? attr10, Guid? attr11, Guid? attr12, Guid? attr13, Guid? attr14, Guid? attr15, Guid? attr16, Guid? attr17, Guid? attr18, Guid? attr19, bool dummy)
        {

            Id = id;
            Dummy = dummy;
            ItemGroupId = itemGroupId;
            Attr0 = attr0;
            Attr1 = attr1;
            Attr2 = attr2;
            Attr3 = attr3;
            Attr4 = attr4;
            Attr5 = attr5;
            Attr6 = attr6;
            Attr7 = attr7;
            Attr8 = attr8;
            Attr9 = attr9;
            Attr10 = attr10;
            Attr11 = attr11;
            Attr12 = attr12;
            Attr13 = attr13;
            Attr14 = attr14;
            Attr15 = attr15;
            Attr16 = attr16;
            Attr17 = attr17;
            Attr18 = attr18;
            Attr19 = attr19;
        }

    }
}