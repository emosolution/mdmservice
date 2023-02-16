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

        public virtual bool IsFirstPriceList { get; set; }
        public Guid? BasePriceListId { get; set; }

        public PriceList()
        {

        }

        public PriceList(Guid id, Guid? basePriceListId, string code, string name, bool active, bool isFirstPriceList, ArithmeticOperator? arithmeticOperation = null, int? arithmeticFactor = null, ArithmeticFactorType? arithmeticFactorType = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), PriceListConsts.CodeMaxLength, PriceListConsts.CodeMinLength);
            Code = code;
            Name = name;
            Active = active;
            IsFirstPriceList = isFirstPriceList;
            ArithmeticOperation = arithmeticOperation;
            ArithmeticFactor = arithmeticFactor;
            ArithmeticFactorType = arithmeticFactorType;
            BasePriceListId = basePriceListId;
        }

    }
}