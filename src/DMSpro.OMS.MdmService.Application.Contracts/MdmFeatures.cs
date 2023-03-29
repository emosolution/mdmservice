namespace DMSpro.OMS.MdmService
{
    public class MdmFeatures
    {
        public const string GroupName = "MDM";

        public const string Enable = GroupName + ".Enable";

        //
        public const string Administration = GroupName + ".Administration";
        public const string SystemData = Administration + ".SystemData";
        public const string SystemConfig = Administration + ".SystemConfig";
        public const string NumberingConfig = Administration + ".NumberingConfig";

        //
        public const string Geographical = GroupName + ".Geogrphical";
        public const string GeoMaster = Geographical + ".GeoMasters";
        public const string Street = Geographical + ".Streets";

        //
        public const string Company = GroupName + ".Companies";
        public const string CompanyMaster = Company + ".CompanyMasters";
        public const string Currencies = Company + ".Currencies";
        public const string Measurements = Company + ".Measurements";
        public const string VATs = Company + ".VATs";
        public const string CompanyIdentityUserAssignments = Company + ".CompanyIdentityUserAssignments";

        //
        public const string ItemMaster = GroupName + ".Products";
        public const string UOMs = ItemMaster + ".UOMs";
        public const string UOMGroups = ItemMaster + ".UOMGroups";
        public const string ItemAttributes = ItemMaster + ".ItemAttributes";
        public const string Items = ItemMaster + ".Items";
        public const string ItemGroups = ItemMaster + ".ItemGroups";
        public const string PriceLists = ItemMaster + ".PriceLists";
        public const string PriceUpdate = ItemMaster + ".PriceUpdates";
        public const string PriceListAssignments = ItemMaster + ".PriceListAssignments";
        
        //
        public const string SalesOrganizations = GroupName + ".SalesOrganizations";
        public const string WorkingPositions = SalesOrganizations + ".WorkingPositions";
        public const string EmployeeProfiles = SalesOrganizations + ".EmployeeProfiles";
        public const string SalesOrgs = SalesOrganizations + ".SalesOrgs";
        public const string SellingZones = SalesOrganizations + ".SellingZones";

        //
        public const string Customers = GroupName + ".Customers";
        public const string	CustomerAttributes = Customers + ".CustomerAttributes";
        public const string CustomerProfiles = Customers + ".CustomerProfiles";
        public const string Vendors = Customers + ".Vendors";
        public const string CustomerGroups = Customers + ".CustomerGroups";
        public const string CustomerAssignments = Customers + ".CustomerAssignments";

        //
        public const string RouteAndMCP = GroupName + ".RoutesAndMCPs";
        public const string Holidays = RouteAndMCP + ".Holidays";
        public const string Routes = RouteAndMCP + ".Routes";
        public const string MCPs = RouteAndMCP + ".MCPs";
        public const string VisitPlans = RouteAndMCP + ".VisitPlans";
        public const string RouteAssignments = RouteAndMCP + ".RouteAssignments";

        // 
        // public const string HostAdmin = GroupName + ".HostAdmin";
        // public const string HostAdminSeeding = HostAdmin + ".Seeding";
    }
}
