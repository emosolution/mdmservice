using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class GetVendorsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string ERPCode { get; set; }
        public bool? Active { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public string LinkedCompany { get; set; }
        public Guid? WarehouseId { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Guid? PriceListId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }
        public Guid? CompanyId { get; set; }

        public GetVendorsInput()
        {

        }
    }
}