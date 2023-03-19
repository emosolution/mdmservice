using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.SalesOrders
{
    public class GetInfoSODto
    {
        [Required]
        public Guid CompanyId { get; set; }
        [CanBeNull]
        public DateTime? PostingDate { get; set; }
        [CanBeNull]
        public string ObjectType { get; set; }
        [CanBeNull]
        public DateTime? LastUpdateDate { get; set; }
        [CanBeNull]
        public Guid? IdentityUserId { get; set; }
    }
}
