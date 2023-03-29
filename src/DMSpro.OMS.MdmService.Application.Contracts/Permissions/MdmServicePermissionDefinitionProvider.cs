using DMSpro.OMS.MdmService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Features;
using Volo.Abp.Localization;

namespace DMSpro.OMS.MdmService.Permissions;

public class MdmServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MdmServicePermissions.GroupName, L("Permission:MdmService:MdmService"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));

        // Administration group
        var systemDataPermission = myGroup.AddPermission(MdmServicePermissions.SystemData.Default, L("Permission:MdmService:SystemData")).RequireFeatures(MdmFeatures.SystemData);
        systemDataPermission.AddChild(MdmServicePermissions.SystemData.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.SystemData);
        systemDataPermission.AddChild(MdmServicePermissions.SystemData.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.SystemData);
        systemDataPermission.AddChild(MdmServicePermissions.SystemData.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.SystemData);

        var systemConfigPermission = myGroup.AddPermission(MdmServicePermissions.SystemConfig.Default, L("Permission:MdmService:SystemConfigs")).RequireFeatures(MdmFeatures.SystemConfig);
        systemConfigPermission.AddChild(MdmServicePermissions.SystemConfig.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.SystemConfig);
        systemConfigPermission.AddChild(MdmServicePermissions.SystemConfig.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.SystemConfig);
        systemConfigPermission.AddChild(MdmServicePermissions.SystemConfig.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.SystemConfig);

        var numberingConfigPermission = myGroup.AddPermission(MdmServicePermissions.NumberingConfigs.Default, L("Permission:MdmService:NumberingConfigs")).RequireFeatures(MdmFeatures.NumberingConfig);
        numberingConfigPermission.AddChild(MdmServicePermissions.NumberingConfigs.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.NumberingConfig);
        numberingConfigPermission.AddChild(MdmServicePermissions.NumberingConfigs.CreateDetail, L("Permission:Create")).RequireFeatures(MdmFeatures.NumberingConfig);

        // Geographical group
        var geoMasterPermission = myGroup.AddPermission(MdmServicePermissions.GeoMasters.Default, L("Permission:MdmService:GeoMaster")).RequireFeatures(MdmFeatures.GeoMaster);
        geoMasterPermission.AddChild(MdmServicePermissions.GeoMasters.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.GeoMaster);
        geoMasterPermission.AddChild(MdmServicePermissions.GeoMasters.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.GeoMaster);
        geoMasterPermission.AddChild(MdmServicePermissions.GeoMasters.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.GeoMaster);

        // Company group
        var companyPermission = myGroup.AddPermission(MdmServicePermissions.CompanyMasters.Default, L("Permission:MdmService:CompanyProfile")).RequireFeatures(MdmFeatures.CompanyMaster);
        companyPermission.AddChild(MdmServicePermissions.CompanyMasters.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CompanyMaster);
        companyPermission.AddChild(MdmServicePermissions.CompanyMasters.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CompanyMaster);
        companyPermission.AddChild(MdmServicePermissions.CompanyMasters.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CompanyMaster);

        var vatPermission = myGroup.AddPermission(MdmServicePermissions.VATs.Default, L("Permission:MdmService:VATs")).RequireFeatures(MdmFeatures.VATs);
        vatPermission.AddChild(MdmServicePermissions.VATs.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.VATs);
        vatPermission.AddChild(MdmServicePermissions.VATs.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.VATs);
        vatPermission.AddChild(MdmServicePermissions.VATs.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.VATs);

        var companyIdentityUserAssignmentPermission = myGroup.AddPermission(MdmServicePermissions.CompanyIdentityUserAssignments.Default, L("Permission:MdmService:CompanyIdentityUserAssignments")).RequireFeatures(MdmFeatures.CompanyIdentityUserAssignments);
        companyIdentityUserAssignmentPermission.AddChild(MdmServicePermissions.CompanyIdentityUserAssignments.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CompanyIdentityUserAssignments);
        companyIdentityUserAssignmentPermission.AddChild(MdmServicePermissions.CompanyIdentityUserAssignments.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CompanyIdentityUserAssignments);
        companyIdentityUserAssignmentPermission.AddChild(MdmServicePermissions.CompanyIdentityUserAssignments.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CompanyIdentityUserAssignments);

        // Product group
        var uomPermission = myGroup.AddPermission(MdmServicePermissions.UOMs.Default, L("Permission:MdmService:UOM")).RequireFeatures(MdmFeatures.UOMs);
        uomPermission.AddChild(MdmServicePermissions.UOMs.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.UOMs);
        uomPermission.AddChild(MdmServicePermissions.UOMs.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.UOMs);
        uomPermission.AddChild(MdmServicePermissions.UOMs.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.UOMs);

        var uomGroupPermission = myGroup.AddPermission(MdmServicePermissions.UOMGroups.Default, L("Permission:MdmService:UOMGroup")).RequireFeatures(MdmFeatures.UOMGroups);
        uomGroupPermission.AddChild(MdmServicePermissions.UOMGroups.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.UOMGroups);
        uomGroupPermission.AddChild(MdmServicePermissions.UOMGroups.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.UOMGroups);
        uomGroupPermission.AddChild(MdmServicePermissions.UOMGroups.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.UOMGroups);

        var uomGroupDetailPermission = myGroup.AddPermission(MdmServicePermissions.UOMGroupDetails.Default, L("Permission:MdmService:UOMGroupDetails")).RequireFeatures(MdmFeatures.UOMGroups);
        uomGroupDetailPermission.AddChild(MdmServicePermissions.UOMGroupDetails.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.UOMGroups);
        uomGroupDetailPermission.AddChild(MdmServicePermissions.UOMGroupDetails.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.UOMGroups);
        uomGroupDetailPermission.AddChild(MdmServicePermissions.UOMGroupDetails.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.UOMGroups);

        var itemAttributePermission = myGroup.AddPermission(MdmServicePermissions.ItemAttributes.Default, L("Permission:MdmService:ItemAttribute")).RequireFeatures(MdmFeatures.ItemAttributes);
        itemAttributePermission.AddChild(MdmServicePermissions.ItemAttributes.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.ItemAttributes);
        itemAttributePermission.AddChild(MdmServicePermissions.ItemAttributes.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.ItemAttributes);
        itemAttributePermission.AddChild(MdmServicePermissions.ItemAttributes.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.ItemAttributes);

        var itemAttributeValuePermission = myGroup.AddPermission(MdmServicePermissions.ItemAttributeValues.Default, L("Permission:MdmService:ItemAttributeValue")).RequireFeatures(MdmFeatures.ItemAttributes);
        itemAttributeValuePermission.AddChild(MdmServicePermissions.ItemAttributeValues.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.ItemAttributes);
        itemAttributeValuePermission.AddChild(MdmServicePermissions.ItemAttributeValues.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.ItemAttributes);
        itemAttributeValuePermission.AddChild(MdmServicePermissions.ItemAttributeValues.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.ItemAttributes);

        var itemPermission = myGroup.AddPermission(MdmServicePermissions.Items.Default, L("Permission:MdmService:Item")).RequireFeatures(MdmFeatures.Items);
        itemPermission.AddChild(MdmServicePermissions.Items.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.Items);
        itemPermission.AddChild(MdmServicePermissions.Items.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.Items);
        itemPermission.AddChild(MdmServicePermissions.Items.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.Items);

        var itemGroupPermission = myGroup.AddPermission(MdmServicePermissions.ItemGroups.Default, L("Permission:MdmService:ItemGroup")).RequireFeatures(MdmFeatures.ItemGroups);
        itemGroupPermission.AddChild(MdmServicePermissions.ItemGroups.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.ItemGroups);
        itemGroupPermission.AddChild(MdmServicePermissions.ItemGroups.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.ItemGroups);
        itemGroupPermission.AddChild(MdmServicePermissions.ItemGroups.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.ItemGroups);

        var priceListPermission = myGroup.AddPermission(MdmServicePermissions.PriceLists.Default, L("Permission:MdmService:PriceList")).RequireFeatures(MdmFeatures.PriceLists);
        priceListPermission.AddChild(MdmServicePermissions.PriceLists.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.PriceLists);
        priceListPermission.AddChild(MdmServicePermissions.PriceLists.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.PriceLists);
        priceListPermission.AddChild(MdmServicePermissions.PriceLists.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.PriceLists);
        priceListPermission.AddChild(MdmServicePermissions.PriceLists.Release, L("Permission:MdmService:PriceList:Release")).RequireFeatures(MdmFeatures.PriceLists);

        var priceListDetailPermission = myGroup.AddPermission(MdmServicePermissions.PriceListDetails.Default, L("Permission:MdmService:PriceList")).RequireFeatures(MdmFeatures.PriceLists);
        priceListDetailPermission.AddChild(MdmServicePermissions.PriceListDetails.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.PriceLists);
        priceListDetailPermission.AddChild(MdmServicePermissions.PriceListDetails.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.PriceLists);
        priceListDetailPermission.AddChild(MdmServicePermissions.PriceListDetails.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.PriceLists);

        var priceUpdatePermission = myGroup.AddPermission(MdmServicePermissions.PriceUpdates.Default, L("Permission:MdmService:PriceUpdate")).RequireFeatures(MdmFeatures.PriceUpdate);
        priceUpdatePermission.AddChild(MdmServicePermissions.PriceUpdates.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.PriceUpdate);
        priceUpdatePermission.AddChild(MdmServicePermissions.PriceUpdates.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.PriceUpdate);
        priceUpdatePermission.AddChild(MdmServicePermissions.PriceUpdates.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.PriceUpdate);

        var priceUpdateDetailPermission = myGroup.AddPermission(MdmServicePermissions.PriceUpdateDetails.Default, L("Permission:MdmService:PriceUpdateDetail")).RequireFeatures(MdmFeatures.PriceUpdate);
        priceUpdateDetailPermission.AddChild(MdmServicePermissions.PriceUpdateDetails.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.PriceUpdate);
        priceUpdateDetailPermission.AddChild(MdmServicePermissions.PriceUpdateDetails.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.PriceUpdate);
        priceUpdateDetailPermission.AddChild(MdmServicePermissions.PriceUpdateDetails.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.PriceUpdate);

        var pricelistAssignmentPermission = myGroup.AddPermission(MdmServicePermissions.PriceListAssignments.Default, L("Permission:MdmService:PriceListAssignment")).RequireFeatures(MdmFeatures.PriceListAssignments);
        pricelistAssignmentPermission.AddChild(MdmServicePermissions.PriceListAssignments.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.PriceListAssignments);
        pricelistAssignmentPermission.AddChild(MdmServicePermissions.PriceListAssignments.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.PriceListAssignments);
        pricelistAssignmentPermission.AddChild(MdmServicePermissions.PriceListAssignments.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.PriceListAssignments);
        pricelistAssignmentPermission.AddChild(MdmServicePermissions.PriceListAssignments.Release, L("Permission:MdmService:PriceListAssignment:Release")).RequireFeatures(MdmFeatures.PriceListAssignments);

        // Sales Organization group
        var workingPositionPermission = myGroup.AddPermission(MdmServicePermissions.WorkingPositions.Default, L("Permission:MdmService:WorkingPosition")).RequireFeatures(MdmFeatures.WorkingPositions);
        workingPositionPermission.AddChild(MdmServicePermissions.WorkingPositions.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.WorkingPositions);
        workingPositionPermission.AddChild(MdmServicePermissions.WorkingPositions.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.WorkingPositions);
        workingPositionPermission.AddChild(MdmServicePermissions.WorkingPositions.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.WorkingPositions);

        var employeeProfilePermission = myGroup.AddPermission(MdmServicePermissions.EmployeeProfiles.Default, L("Permission:MdmService:EmployeeProfile")).RequireFeatures(MdmFeatures.EmployeeProfiles);
        employeeProfilePermission.AddChild(MdmServicePermissions.EmployeeProfiles.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.EmployeeProfiles);
        employeeProfilePermission.AddChild(MdmServicePermissions.EmployeeProfiles.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.EmployeeProfiles);
        employeeProfilePermission.AddChild(MdmServicePermissions.EmployeeProfiles.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.EmployeeProfiles);

        var salesOrgHeaderPermission = myGroup.AddPermission(MdmServicePermissions.SalesOrgHeaders.Default, L("Permission:MdmService:SalesOrgHeader")).RequireFeatures(MdmFeatures.SalesOrgs);
        salesOrgHeaderPermission.AddChild(MdmServicePermissions.SalesOrgHeaders.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.SalesOrgs).RequireFeatures(MdmFeatures.SalesOrgs);
        salesOrgHeaderPermission.AddChild(MdmServicePermissions.SalesOrgHeaders.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.SalesOrgs).RequireFeatures(MdmFeatures.SalesOrgs);
        salesOrgHeaderPermission.AddChild(MdmServicePermissions.SalesOrgHeaders.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.SalesOrgs).RequireFeatures(MdmFeatures.SalesOrgs);

        var salesOrgHierarchyPermission = myGroup.AddPermission(MdmServicePermissions.SalesOrgHierarchies.Default, L("Permission:MdmService:SalesOrgHierarchy")).RequireFeatures(MdmFeatures.SalesOrgs);
        salesOrgHierarchyPermission.AddChild(MdmServicePermissions.SalesOrgHierarchies.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.SalesOrgs);
        salesOrgHierarchyPermission.AddChild(MdmServicePermissions.SalesOrgHierarchies.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.SalesOrgs);
        salesOrgHierarchyPermission.AddChild(MdmServicePermissions.SalesOrgHierarchies.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.SalesOrgs);

        var salesOrgEmpAssignmentPermission = myGroup.AddPermission(MdmServicePermissions.SalesOrgEmpAssignments.Default, L("Permission:MdmService:SalesOrgEmpAssignment")).RequireFeatures(MdmFeatures.SalesOrgs);
        salesOrgEmpAssignmentPermission.AddChild(MdmServicePermissions.SalesOrgEmpAssignments.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.SalesOrgs);
        salesOrgEmpAssignmentPermission.AddChild(MdmServicePermissions.SalesOrgEmpAssignments.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.SalesOrgs);
        salesOrgEmpAssignmentPermission.AddChild(MdmServicePermissions.SalesOrgEmpAssignments.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.SalesOrgs);

        var companyInZonePermission = myGroup.AddPermission(MdmServicePermissions.CompanyInZones.Default, L("Permission:MdmService:CompanyInZone")).RequireFeatures(MdmFeatures.SellingZones);
        companyInZonePermission.AddChild(MdmServicePermissions.CompanyInZones.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.SellingZones);
        companyInZonePermission.AddChild(MdmServicePermissions.CompanyInZones.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.SellingZones);
        companyInZonePermission.AddChild(MdmServicePermissions.CompanyInZones.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.SellingZones);

        var customerInZonePermission = myGroup.AddPermission(MdmServicePermissions.CustomerInZones.Default, L("Permission:MdmService:CustomerInZone")).RequireFeatures(MdmFeatures.SellingZones);
        customerInZonePermission.AddChild(MdmServicePermissions.CustomerInZones.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.SellingZones);
        customerInZonePermission.AddChild(MdmServicePermissions.CustomerInZones.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.SellingZones);
        customerInZonePermission.AddChild(MdmServicePermissions.CustomerInZones.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.SellingZones);

        var employeeInZonePermission = myGroup.AddPermission(MdmServicePermissions.EmployeeInZones.Default, L("Permission:MdmService:EmployeeInZone")).RequireFeatures(MdmFeatures.SellingZones);
        employeeInZonePermission.AddChild(MdmServicePermissions.EmployeeInZones.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.SellingZones);
        employeeInZonePermission.AddChild(MdmServicePermissions.EmployeeInZones.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.SellingZones);
        employeeInZonePermission.AddChild(MdmServicePermissions.EmployeeInZones.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.SellingZones);

        // Customer group
        var customerAttributePermission = myGroup.AddPermission(MdmServicePermissions.CustomerAttributes.Default, L("Permission:MdmService:CustomerAttribute")).RequireFeatures(MdmFeatures.CustomerAttributes);
        customerAttributePermission.AddChild(MdmServicePermissions.CustomerAttributes.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CustomerAttributes);
        customerAttributePermission.AddChild(MdmServicePermissions.CustomerAttributes.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CustomerAttributes);
        customerAttributePermission.AddChild(MdmServicePermissions.CustomerAttributes.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CustomerAttributes);

        var cusAttributeValuePermission = myGroup.AddPermission(MdmServicePermissions.CusAttributeValues.Default, L("Permission:MdmService:CusAttributeValue")).RequireFeatures(MdmFeatures.CustomerAttributes);
        cusAttributeValuePermission.AddChild(MdmServicePermissions.CusAttributeValues.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CustomerAttributes);
        cusAttributeValuePermission.AddChild(MdmServicePermissions.CusAttributeValues.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CustomerAttributes);
        cusAttributeValuePermission.AddChild(MdmServicePermissions.CusAttributeValues.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CustomerAttributes);

        var customerPermission = myGroup.AddPermission(MdmServicePermissions.Customers.Default, L("Permission:MdmService:Customer")).RequireFeatures(MdmFeatures.CustomerProfiles);
        customerPermission.AddChild(MdmServicePermissions.Customers.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CustomerProfiles);
        customerPermission.AddChild(MdmServicePermissions.Customers.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CustomerProfiles);
        customerPermission.AddChild(MdmServicePermissions.Customers.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CustomerProfiles);

        var vendorPermission = myGroup.AddPermission(MdmServicePermissions.Vendors.Default, L("Permission:MdmService:Vendor")).RequireFeatures(MdmFeatures.Vendors);
        vendorPermission.AddChild(MdmServicePermissions.Vendors.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.Vendors);
        vendorPermission.AddChild(MdmServicePermissions.Vendors.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.Vendors);
        vendorPermission.AddChild(MdmServicePermissions.Vendors.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.Vendors);

        var customerGroupPermission = myGroup.AddPermission(MdmServicePermissions.CustomerGroups.Default, L("Permission:MdmService:CustomerGroup")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupPermission.AddChild(MdmServicePermissions.CustomerGroups.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupPermission.AddChild(MdmServicePermissions.CustomerGroups.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupPermission.AddChild(MdmServicePermissions.CustomerGroups.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CustomerGroups);

        var customerGroupByAttPermission = myGroup.AddPermission(MdmServicePermissions.CustomerGroupByAtts.Default, L("Permission:MdmService:CustomerGroupByAtt")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupByAttPermission.AddChild(MdmServicePermissions.CustomerGroupByAtts.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupByAttPermission.AddChild(MdmServicePermissions.CustomerGroupByAtts.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupByAttPermission.AddChild(MdmServicePermissions.CustomerGroupByAtts.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CustomerGroups);

        var customerGroupByListPermission = myGroup.AddPermission(MdmServicePermissions.CustomerGroupByLists.Default, L("Permission:MdmService:CustomerGroupByList")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupByListPermission.AddChild(MdmServicePermissions.CustomerGroupByLists.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupByListPermission.AddChild(MdmServicePermissions.CustomerGroupByLists.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupByListPermission.AddChild(MdmServicePermissions.CustomerGroupByLists.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CustomerGroups);

        var customerGroupByGeoPermission = myGroup.AddPermission(MdmServicePermissions.CustomerGroupByGeos.Default, L("Permission:MdmService:CustomerGroupByGeo")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupByGeoPermission.AddChild(MdmServicePermissions.CustomerGroupByGeos.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupByGeoPermission.AddChild(MdmServicePermissions.CustomerGroupByGeos.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CustomerGroups);
        customerGroupByGeoPermission.AddChild(MdmServicePermissions.CustomerGroupByGeos.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CustomerGroups);

        var customerAssignmentPermission = myGroup.AddPermission(MdmServicePermissions.CustomerAssignments.Default, L("Permission:MdmService:CustomerAssignment")).RequireFeatures(MdmFeatures.CustomerAssignments); ;
        customerAssignmentPermission.AddChild(MdmServicePermissions.CustomerAssignments.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.CustomerAssignments); ;
        customerAssignmentPermission.AddChild(MdmServicePermissions.CustomerAssignments.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.CustomerAssignments); ;
        customerAssignmentPermission.AddChild(MdmServicePermissions.CustomerAssignments.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.CustomerAssignments); ;

        // Route & MCP group
        var holidayPermission = myGroup.AddPermission(MdmServicePermissions.Holidays.Default, L("Permission:MdmService:Holiday")).RequireFeatures(MdmFeatures.Holidays);
        holidayPermission.AddChild(MdmServicePermissions.Holidays.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.Holidays);
        holidayPermission.AddChild(MdmServicePermissions.Holidays.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.Holidays);
        holidayPermission.AddChild(MdmServicePermissions.Holidays.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.Holidays);

        var holidayDetailPermission = myGroup.AddPermission(MdmServicePermissions.HolidayDetails.Default, L("Permission:MdmService:HolidayDetail")).RequireFeatures(MdmFeatures.Holidays);
        holidayDetailPermission.AddChild(MdmServicePermissions.HolidayDetails.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.Holidays);
        holidayDetailPermission.AddChild(MdmServicePermissions.HolidayDetails.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.Holidays);
        holidayDetailPermission.AddChild(MdmServicePermissions.HolidayDetails.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.Holidays);

        var routePermission = myGroup.AddPermission(MdmServicePermissions.Routes.Default, L("Permission:MdmService:Route")).RequireFeatures(MdmFeatures.Routes);
        routePermission.AddChild(MdmServicePermissions.Routes.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.Routes);
        routePermission.AddChild(MdmServicePermissions.Routes.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.Routes);
        routePermission.AddChild(MdmServicePermissions.Routes.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.Routes);

        var mcpHeaderPermission = myGroup.AddPermission(MdmServicePermissions.MCPs.Default, L("Permission:MdmService:MCPs")).RequireFeatures(MdmFeatures.MCPs);
        mcpHeaderPermission.AddChild(MdmServicePermissions.MCPs.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.MCPs);
        mcpHeaderPermission.AddChild(MdmServicePermissions.MCPs.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.MCPs);
        mcpHeaderPermission.AddChild(MdmServicePermissions.MCPs.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.MCPs);

        var visitPlanPermission = myGroup.AddPermission(MdmServicePermissions.VisitPlans.Default, L("Permission:MdmService:VisitPlan")).RequireFeatures(MdmFeatures.VisitPlans);
        visitPlanPermission.AddChild(MdmServicePermissions.VisitPlans.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.VisitPlans);
        visitPlanPermission.AddChild(MdmServicePermissions.VisitPlans.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.VisitPlans);
        visitPlanPermission.AddChild(MdmServicePermissions.VisitPlans.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.VisitPlans);
        visitPlanPermission.AddChild(MdmServicePermissions.VisitPlans.GenerateVisitPlanFromMCP, L("Permission:MdmService:VisitPlan:GenerateFromMCP")).RequireFeatures(MdmFeatures.VisitPlans);

        var routeAssignmentPermission = myGroup.AddPermission(MdmServicePermissions.RouteAssignments.Default, L("Permission:MdmService:RouteAssignment")).RequireFeatures(MdmFeatures.RouteAssignments);
        routeAssignmentPermission.AddChild(MdmServicePermissions.RouteAssignments.Create, L("Permission:Create")).RequireFeatures(MdmFeatures.RouteAssignments);
        routeAssignmentPermission.AddChild(MdmServicePermissions.RouteAssignments.Edit, L("Permission:Edit")).RequireFeatures(MdmFeatures.RouteAssignments);
        routeAssignmentPermission.AddChild(MdmServicePermissions.RouteAssignments.Delete, L("Permission:Delete")).RequireFeatures(MdmFeatures.RouteAssignments);

        var masterDataManipulatorPermission = myGroup.AddPermission(MdmServicePermissions.MasterDataManipulators.Default, L("Permission:MdmService:MasterDataManipulators"));
        masterDataManipulatorPermission.AddChild(MdmServicePermissions.MasterDataManipulators.CreateNumberConfigs, L("Permission:MdmService:MasterDataManipulators:CreateNumberConfigs"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MdmServiceResource>(name);
    }
}