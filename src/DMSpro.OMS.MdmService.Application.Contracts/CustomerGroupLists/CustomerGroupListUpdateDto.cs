using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListUpdateDto : IHasConcurrencyStamp
    {
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}