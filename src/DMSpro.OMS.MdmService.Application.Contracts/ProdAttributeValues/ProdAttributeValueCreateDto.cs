using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValueCreateDto
    {
        [Required]
        [StringLength(ProdAttributeValueConsts.AttrValNameMaxLength, MinimumLength = ProdAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        public Guid ProdAttributeId { get; set; }
        public Guid? ParentProdAttributeValueId { get; set; }
    }
}