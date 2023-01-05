namespace DMSpro.OMS.MdmService.Web.Menus;

public class MdmServiceMenus
{
    public const string Prefix = "MdmService";

    public const string Administration = Prefix + ".Administration";
    public const string SystemData = Administration + ".SystemData";
    public const string SystemConfig = Administration + ".SystemConfig";
    public const string NumberingConfig = Administration + ".NumberingConfig";

    public const string Geographical = Prefix + ".Geographical";
    public const string GeoMasters = Geographical + ".GeoMasters";
    public const string Streets = Geographical + ".Streets";
    public const string Maps = Geographical + ".Maps";

    public const string Companies = Prefix + ".Companies";
    public const string CompanyMaster = Companies + ".CompanyMasters";
    public const string Currencies = Companies + ".Currencies";
    public const string Measurements = Companies + ".Measurements";
    public const string DimensionMeasurements = Measurements + ".DimensionMeasurements";
    public const string WeightMeasurements = Measurements + ".WeightMeasurements";
    public const string VATs = Companies + ".VATs";
    public const string SalesChannels = Companies + ".SalesChannels";
    public const string CompanyIdentityUserAssignments = Companies + ".CompanyIdentityUserAssignments";

    public const string Item = Prefix + ".Products";
    public const string UOMs = Item + ".UOMs";
    public const string UOMGroups = Item + ".UOMGroups";
    public const string UOMGroupDef = UOMGroups + ".UOMGroupDef";
    public const string UOMGroupDetails = UOMGroups + ".UOMGroupDetails";
    public const string ItemAttributes = Item + ".ItemAttributes";
    public const string ItemAttributeValues = Item + ".ItemAttributeValues";
    public const string Items = Item + ".Items";
    public const string ItemImages = Item + ".ItemImages";
    public const string ItemAttachments = Item + ".ItemAttachments";
    public const string ItemGroups = Item + ".ItemGroups";
    public const string PriceLists = Item + ".PriceLists";
    public const string PriceListDefs = PriceLists + ".PriceListDefs";
    public const string PriceListDetails = PriceLists + ".PriceListDetails";
    public const string PriceUpdate = Item + ".PriceUpdates";
    public const string PriceUpdateDefs = PriceUpdate + ".PriceUpdateDefs";
    public const string PriceUpdateDetails = PriceUpdate + ".PriceUpdateDetails";
    public const string PriceListAssignments = Item + ".PriceAssignments";

    public const string SalesOrganizations = Prefix + ".SalesOrganizations";
    public const string WorkingPositions = SalesOrganizations + ".WorkingPositions";
    public const string EmployeeProfiles = SalesOrganizations + ".EmployeeProfiles";
    public const string EmployeeImages = SalesOrganizations + ".EmployeeImages";
    public const string EmployeeAttachments = SalesOrganizations + ".EmployeeAttachments";
    public const string SalesOrgs = SalesOrganizations + ".SalesOrgs";
    public const string SalesOrgHeaders = SalesOrgs + ".SalesOrgHeaders";
    public const string SalesOrgHierarchies = Prefix + ".SalesOrgHierarchies";
    public const string SalesOrgEmpAssignments = SalesOrgs + ".SalesOrgEmpAssignments";
    public const string SellingZones = SalesOrganizations + ".SellingZones";
    public const string CompanyInZones = SellingZones + ".CompanyInZones";
    public const string CustomerInZones = SellingZones + ".CustomerInZones";
    public const string EmployeeInZones = SellingZones + ".EmployeeInZones";

    public const string Customers = Prefix + ".Customers";
    public const string CustomerAttributes = Customers + ".CustomerAttributes";
    public const string CustomerAttributeDefs = CustomerAttributes + ".CustomerAttributeDefs";
    public const string CusAttributeValues = CustomerAttributes + ".CusAttributeValues";
    public const string CustomerProfiles = Customers + ".CustomerProfiles";
    public const string CustomerContacts = CustomerProfiles + ".CustomerContacts";
    public const string CustomerAttachments = CustomerProfiles + ".CustomerAttachments";
    public const string Vendors = Customers + ".Vendors";
    public const string CustomerGroups = Customers + ".CustomerGroups";
    public const string CustomerGroupDefs = CustomerGroups + ".CustomerGroupDefs";
    public const string CustomerGroupByAtts = CustomerGroups + ".CustomerGroupByAtts";
    public const string CustomerGroupByLists = CustomerGroups + ".CustomerGroupByLists";
    public const string CustomerGroupByGeos = CustomerGroups + ".CustomerGroupByGeos";
    public const string CustomerAssignments = Customers + ".CustomerAssignments";

    public const string RouteAndMCP = Prefix + ".RoutesAndMCPs";
    public const string HolidayDefs = RouteAndMCP + ".HolidayDefs";
    public const string HolidayDetails = RouteAndMCP + ".HolidayDetails";
    public const string Routes = RouteAndMCP + ".Routes";
    public const string MCPHeaders = RouteAndMCP + ".MCPHeader";
    public const string MCPDetails = RouteAndMCP + ".MCPDetail";
    public const string VisitPlans = RouteAndMCP + ".VisitPlans";
    public const string RouteAssignments = RouteAndMCP + ".RouteAssignments";
}