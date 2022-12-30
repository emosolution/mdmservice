using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public class RouteAssignmentCreateDto
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid RouteId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}