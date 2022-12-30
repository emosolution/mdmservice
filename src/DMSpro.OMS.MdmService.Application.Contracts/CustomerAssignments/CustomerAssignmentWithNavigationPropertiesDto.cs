using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.Customers;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
    public class CustomerAssignmentWithNavigationPropertiesDto
    {
        public CustomerAssignmentDto CustomerAssignment { get; set; }

        public CompanyDto Company { get; set; }
        public CustomerDto Customer { get; set; }

    }
}