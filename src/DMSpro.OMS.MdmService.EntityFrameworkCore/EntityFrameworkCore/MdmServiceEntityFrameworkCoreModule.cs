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
using DMSpro.OMS.MdmService.RouteAssignments;
using DMSpro.OMS.MdmService.VisitPlans;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.Routes;
using DMSpro.OMS.MdmService.HolidayDetails;
using DMSpro.OMS.MdmService.Holidays;
using DMSpro.OMS.MdmService.CustomerAssignments;
using DMSpro.OMS.MdmService.CustomerGroupByGeos;
using DMSpro.OMS.MdmService.CustomerGroupByLists;
using DMSpro.OMS.MdmService.CustomerGroupByAtts;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.EmployeeInZones;
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
using DMSpro.OMS.MdmService.ItemGroupAttrs;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.ProdAttributeValues;
using DMSpro.OMS.MdmService.ProductAttributes;
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
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace DMSpro.OMS.MdmService.EntityFrameworkCore;

[DependsOn(
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(MdmServiceDomainModule)
)]
public class MdmServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MdmServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MdmServiceDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<Company, Companies.EfCoreCompanyRepository>();

            options.AddRepository<GeoMaster, GeoMasters.EfCoreGeoMasterRepository>();

            options.AddRepository<Street, Streets.EfCoreStreetRepository>();

            options.AddRepository<Currency, Currencies.EfCoreCurrencyRepository>();

            options.AddRepository<DimensionMeasurement, DimensionMeasurements.EfCoreDimensionMeasurementRepository>();

            options.AddRepository<WeightMeasurement, WeightMeasurements.EfCoreWeightMeasurementRepository>();

            options.AddRepository<VAT, VATs.EfCoreVATRepository>();

            options.AddRepository<UOM, UOMs.EfCoreUOMRepository>();

            options.AddRepository<UOMGroup, UOMGroups.EfCoreUOMGroupRepository>();

            options.AddRepository<UOMGroupDetail, UOMGroupDetails.EfCoreUOMGroupDetailRepository>();

            options.AddRepository<ProductAttribute, ProductAttributes.EfCoreProductAttributeRepository>();

            options.AddRepository<ProdAttributeValue, ProdAttributeValues.EfCoreProdAttributeValueRepository>();

            options.AddRepository<ItemGroup, ItemGroups.EfCoreItemGroupRepository>();

            options.AddRepository<ItemGroupAttr, ItemGroupAttrs.EfCoreItemGroupAttrRepository>();

            options.AddRepository<PriceListDetail, PriceListDetails.EfCorePriceListDetailRepository>();

            options.AddRepository<PriceUpdate, PriceUpdates.EfCorePriceUpdateRepository>();

            options.AddRepository<PricelistAssignment, PricelistAssignments.EfCorePricelistAssignmentRepository>();

            options.AddRepository<SystemData, SystemDatas.EfCoreSystemDataRepository>();

            options.AddRepository<NumberingConfig, NumberingConfigs.EfCoreNumberingConfigRepository>();

            options.AddRepository<WorkingPosition, WorkingPositions.EfCoreWorkingPositionRepository>();

            options.AddRepository<SalesOrgEmpAssignment, SalesOrgEmpAssignments.EfCoreSalesOrgEmpAssignmentRepository>();

            options.AddRepository<CompanyInZone, CompanyInZones.EfCoreCompanyInZoneRepository>();

            options.AddRepository<CustomerInZone, CustomerInZones.EfCoreCustomerInZoneRepository>();

            options.AddRepository<EmployeeInZone, EmployeeInZones.EfCoreEmployeeInZoneRepository>();

            options.AddRepository<CustomerAttribute, CustomerAttributes.EfCoreCustomerAttributeRepository>();

            options.AddRepository<CustomerGroup, CustomerGroups.EfCoreCustomerGroupRepository>();

            options.AddRepository<CustomerGroupByAtt, CustomerGroupByAtts.EfCoreCustomerGroupByAttRepository>();

            options.AddRepository<CustomerGroupByList, CustomerGroupByLists.EfCoreCustomerGroupByListRepository>();

            options.AddRepository<CustomerGroupByGeo, CustomerGroupByGeos.EfCoreCustomerGroupByGeoRepository>();

            options.AddRepository<CustomerAssignment, CustomerAssignments.EfCoreCustomerAssignmentRepository>();

            options.AddRepository<Holiday, Holidays.EfCoreHolidayRepository>();

            options.AddRepository<HolidayDetail, HolidayDetails.EfCoreHolidayDetailRepository>();

            options.AddRepository<Route, Routes.EfCoreRouteRepository>();

            options.AddRepository<MCPHeader, MCPHeaders.EfCoreMCPHeaderRepository>();

            options.AddRepository<MCPDetail, MCPDetails.EfCoreMCPDetailRepository>();

            options.AddRepository<VisitPlan, VisitPlans.EfCoreVisitPlanRepository>();

            options.AddRepository<RouteAssignment, RouteAssignments.EfCoreRouteAssignmentRepository>();

            options.AddRepository<SalesChannel, SalesChannels.EfCoreSalesChannelRepository>();

            options.AddRepository<EmployeeProfile, EmployeeProfiles.EfCoreEmployeeProfileRepository>();

            options.AddRepository<PriceList, PriceLists.EfCorePriceListRepository>();

            options.AddRepository<PriceUpdateDetail, PriceUpdateDetails.EfCorePriceUpdateDetailRepository>();

            options.AddRepository<EmployeeImage, EmployeeImages.EfCoreEmployeeImageRepository>();

            options.AddRepository<EmployeeAttachment, EmployeeAttachments.EfCoreEmployeeAttachmentRepository>();

            options.AddRepository<SalesOrgHeader, SalesOrgHeaders.EfCoreSalesOrgHeaderRepository>();

            options.AddRepository<SalesOrgHierarchy, SalesOrgHierarchies.EfCoreSalesOrgHierarchyRepository>();

            options.AddRepository<CusAttributeValue, CusAttributeValues.EfCoreCusAttributeValueRepository>();

            options.AddRepository<CustomerContact, CustomerContacts.EfCoreCustomerContactRepository>();

            options.AddRepository<CustomerAttachment, CustomerAttachments.EfCoreCustomerAttachmentRepository>();

            options.AddRepository<Vendor, Vendors.EfCoreVendorRepository>();

            options.AddRepository<SystemConfig, SystemConfigs.EfCoreSystemConfigRepository>();

            options.AddRepository<Customer, Customers.EfCoreCustomerRepository>();

            options.AddRepository<CompanyIdentityUserAssignment, CompanyIdentityUserAssignments.EfCoreCompanyIdentityUserAssignmentRepository>();

        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure<MdmServiceDbContext>(c =>
            {
                c.UseSqlServer(b =>
                {
                    b.MigrationsHistoryTable("__MdmService_Migrations");
                });
            });
        });
    }
}