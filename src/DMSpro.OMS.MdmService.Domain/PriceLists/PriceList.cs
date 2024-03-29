using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public partial class PriceList : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Name { get; set; }

        public virtual bool Active { get; set; }

        public virtual ArithmeticOperator? ArithmeticOperation { get; set; }

        public virtual int? ArithmeticFactor { get; set; }

        public virtual ArithmeticFactorType? ArithmeticFactorType { get; set; }

        public virtual bool IsBase { get; set; }

        public virtual bool IsDefaultForCustomer { get; set; }

        public virtual bool IsReleased { get; set; }

        public virtual DateTime? ReleasedDate { get; set; }

        public virtual bool IsDefaultForVendor { get; set; }
        public Guid? BasePriceListId { get; set; }

        public PriceList()
        {

        }

        public PriceList(Guid id, Guid? basePriceListId, string code, string name, bool active, bool isBase, bool isDefaultForCustomer, bool isReleased, bool isDefaultForVendor, ArithmeticOperator? arithmeticOperation = null, int? arithmeticFactor = null, ArithmeticFactorType? arithmeticFactorType = null, DateTime? releasedDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), PriceListConsts.CodeMaxLength, PriceListConsts.CodeMinLength);
            Check.Length(name, nameof(name), PriceListConsts.NameMaxLength, 0);
            Code = code;
            Name = name;
            Active = active;
            IsBase = isBase;
            IsDefaultForCustomer = isDefaultForCustomer;
            IsReleased = isReleased;
            IsDefaultForVendor = isDefaultForVendor;
            ArithmeticOperation = arithmeticOperation;
            ArithmeticFactor = arithmeticFactor;
            ArithmeticFactorType = arithmeticFactorType;
            ReleasedDate = releasedDate;
            BasePriceListId = basePriceListId;
        }

    }
}