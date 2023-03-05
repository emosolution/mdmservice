using DMSpro.OMS.MdmService.ItemGroupInZones;
using DMSpro.OMS.MdmService.CustomerImages;
using Volo.Abp.EntityFrameworkCore.Modeling;
using DMSpro.OMS.MdmService.ItemGroupLists;
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SystemConfigs;
using DMSpro.OMS.MdmService.Vendors;
using DMSpro.OMS.MdmService.CustomerAttachments;
using DMSpro.OMS.MdmService.CustomerContacts;
using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;
using DMSpro.OMS.MdmService.PriceUpdateDetails;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesChannels;
using DMSpro.OMS.MdmService.VisitPlans;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.HolidayDetails;
using DMSpro.OMS.MdmService.Holidays;
using DMSpro.OMS.MdmService.CustomerAssignments;
using DMSpro.OMS.MdmService.CustomerGroupByGeos;
using DMSpro.OMS.MdmService.CustomerGroupByLists;
using DMSpro.OMS.MdmService.CustomerGroupByAtts;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.CustomerInZones;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.PricelistAssignments;
using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.WeightMeasurements;
using DMSpro.OMS.MdmService.DimensionMeasurements;
using DMSpro.OMS.MdmService.Currencies;
using DMSpro.OMS.MdmService.Streets;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.Companies;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.EntityFrameworkCore;

[ConnectionStringName(MdmServiceDbProperties.ConnectionStringName)]
public class MdmServiceDbContext : AbpDbContext<MdmServiceDbContext>
{
    public DbSet<ItemGroupInZone> ItemGroupInZones { get; set; }
    public DbSet<CustomerImage> CustomerImages { get; set; }
    public DbSet<ItemGroupList> ItemGroupLists { get; set; }
    public DbSet<ItemAttachment> ItemAttachments { get; set; }
    public DbSet<ItemImage> ItemImages { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemGroupAttribute> ItemGroupAttributes { get; set; }
    public DbSet<ItemAttributeValue> ItemAttributeValues { get; set; }
    public DbSet<ItemAttribute> ItemAttributes { get; set; }
    public DbSet<CompanyIdentityUserAssignment> CompanyIdentityUserAssignments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<SystemConfig> SystemConfigs { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<CustomerAttachment> CustomerAttachments { get; set; }
    public DbSet<CustomerContact> CustomerContacts { get; set; }
    public DbSet<CusAttributeValue> CusAttributeValues { get; set; }
    public DbSet<SalesOrgHierarchy> SalesOrgHierarchies { get; set; }
    public DbSet<SalesOrgHeader> SalesOrgHeaders { get; set; }
    public DbSet<EmployeeAttachment> EmployeeAttachments { get; set; }
    public DbSet<EmployeeImage> EmployeeImages { get; set; }
    public DbSet<PriceUpdateDetail> PriceUpdateDetails { get; set; }
    public DbSet<PriceList> PriceLists { get; set; }
    public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
    public DbSet<SalesChannel> SalesChannels { get; set; }
    public DbSet<VisitPlan> VisitPlans { get; set; }
    public DbSet<MCPDetail> MCPDetails { get; set; }
    public DbSet<MCPHeader> MCPHeaders { get; set; }
    public DbSet<HolidayDetail> HolidayDetails { get; set; }
    public DbSet<Holiday> Holidays { get; set; }
    public DbSet<CustomerAssignment> CustomerAssignments { get; set; }
    public DbSet<CustomerGroupByGeo> CustomerGroupByGeos { get; set; }
    public DbSet<CustomerGroupByList> CustomerGroupByLists { get; set; }
    public DbSet<CustomerGroupByAtt> CustomerGroupByAtts { get; set; }
    public DbSet<CustomerGroup> CustomerGroups { get; set; }
    public DbSet<CustomerAttribute> CustomerAttributes { get; set; }
    public DbSet<CustomerInZone> CustomerInZones { get; set; }
    public DbSet<CompanyInZone> CompanyInZones { get; set; }
    public DbSet<SalesOrgEmpAssignment> SalesOrgEmpAssignments { get; set; }
    public DbSet<WorkingPosition> WorkingPositions { get; set; }
    public DbSet<NumberingConfig> NumberingConfigs { get; set; }
    public DbSet<SystemData> SystemDatas { get; set; }
    public DbSet<PricelistAssignment> PricelistAssignments { get; set; }
    public DbSet<PriceUpdate> PriceUpdates { get; set; }
    public DbSet<PriceListDetail> PriceListDetails { get; set; }
    public DbSet<ItemGroup> ItemGroups { get; set; }
    public DbSet<UOMGroupDetail> UOMGroupDetails { get; set; }
    public DbSet<UOMGroup> UOMGroups { get; set; }
    public DbSet<UOM> UOMs { get; set; }
    public DbSet<VAT> VATs { get; set; }
    public DbSet<WeightMeasurement> WeightMeasurements { get; set; }
    public DbSet<DimensionMeasurement> DimensionMeasurements { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Street> Streets { get; set; }
    public DbSet<GeoMaster> GeoMasters { get; set; }
    public DbSet<Company> Companies { get; set; }

    public MdmServiceDbContext(DbContextOptions<MdmServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureMdmService();
    }
}