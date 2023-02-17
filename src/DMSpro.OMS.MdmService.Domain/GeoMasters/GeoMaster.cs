using DMSpro.OMS.MdmService.GeoMasters;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public partial class GeoMaster : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string ERPCode { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public virtual int Level { get; set; }
        public Guid? ParentId { get; set; }

        public GeoMaster()
        {

        }

        public GeoMaster(Guid id, Guid? parentId, string code, string erpCode, string name, int level)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), GeoMasterConsts.NameMaxLength, GeoMasterConsts.NameMinLength);
            Code = code;
            ERPCode = erpCode;
            Name = name;
            Level = level;
            ParentId = parentId;
        }

    }
}