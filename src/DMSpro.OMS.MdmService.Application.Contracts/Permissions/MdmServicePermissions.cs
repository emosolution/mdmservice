using Volo.Abp.Reflection;

namespace DMSpro.OMS.MdmService.Permissions;

public class MdmServicePermissions
{
    public const string GroupName = "MdmService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MdmServicePermissions));
    }

    // Administration group
    public static class SystemData
    {
        public const string Default = GroupName + ".SystemData";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class SystemConfig
    {
        public const string Default = GroupName + ".SystemConfig";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class NumberingConfigs
    {
        public const string Default = GroupName + ".NumberingConfig";
        public const string Edit = Default + ".Edit";
        public const string CreateDetail = Default + ".CreateDetail";
    }

    // Geographical group
    public static class GeoMasters
    {
        public const string Default = GroupName + ".GeoMasters";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Streets
    {
        public const string Default = GroupName + ".Streets";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    // Company group
    public static class CompanyMasters
    {
        public const string Default = GroupName + ".CompanyMasters";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Currencies
    {
        public const string Default = GroupName + ".Currencies";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class DimensionMeasurements
    {
        public const string Default = GroupName + ".DimensionMeasurements";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class WeightMeasurements
    {
        public const string Default = GroupName + ".WeightMeasurements";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class VATs
    {
        public const string Default = GroupName + ".VATs";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CompanyIdentityUserAssignments
    {
        public const string Default = GroupName + ".CompanyIdentityUserAssignments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    // Product group
    public static class UOMs
    {
        public const string Default = GroupName + ".UOMs";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class UOMGroups
    {
        public const string Default = GroupName + ".UOMGroups";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class UOMGroupDetails
    {
        public const string Default = GroupName + ".UOMGroupDetails";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class ItemAttributes
    {
        public const string Default = GroupName + ".ItemAttributes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Items
    {
        public const string Default = GroupName + ".Items";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class ItemGroups
    {
        public const string Default = GroupName + ".ItemGroups";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PriceLists
    {
        public const string Default = GroupName + ".PriceLists";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Release = Default + ".Relase";
    }

    public static class PriceListDetails
    {
        public const string Default = GroupName + ".PriceListDetails";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PriceUpdates
    {
        public const string Default = GroupName + ".PriceUpdates";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Release = Default + ".Release";
    }

    public static class PriceListAssignments
    {
        public const string Default = GroupName + ".PricelistAssignments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Release = Default + ".Release";
    }

    // Sales Organization group
    public static class WorkingPositions
    {
        public const string Default = GroupName + ".WorkingPositions";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class EmployeeProfiles
    {
        public const string Default = GroupName + ".EmployeeProfiles";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class SalesOrgHeaders
    {
        public const string Default = GroupName + ".SalesOrgHeaders";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class SalesOrgHierarchies
    {
        public const string Default = GroupName + ".SalesOrgHierarchies";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class SalesOrgEmpAssignments
    {
        public const string Default = GroupName + ".SalesOrgEmpAssignments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CompanyInZones
    {
        public const string Default = GroupName + ".CompanyInZones";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CustomerInZones
    {
        public const string Default = GroupName + ".CustomerInZones";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class EmployeeInZones
    {
        public const string Default = GroupName + ".EmployeeInZones";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    // Customer group
    public static class CustomerAttributes
    {
        public const string Default = GroupName + ".CustomerAttributes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Customers
    {
        public const string Default = GroupName + ".Customers";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CustomerGroups
    {
        public const string Default = GroupName + ".CustomerGroups";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CustomerAssignments
    {
        public const string Default = GroupName + ".CustomerAssignments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    // Route & MCP group
    public static class Holidays
    {
        public const string Default = GroupName + ".Holidays";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class HolidayDetails
    {
        public const string Default = GroupName + ".HolidayDetails";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Routes
    {
        public const string Default = GroupName + ".Routes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class MCPs
    {
        public const string Default = GroupName + ".MCPs";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class VisitPlans
    {
        public const string Default = GroupName + ".VisitPlans";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string GenerateVisitPlanFromMCP = Default + ".GenerateVisitPlanFromMCP";
    }

    public static class RouteAssignments
    {
        public const string Default = GroupName + ".RouteAssignments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class HostAdmin
    {
        public const string Default = GroupName + ".HostAdmin";
        public const string Seeding = Default + ".Seeding";
    }

    public static class Vendors
    {
        public const string Default = GroupName + ".Vendors";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class MasterDataManipulators
    {
        public const string Default = GroupName + ".MasterDataManipulators";
        public const string CreateNumberConfigs = Default + ".CreateNumberConfigs";
    }
}