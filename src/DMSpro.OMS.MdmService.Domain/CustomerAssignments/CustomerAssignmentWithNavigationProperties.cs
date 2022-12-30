using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.Customers;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
    public class CustomerAssignmentWithNavigationProperties
    {
        public CustomerAssignment CustomerAssignment { get; set; }

        public Company Company { get; set; }
        public Customer Customer { get; set; }
        

        
    }
}