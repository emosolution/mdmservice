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
        public const string SalesChannels = Company + ".SalesChannels";
        public const string CompanyIdentityUserAssignments = Company + ".CompanyIdentityUserAssignments";

        //
        public const string Product = GroupName + ".Products";
        public const string UOMs = Product + ".UOMs";
        public const string UOMGroups = Product + ".UOMGroups";
        public const string ProductAttributes = Product + ".ProductAttributes";
        public const string Item = Product + ".Items";
        public const string ItemGroups = Product + ".ItemGroups";
        public const string PriceLists = Product + ".PriceLists";
        public const string PriceUpdate = Product + ".PriceUpdates";
        public const string PriceListAssignments = Product + ".PriceListAssignments";
        
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
