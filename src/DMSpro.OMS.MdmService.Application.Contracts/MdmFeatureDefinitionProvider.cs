using DMSpro.OMS.MdmService.Localization;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace DMSpro.OMS.MdmService
{
    public class MdmFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            var masterGroup = context.AddGroup(MdmFeatures.GroupName,
                 LocalizableString.Create<MdmServiceResource>("Feature:MDMService:MDMService"));

            var enableMdm = masterGroup.AddFeature(
                MdmFeatures.Enable,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableMDM"),
                valueType: new ToggleStringValueType()
            );

            // Administration group
            var enableAdministration = enableMdm.CreateChild(
                MdmFeatures.Administration,
                defaultValue: "true",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableAdministration"),
                valueType: new ToggleStringValueType()
            );

            enableAdministration.CreateChild(
              MdmFeatures.SystemData,
              defaultValue: "true",
              displayName: LocalizableString
                               .Create<MdmServiceResource>("Feature:MDMService:EnableSystemData"),
              valueType: new ToggleStringValueType()
            );

            enableAdministration.CreateChild(
              MdmFeatures.SystemConfig,
              defaultValue: "true",
              displayName: LocalizableString
                               .Create<MdmServiceResource>("Feature:MDMService:EnableSystemConfig"),
              valueType: new ToggleStringValueType()
            );

            enableAdministration.CreateChild(
              MdmFeatures.NumberingConfig,
              defaultValue: "true",
              displayName: LocalizableString
                               .Create<MdmServiceResource>("Feature:MDMService:EnableNumberingConfig"),
              valueType: new ToggleStringValueType()
            );

            // Geographical group
            var enableGeographical = enableMdm.CreateChild(
                MdmFeatures.Geographical,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableGeographical"),
                valueType: new ToggleStringValueType()
            );

            enableGeographical.CreateChild(
                MdmFeatures.GeoMaster,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableGeoMaster"),
                valueType: new ToggleStringValueType()
            );
            
            // Company group
             var enableCompany = enableMdm.CreateChild(
                MdmFeatures.Company,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableCompany"),
                valueType: new ToggleStringValueType()
            );

            enableCompany.CreateChild(
                MdmFeatures.CompanyMaster,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableCompanyProfile"),
                valueType: new ToggleStringValueType()
            );

            enableCompany.CreateChild(
                MdmFeatures.VATs,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableVAT"),
                valueType: new ToggleStringValueType()
            );

            enableCompany.CreateChild(
                MdmFeatures.SalesChannels,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableSalesChannel"),
                valueType: new ToggleStringValueType()
            );

            enableCompany.CreateChild(
                MdmFeatures.CompanyIdentityUserAssignments,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableCompanyIdentityUserAssignment"),
                valueType: new ToggleStringValueType()
            );

            // Item group
            var enableItem = enableMdm.CreateChild(
                MdmFeatures.Item,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableItem"),
                valueType: new ToggleStringValueType()
            );

            enableItem.CreateChild(
                MdmFeatures.UOMs,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableUOM"),
                valueType: new ToggleStringValueType()
            );

            enableItem.CreateChild(
                MdmFeatures.UOMGroups,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableUOMGroup"),
                valueType: new ToggleStringValueType()
            );

            enableItem.CreateChild(
                MdmFeatures.ItemAttributes,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableItemAttribute"),
                valueType: new ToggleStringValueType()
            );

            enableItem.CreateChild(
                MdmFeatures.Item,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableItemMaster"),
                valueType: new ToggleStringValueType()
            );

            enableItem.CreateChild(
                MdmFeatures.ItemGroups,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableItemGroup"),
                valueType: new ToggleStringValueType()
            );

            enableItem.CreateChild(
                MdmFeatures.PriceLists,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnablePriceList"),
                valueType: new ToggleStringValueType()
            );

            enableItem.CreateChild(
                MdmFeatures.PriceUpdate,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnablePriceUpdate"),
                valueType: new ToggleStringValueType()
            );

            enableItem.CreateChild(
                MdmFeatures.PriceListAssignments,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:PriceListAssignment"),
                valueType: new ToggleStringValueType()
            );

            // Sales Organizations group
            var enableSalesOrganization = enableMdm.CreateChild(
                MdmFeatures.SalesOrganizations,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableSalesOrganization"),
                valueType: new ToggleStringValueType()
            );

            enableSalesOrganization.CreateChild(
                MdmFeatures.WorkingPositions,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EnableWorkingPosition"),
                valueType: new ToggleStringValueType()
            );

            enableSalesOrganization.CreateChild(
                MdmFeatures.EmployeeProfiles,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:EmployeeProfile"),
                valueType: new ToggleStringValueType()
            );

            enableSalesOrganization.CreateChild(
                MdmFeatures.SalesOrgs,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:SalesOrg"),
                valueType: new ToggleStringValueType()
            );

            enableSalesOrganization.CreateChild(
                MdmFeatures.SellingZones,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:SellingZone"),
                valueType: new ToggleStringValueType()
            );

            // Customer group
            var enableCustomer = enableMdm.CreateChild(
                MdmFeatures.Customers,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:Customer"),
                valueType: new ToggleStringValueType()
            );

            enableCustomer.CreateChild(
                MdmFeatures.CustomerAttributes,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:CustomerAttribute"),
                valueType: new ToggleStringValueType()
            );

            enableCustomer.CreateChild(
                MdmFeatures.CustomerProfiles,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:CustomerProfile"),
                valueType: new ToggleStringValueType()
            );

            enableCustomer.CreateChild(
                MdmFeatures.Vendors,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:Vendor"),
                valueType: new ToggleStringValueType()
            );

            enableCustomer.CreateChild(
                MdmFeatures.CustomerGroups,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:CustomerGroup"),
                valueType: new ToggleStringValueType()
            );

            enableCustomer.CreateChild(
                MdmFeatures.CustomerAssignments,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:CustomerAssignment"),
                valueType: new ToggleStringValueType()
            );

            // Route & MCP group
            var enableRouteAndMCP = enableMdm.CreateChild(
                MdmFeatures.RouteAndMCP,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:RouteAndMCP"),
                valueType: new ToggleStringValueType()
            );

            enableRouteAndMCP.CreateChild(
                MdmFeatures.Holidays,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:Holiday"),
                valueType: new ToggleStringValueType()
            );

            enableRouteAndMCP.CreateChild(
                MdmFeatures.Routes,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:Route"),
                valueType: new ToggleStringValueType()
            );

            enableRouteAndMCP.CreateChild(
                MdmFeatures.MCPs,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:MCP"),
                valueType: new ToggleStringValueType()
            );

            enableRouteAndMCP.CreateChild(
                MdmFeatures.VisitPlans,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:VisitPlan"),
                valueType: new ToggleStringValueType()
            );

            enableRouteAndMCP.CreateChild(
                MdmFeatures.RouteAssignments,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<MdmServiceResource>("Feature:MDMService:RouteAssignment"),
                valueType: new ToggleStringValueType()
            );
        }
    }
}

