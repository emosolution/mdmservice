using DMSpro.OMS.MdmService.CustomerGroupAttributes;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroupGeos;
using DMSpro.OMS.MdmService.CustomerGroupLists;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.CustomerImages;
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
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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

            options.AddRepository<ItemGroup, ItemGroups.EfCoreItemGroupRepository>();

            options.AddRepository<PriceListDetail, PriceListDetails.EfCorePriceListDetailRepository>();

            options.AddRepository<PriceUpdate, PriceUpdates.EfCorePriceUpdateRepository>();

            options.AddRepository<PricelistAssignment, PricelistAssignments.EfCorePricelistAssignmentRepository>();

            options.AddRepository<SystemData, SystemDatas.EfCoreSystemDataRepository>();

            options.AddRepository<NumberingConfig, NumberingConfigs.EfCoreNumberingConfigRepository>();

            options.AddRepository<WorkingPosition, WorkingPositions.EfCoreWorkingPositionRepository>();

            options.AddRepository<SalesOrgEmpAssignment, SalesOrgEmpAssignments.EfCoreSalesOrgEmpAssignmentRepository>();

            options.AddRepository<CompanyInZone, CompanyInZones.EfCoreCompanyInZoneRepository>();

            options.AddRepository<CustomerInZone, CustomerInZones.EfCoreCustomerInZoneRepository>();

            options.AddRepository<CustomerAttribute, CustomerAttributes.EfCoreCustomerAttributeRepository>();

            options.AddRepository<CustomerGroup, CustomerGroups.EfCoreCustomerGroupRepository>();

            options.AddRepository<CustomerGroupByAtt, CustomerGroupByAtts.EfCoreCustomerGroupByAttRepository>();

            options.AddRepository<CustomerGroupByList, CustomerGroupByLists.EfCoreCustomerGroupByListRepository>();

            options.AddRepository<CustomerGroupByGeo, CustomerGroupByGeos.EfCoreCustomerGroupByGeoRepository>();

            options.AddRepository<CustomerAssignment, CustomerAssignments.EfCoreCustomerAssignmentRepository>();

            options.AddRepository<Holiday, Holidays.EfCoreHolidayRepository>();

            options.AddRepository<HolidayDetail, HolidayDetails.EfCoreHolidayDetailRepository>();

            options.AddRepository<MCPHeader, MCPHeaders.EfCoreMCPHeaderRepository>();

            options.AddRepository<MCPDetail, MCPDetails.EfCoreMCPDetailRepository>();

            options.AddRepository<VisitPlan, VisitPlans.EfCoreVisitPlanRepository>();

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

            options.AddRepository<ItemAttribute, ItemAttributes.EfCoreItemAttributeRepository>();

            options.AddRepository<ItemAttributeValue, ItemAttributeValues.EfCoreItemAttributeValueRepository>();

            options.AddRepository<ItemGroupAttribute, ItemGroupAttributes.EfCoreItemGroupAttributeRepository>();

            options.AddRepository<Item, Items.EfCoreItemRepository>();

            options.AddRepository<ItemImage, ItemImages.EfCoreItemImageRepository>();

            options.AddRepository<ItemAttachment, ItemAttachments.EfCoreItemAttachmentRepository>();

            options.AddRepository<ItemGroupList, ItemGroupLists.EfCoreItemGroupListRepository>();

            options.AddRepository<CustomerImage, CustomerImages.EfCoreCustomerImageRepository>();

            options.AddRepository<NumberingConfigDetail, NumberingConfigDetails.EfCoreNumberingConfigDetailRepository>();

            options.AddRepository<CustomerGroupList, CustomerGroupLists.EfCoreCustomerGroupListRepository>();

            options.AddRepository<CustomerGroupGeo, CustomerGroupGeos.EfCoreCustomerGroupGeoRepository>();

            options.AddRepository<CustomerAttributeValue, CustomerAttributeValues.EfCoreCustomerAttributeValueRepository>();

            options.AddRepository<CustomerGroupAttribute, CustomerGroupAttributes.EfCoreCustomerGroupAttributeRepository>();

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
        Configure<AbpEntityOptions>(options =>
        {
            options.Entity<GeoMaster>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Parent);
            });
            options.Entity<Company>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.GeoLevel0)
                .Include(o => o.GeoLevel1).Include(o => o.GeoLevel2).Include(o => o.GeoLevel3
                ).Include(o => o.GeoLevel4).Include(o => o.Parent);
            });
            options.Entity<CompanyInZone>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.SalesOrgHierarchy).Include(o => o.Company).Include(o => o.ItemGroup);
            });
            options.Entity<CusAttributeValue>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Parent).Include(o => o.CustomerAttribute);
            });

            options.Entity<CustomerAssignment>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Company).Include(o => o.Customer);
            });

            options.Entity<CustomerAttachment>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Customer);
            });

            options.Entity<CustomerImage>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Customer).Include(o => o.ItemPOSM);
            });

            options.Entity<CustomerContact>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Customer);
            });

            options.Entity<CustomerGroupByAtt>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.CustomerGroup).Include(o => o.CusAttributeValue);
            });

            options.Entity<CustomerGroupByGeo>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.CustomerGroup)
                    .Include(o => o.GeoMaster0).Include(o => o.GeoMaster1).Include(o => o.GeoMaster2)
                    .Include(o => o.GeoMaster3).Include(o => o.GeoMaster4);
            });

            options.Entity<CustomerGroupByList>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Customer).Include(o => o.CustomerGroup);
            });

            options.Entity<CustomerInZone>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Customer).Include(o => o.SalesOrgHierarchy);
            });

            options.Entity<Customer>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.GeoMaster0).Include(o => o.GeoMaster1)
                    .Include(o => o.GeoMaster2).Include(o => o.GeoMaster3)
                    .Include(o => o.GeoMaster4).Include(o => o.PriceList);
            });

            options.Entity<EmployeeAttachment>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.EmployeeProfile);
            });

            options.Entity<EmployeeImage>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.EmployeeProfile);
            });

            options.Entity<EmployeeProfile>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.EmployeeType).Include(o => o.WorkingPosition);
            });

            options.Entity<HolidayDetail>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Holiday);
            });

            options.Entity<ItemAttachment>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Item);
            });

            options.Entity<ItemAttributeValue>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Parent).Include(o => o.ItemAttribute);
            });

            options.Entity<ItemGroupList>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.ItemGroup).Include(o => o.UOM).Include(o => o.Item);
            });

            options.Entity<ItemImage>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Item);
            });

            options.Entity<Item>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.ItemType)
                    .Include(o => o.VAT).Include(o => o.UOMGroup)
                    .Include(o => o.InventoryUOM).Include(o => o.PurUOM)
                    .Include(o => o.SalesUOM);
            });

            options.Entity<MCPHeader>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Company)
                    .Include(o => o.ItemGroup).Include(o => o.SalesOrgHierarchy);
            });

            options.Entity<MCPDetail>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.MCPHeader).Include(o => o.Customer);
            });

            options.Entity<NumberingConfig>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.SystemData);
            });

            options.Entity<PricelistAssignment>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.CustomerGroup).Include(o => o.PriceList);
            });

            options.Entity<PriceListDetail>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.UOM).Include(o => o.Item).Include(o => o.PriceList);
            });

            options.Entity<PriceList>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.BasePriceList);
            });

            options.Entity<PriceUpdateDetail>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.PriceListDetail).Include(o => o.PriceUpdate);
            });

            options.Entity<PriceUpdate>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.PriceList);
            });

            options.Entity<SalesOrgEmpAssignment>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.EmployeeProfile).Include(o => o.SalesOrgHierarchy);
            });

            options.Entity<SalesOrgHierarchy>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Parent).Include(o => o.SalesOrgHeader);
            });

            options.Entity<UOMGroup>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Details);
            });

            options.Entity<UOMGroupDetail>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.UOMGroup).Include(o => o.BaseUOM).Include(o => o.AltUOM);
            });

            options.Entity<Vendor>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.PriceList).Include(o => o.Company)
                    .Include(o => o.GeoMaster0).Include(o => o.GeoMaster1).Include(o => o.GeoMaster2)
                    .Include(o => o.GeoMaster3).Include(o => o.GeoMaster4).Include(o => o.LinkedCompany);
            });

            options.Entity<VisitPlan>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query
                    .Include(o => o.ItemGroup).Include(o => o.Customer)
                    .Include(o => o.MCPDetail).Include(o => o.Route);
            });

            options.Entity<NumberingConfigDetail>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Company)
                    .Include(o => o.NumberingConfig);
            });

            options.Entity<CustomerGroupGeo>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.CustomerGroup)
                    .Include(o => o.GeoMaster0)
                    .Include(o => o.GeoMaster1)
                    .Include(o => o.GeoMaster2)
                    .Include(o => o.GeoMaster3)
                    .Include(o => o.GeoMaster4);
            });

            options.Entity<CustomerAttributeValue>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.CustomerAttribute)
                    .Include(o => o.Parent);
            });

            options.Entity<CustomerGroupAttribute>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.CustomerGroup)
                    .Include(o => o.Attr0).Include(o => o.Attr1)
                    .Include(o => o.Attr2).Include(o => o.Attr3)
                    .Include(o => o.Attr4).Include(o => o.Attr5)
                    .Include(o => o.Attr6).Include(o => o.Attr7)
                    .Include(o => o.Attr8).Include(o => o.Attr9)
                    .Include(o => o.Attr10).Include(o => o.Attr11)
                    .Include(o => o.Attr12).Include(o => o.Attr13)
                    .Include(o => o.Attr14).Include(o => o.Attr15)
                    .Include(o => o.Attr16).Include(o => o.Attr17)
                    .Include(o => o.Attr18).Include(o => o.Attr19);
            });

            options.Entity<CustomerGroupList>(orderOptions =>
            {
                orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.CustomerGroup)
                    .Include(o => o.Customer);
            });
        });
    }
}