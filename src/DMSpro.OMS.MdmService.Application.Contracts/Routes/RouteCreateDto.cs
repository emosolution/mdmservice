using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Routes
{
    public class RouteCreateDto
    {
        public bool CheckIn { get; set; }
        public bool CheckOut { get; set; }
        public bool GPSLock { get; set; }
        public bool OutRoute { get; set; }
        public Guid RouteTypeId { get; set; }
        public Guid ItemGroupId { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
    }
}