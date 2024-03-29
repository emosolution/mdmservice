﻿using System;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.Customers;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.CustomerAssignments
{
	public class CustomerAssignmentWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CompanyDto Company { get; set; }
        public CustomerDto Customer { get; set; }


        public CustomerAssignmentWithDetailsDto()
		{
		}
	}
}

