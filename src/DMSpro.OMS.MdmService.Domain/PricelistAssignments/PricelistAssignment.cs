using System;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public partial class PricelistAssignment 
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual DateTime? ReleasedDate { get; set; }

        public virtual bool IsReleased { get; set; }
        
        public Guid PriceListId { get; set; }
        public Guid CustomerGroupId { get; set; }

        public PricelistAssignment()
        {

        }

        public PricelistAssignment(Guid id, Guid priceListId, Guid customerGroupId, string description)
        {

            Id = id;
            Check.Length(description, nameof(description), PricelistAssignmentConsts.DescriptionMaxLength, 0);
            Description = description;
            IsReleased = false;
            ReleasedDate = null;
            PriceListId = priceListId;
            CustomerGroupId = customerGroupId;
        }

    }
}