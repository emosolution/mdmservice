﻿using System;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.MCPHeaders;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.MCPDetails
{
	public class MCPDetailWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Distance { get; set; }
        public int VisitOrder { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool Week1 { get; set; }
        public bool Week2 { get; set; }
        public bool Week3 { get; set; }
        public bool Week4 { get; set; }
        public Guid CustomerId { get; set; }
        public Guid MCPHeaderId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CustomerWithDetailsDto Customer { get; set; }
        public MCPHeaderDto MCPHeader { get; set; }
        
        public MCPDetailWithDetailsDto()
		{
		}
	}
}

