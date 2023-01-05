using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Features;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Authorization.Permissions;

namespace DMSpro.OMS.MdmService.Web.Menus;

public class MdmServiceMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }

        var moduleMenu = AddModuleMenuItem(context);
        AddMenuItemAdministration(context, moduleMenu);
        AddMenuItemGeographical(context, moduleMenu);
        AddMenuItemCompanies(context, moduleMenu);
        AddMenuItemProducts(context, moduleMenu);
        AddMenuItemSalesOrganizations(context, moduleMenu);
        AddMenuItemCustomers(context, moduleMenu);
        AddMenuItemRouteAndMCP(context, moduleMenu);

        AddMenuItemCompanyIdentityUserAssignments(context, moduleMenu);
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<MdmServiceResource>();
        return Task.CompletedTask;
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            MdmServiceMenus.Prefix,
            context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:MdmService"],
            icon: "fa fa-folder"
        ).RequireFeatures(MdmFeatures.Enable);

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }

    private static void AddMenuItemAdministration(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        ApplicationMenuItem groupMenu = new ApplicationMenuItem(
               Menus.MdmServiceMenus.Administration,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:GroupMenu:Administration"],
               null,
               icon: "fa fa-file-alt"
           )
            .RequirePermissions(false, MdmServicePermissions.SystemData.Default,
                MdmServicePermissions.SystemConfig.Default, MdmServicePermissions.NumberingConfigs.Default)
            .RequireFeatures(false, MdmFeatures.SystemData, MdmFeatures.SystemConfig, MdmFeatures.NumberingConfig);

        parentMenu.AddItem(groupMenu);

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.SystemData,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:SystemData"],
                "/Mdm/SystemDatas",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.SystemData.Default
            ).RequireFeatures(MdmFeatures.SystemData)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.SystemConfig,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:SystemConfigs"],
                "/SystemConfigs",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.SystemConfig.Default
            ).RequireFeatures(MdmFeatures.SystemConfig)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.NumberingConfig,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:NumberingConfigs"],
                "/Mdm/NumberingConfigs",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.NumberingConfigs.Default
            ).RequireFeatures(MdmFeatures.NumberingConfig)
        );
    }

    private static void AddMenuItemGeographical(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        ApplicationMenuItem groupMenu = new ApplicationMenuItem(
               Menus.MdmServiceMenus.Geographical,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:GroupMenu:Geographical"],
               null,
               icon: "fa fa-file-alt"
           )
            .RequirePermissions(false, MdmServicePermissions.GeoMasters.Default)
            .RequireFeatures(false, MdmFeatures.GeoMaster);

        parentMenu.AddItem(groupMenu);

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.GeoMasters,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:GeoMasters"],
                "/Mdm/GeoMasters",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.GeoMasters.Default
            ).RequireFeatures(MdmFeatures.GeoMaster)
        );
    }

    private static void AddMenuItemCompanies(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        ApplicationMenuItem groupMenu = new ApplicationMenuItem(
               Menus.MdmServiceMenus.Companies,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:GroupMenu:Company"],
               null,
               icon: "fa fa-file-alt"
           )
            .RequirePermissions(false, MdmServicePermissions.CompanyMasters.Default,
                MdmServicePermissions.VATs.Default,
                MdmServicePermissions.SalesChannels.Default,
                MdmServicePermissions.CompanyIdentityUserAssignments.Default)
            .RequireFeatures(false,
                MdmFeatures.CompanyMaster,
                MdmFeatures.VATs,
                MdmFeatures.SalesChannels,
                MdmFeatures.CompanyIdentityUserAssignments);

        parentMenu.AddItem(groupMenu);

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CompanyMaster,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CompanyProfiles"],
                "/Mdm/Companies",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.CompanyMasters.Default
            ).RequireFeatures(MdmFeatures.CompanyMaster)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.VATs,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:VATs"],
                "/Mdm/VATs",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.VATs.Default
            ).RequireFeatures(MdmFeatures.VATs)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.SalesChannels,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:SalesChannels"],
                "/Mdm/SalesChannels",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.SalesChannels.Default
            ).RequireFeatures(MdmFeatures.SalesChannels)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CompanyIdentityUserAssignments,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CompanyIdentityUserAssignments"],
                "/CompanyIdentityUserAssignments",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.CompanyIdentityUserAssignments.Default
            ).RequireFeatures(MdmFeatures.CompanyIdentityUserAssignments)
        );
    }

    private static void AddMenuItemProducts(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        ApplicationMenuItem groupMenu = new ApplicationMenuItem(
               Menus.MdmServiceMenus.Product,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:GroupMenu:Product"],
               null,
               icon: "fa fa-file-alt"
           )
            .RequirePermissions(false, MdmServicePermissions.UOMs.Default,
                MdmServicePermissions.UOMGroups.Default,
                MdmServicePermissions.UOMGroupDetails.Default,
                MdmServicePermissions.ProductAttributes.Default,
                MdmServicePermissions.ProdAttributeValues.Default,
                MdmServicePermissions.ItemMasters.Default,
                MdmServicePermissions.ItemImages.Default,
                MdmServicePermissions.ItemAttachments.Default,
                MdmServicePermissions.ItemGroups.Default,
                MdmServicePermissions.ItemGroupAttrs.Default,
                MdmServicePermissions.ItemGroupLists.Default,
                MdmServicePermissions.PriceLists.Default,
                MdmServicePermissions.PriceListDetails.Default,
                MdmServicePermissions.PriceUpdates.Default,
                MdmServicePermissions.PriceUpdateDetails.Default,
                MdmServicePermissions.PriceListAssignments.Default)
            .RequireFeatures(false, MdmFeatures.UOMs,
                MdmFeatures.UOMGroups,
                MdmFeatures.ProductAttributes,
                MdmFeatures.Item, MdmFeatures.ItemGroups,
                MdmFeatures.PriceLists, MdmFeatures.PriceUpdate,
                MdmFeatures.PriceListAssignments);

        parentMenu.AddItem(groupMenu);

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.UOMs,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:UOMs"],
                "/Mdm/UOMs",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.UOMs.Default
            ).RequireFeatures(MdmFeatures.UOMs)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.UOMGroups,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:UOMGroups"],
                "/Mdm/UOMGroups",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.UOMGroups.Default
            ).RequireFeatures(MdmFeatures.UOMGroups)
        );

        groupMenu.AddItem(
           new ApplicationMenuItem(
               Menus.MdmServiceMenus.UOMGroupDetails,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:UOMGroupDetails"],
               "/Mdm/UOMGroupDetails",
               icon: "fa fa-file-alt",
               requiredPermissionName: MdmServicePermissions.UOMGroupDetails.Default
           ).RequireFeatures(MdmFeatures.UOMGroups)
        );

        groupMenu.AddItem(
          new ApplicationMenuItem(
              Menus.MdmServiceMenus.ProductAttributes,
              context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:ProductAttributes"],
              "/Mdm/ProductAttributes",
              icon: "fa fa-file-alt",
              requiredPermissionName: MdmServicePermissions.ProductAttributes.Default
          ).RequireFeatures(MdmFeatures.ProductAttributes)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.ProdAttributeValues,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:ProdAttributeValue"],
                "/Mdm/ProdAttributeValues",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.ProdAttributeValues.Default
            ).RequireFeatures(MdmFeatures.ProductAttributes)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.ItemMasters,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:ItemMasters"],
                "/Mdm/ItemMasters",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.ItemMasters.Default
            ).RequireFeatures(MdmFeatures.Item)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.ItemImages,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:ItemImages"],
                "/Mdm/ItemImages",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.ItemImages.Default
            ).RequireFeatures(MdmFeatures.Item)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.ItemAttachments,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:ItemAttachments"],
                "/Mdm/ItemAttachments",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.ItemAttachments.Default
            ).RequireFeatures(MdmFeatures.Item)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.ItemGroups,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:ItemGroups"],
                "/Mdm/ItemGroups",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.ItemGroups.Default
            ).RequireFeatures(MdmFeatures.ItemGroups)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.ItemGroupAttrs,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:ItemGroupAttrs"],
                "/Mdm/ItemGroupAttrs",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.ItemGroupAttrs.Default
            ).RequireFeatures(MdmFeatures.ItemGroups)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.ItemGroupLists,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:ItemGroupLists"],
                "/Mdm/ItemGroupLists",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.ItemGroupLists.Default
            ).RequireFeatures(MdmFeatures.ItemGroups)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.PriceLists,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:PriceLists"],
                "/Mdm/PriceLists",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.PriceLists.Default
            ).RequireFeatures(MdmFeatures.PriceLists)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.PriceListDetails,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:PriceListDetails"],
                "/Mdm/PriceListDetails",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.PriceListDetails.Default
            ).RequireFeatures(MdmFeatures.PriceLists)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.PriceUpdateDefs,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:PriceUpdates"],
                "/Mdm/PriceUpdates",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.PriceUpdates.Default
            ).RequireFeatures(MdmFeatures.PriceUpdate)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.PriceUpdateDetails,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:PriceUpdateDetails"],
                "/Mdm/PriceUpdateDetails",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.PriceUpdateDetails.Default
            ).RequireFeatures(MdmFeatures.PriceUpdate)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.PriceListAssignments,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:PriceListAssignments"],
                "/Mdm/PricelistAssignments",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.PriceListAssignments.Default
            ).RequireFeatures(MdmFeatures.PriceListAssignments)
        );
    }

    private static void AddMenuItemSalesOrganizations(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        ApplicationMenuItem groupMenu = new ApplicationMenuItem(
               Menus.MdmServiceMenus.SalesOrganizations,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:GroupMenu:SalesOrganization"],
               null,
               icon: "fa fa-file-alt"
           )
            .RequirePermissions(false,
                MdmServicePermissions.WorkingPositions.Default,
                MdmServicePermissions.EmployeeProfiles.Default,
                MdmServicePermissions.SalesOrgHeaders.Default,
                MdmServicePermissions.SalesOrgHierarchies.Default,
                MdmServicePermissions.SalesOrgEmpAssignments.Default,
                MdmServicePermissions.CompanyInZones.Default,
                MdmServicePermissions.CustomerInZones.Default,
                MdmServicePermissions.EmployeeInZones.Default)
            .RequireFeatures(false, MdmFeatures.WorkingPositions,
                MdmFeatures.EmployeeProfiles, MdmFeatures.SalesOrgs,
                MdmFeatures.SellingZones);

        parentMenu.AddItem(groupMenu);

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.WorkingPositions,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:WorkingPositions"],
                "/Mdm/WorkingPositions",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.WorkingPositions.Default
            ).RequireFeatures(MdmFeatures.WorkingPositions)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.EmployeeProfiles,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:EmployeeProfiles"],
                "/Mdm/EmployeeProfiles",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.EmployeeProfiles.Default
            ).RequireFeatures(MdmFeatures.EmployeeProfiles)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.EmployeeImages,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:EmployeeImages"],
                "/Mdm/EmployeeImages",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.EmployeeProfiles.Default
            ).RequireFeatures(MdmFeatures.EmployeeProfiles)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.EmployeeAttachments,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:EmployeeAttachments"],
                "/Mdm/EmployeeAttachments",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.EmployeeProfiles.Default
            ).RequireFeatures(MdmFeatures.EmployeeProfiles)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.SalesOrgHeaders,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:SalesOrgHeaders"],
                "/Mdm/SalesOrgHeaders",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.SalesOrgHeaders.Default
            ).RequireFeatures(MdmFeatures.SalesOrgs)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.SalesOrgHierarchies,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:SalesOrgHierarchies"],
                "/Mdm/SalesOrgHierarchies",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.SalesOrgHierarchies.Default
            ).RequireFeatures(MdmFeatures.SalesOrgs)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.SalesOrgEmpAssignments,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:SalesOrgEmpAssignments"],
                "/Mdm/SalesOrgEmpAssignments",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.SalesOrgEmpAssignments.Default
            ).RequireFeatures(MdmFeatures.SalesOrgs)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CompanyInZones,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CompanyInZone"],
                "/Mdm/CompanyInZones",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.CompanyInZones.Default
            ).RequireFeatures(MdmFeatures.SellingZones)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CustomerInZones,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CustomerInZone"],
                "/Mdm/CustomerInZones",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.CustomerInZones.Default
            ).RequireFeatures(MdmFeatures.SellingZones)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.EmployeeInZones,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:EmployeeInZone"],
                "/Mdm/EmployeeInZones",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.EmployeeInZones.Default
            ).RequireFeatures(MdmFeatures.SellingZones)
        );

    }

    private static void AddMenuItemCustomers(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        ApplicationMenuItem groupMenu = new ApplicationMenuItem(
               Menus.MdmServiceMenus.Customers,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:Customer"],
               null,
               icon: "fa fa-file-alt"
           )
            .RequirePermissions(false, MdmServicePermissions.CustomerAttributes.Default,
                MdmServicePermissions.CusAttributeValues.Default,
                MdmServicePermissions.Vendors.Default,
                MdmServicePermissions.CustomerGroups.Default,
                MdmServicePermissions.CustomerGroupByAtts.Default,
                MdmServicePermissions.CustomerGroupByLists.Default,
                MdmServicePermissions.CustomerGroupByGeos.Default,
                MdmServicePermissions.Customers.Default,
                MdmServicePermissions.CustomerAssignments.Default)
            .RequireFeatures(false, MdmFeatures.CustomerAttributes,
                MdmFeatures.CustomerProfiles,
                MdmFeatures.CustomerGroups, MdmFeatures.CustomerAssignments);

        parentMenu.AddItem(groupMenu);

        groupMenu.AddItem(
           new ApplicationMenuItem(
               Menus.MdmServiceMenus.CustomerAttributeDefs,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CustomerAttributes"],
               "/Mdm/CustomerAttributes",
               icon: "fa fa-file-alt",
               requiredPermissionName: MdmServicePermissions.CustomerAttributes.Default
           ).RequireFeatures(MdmFeatures.CustomerAttributes)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CusAttributeValues,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CusAttributeValues"],
                "/Mdm/CusAttributeValues",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.CusAttributeValues.Default
            ).RequireFeatures(MdmFeatures.CustomerAttributes)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CustomerProfiles,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:Customers"],
                "/Customers",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.Customers.Default
            ).RequireFeatures(MdmFeatures.CustomerProfiles)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CustomerContacts,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CustomerContacts"],
                "/Mdm/CustomerContacts",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.Customers.Default
            ).RequireFeatures(MdmFeatures.CustomerProfiles)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CustomerAttachments,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CustomerAttachments"],
                "/Mdm/CustomerAttachments",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.Customers.Default
            ).RequireFeatures(MdmFeatures.CustomerProfiles)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.Vendors,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:Vendors"],
                "/Mdm/Vendors",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.Vendors.Default
            ).RequireFeatures(MdmFeatures.Vendors)
        );

        groupMenu.AddItem(
           new ApplicationMenuItem(
               Menus.MdmServiceMenus.CustomerAssignments,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CustomerAssignments"],
               "/Mdm/CustomerAssignments",
               icon: "fa fa-file-alt",
               requiredPermissionName: MdmServicePermissions.CustomerAssignments.Default
           ).RequireFeatures(MdmFeatures.CustomerAssignments)
       );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CustomerGroupDefs,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CustomerGroups"],
                "/Mdm/CustomerGroups",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.CustomerGroups.Default
            ).RequireFeatures(MdmFeatures.CustomerGroups)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CustomerGroupByAtts,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CustomerGroupsByAtt"],
                "/Mdm/CustomerGroupByAtts",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.CustomerGroupByAtts.Default
            ).RequireFeatures(MdmFeatures.CustomerGroups)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CustomerGroupByLists,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CustomerGroupsByList"],
                "/Mdm/CustomerGroupByLists",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.CustomerGroupByLists.Default
            ).RequireFeatures(MdmFeatures.CustomerGroups)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.CustomerGroupByGeos,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:CustomerGroupsByGeo"],
                "/Mdm/CustomerGroupByGeos",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.CustomerGroupByGeos.Default
            ).RequireFeatures(MdmFeatures.CustomerGroups)
        );
    }

    private static void AddMenuItemRouteAndMCP(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        ApplicationMenuItem groupMenu = new ApplicationMenuItem(
               Menus.MdmServiceMenus.RouteAndMCP,
               context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:RouteAndMCP"],
               null,
               icon: "fa fa-file-alt"
           )
            .RequirePermissions(false, MdmServicePermissions.Holidays.Default,
                MdmServicePermissions.HolidayDetails.Default,
                MdmServicePermissions.Routes.Default,
                MdmServicePermissions.MCPHeaders.Default,
                MdmServicePermissions.MCPDetails.Default,
                MdmServicePermissions.VisitPlans.Default,
                MdmServicePermissions.RouteAssignments.Default)
            .RequireFeatures(false, MdmFeatures.Holidays, MdmFeatures.Routes,
                MdmFeatures.MCPs, MdmFeatures.VisitPlans, MdmFeatures.RouteAssignments);

        parentMenu.AddItem(groupMenu);

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.HolidayDefs,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:Holidays"],
                "/Mdm/Holidays",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.Holidays.Default
            ).RequireFeatures(MdmFeatures.Holidays)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.HolidayDetails,
                context.GetLocalizer<MdmServiceResource>()["Page.Title.HolidayDetails"],
                "/Mdm/HolidayDetails",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.HolidayDetails.Default
            ).RequireFeatures(MdmFeatures.Holidays)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.Routes,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:Routes"],
                "/Mdm/Routes",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.Routes.Default
            ).RequireFeatures(MdmFeatures.Routes)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.MCPHeaders,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:MCPHeaders"],
                "/Mdm/MCPHeaders",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.MCPHeaders.Default
            ).RequireFeatures(MdmFeatures.MCPs)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.MCPDetails,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:MCPDetails"],
                "/Mdm/MCPDetails",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.MCPDetails.Default
            ).RequireFeatures(MdmFeatures.MCPs)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.VisitPlans,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:VisitPlans"],
                "/Mdm/VisitPlans",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.VisitPlans.Default
            ).RequireFeatures(MdmFeatures.VisitPlans)
        );

        groupMenu.AddItem(
            new ApplicationMenuItem(
                Menus.MdmServiceMenus.RouteAssignments,
                context.GetLocalizer<MdmServiceResource>()["Menu:MdmService:RouteAssignments"],
                "/Mdm/RouteAssignments",
                icon: "fa fa-file-alt",
                requiredPermissionName: MdmServicePermissions.RouteAssignments.Default
            ).RequireFeatures(MdmFeatures.RouteAssignments)
        );
    }

    private static void AddMenuItemCompanyIdentityUserAssignments(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        
    }
}