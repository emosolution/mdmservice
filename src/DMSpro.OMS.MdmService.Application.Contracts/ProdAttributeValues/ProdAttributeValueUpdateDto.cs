using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValueUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(ProdAttributeValueConsts.AttrValNameMaxLength, MinimumLength = ProdAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        public Guid ProdAttributeId { get; set; }
        public Guid? ParentProdAttributeValueId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}