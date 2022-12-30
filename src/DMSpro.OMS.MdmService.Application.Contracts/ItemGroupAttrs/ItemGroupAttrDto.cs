using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public class ItemGroupAttrDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public bool Dummy { get; set; }
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

        public string ConcurrencyStamp { get; set; }
    }
}