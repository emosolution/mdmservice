using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using DMSpro.OMS.MdmService.Localization;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.MultiTenancy;
using System;

namespace DMSpro.OMS.MdmService.Web;

[DependsOn(
    typeof(MdmServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class MdmServiceWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(MdmServiceResource), typeof(MdmServiceWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(MdmServiceWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MdmServiceMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MdmServiceWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<MdmServiceWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MdmServiceWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            // options.Conventions.AuthorizePage("/MdmService/Index", MdmServicePermissions.MdmService.Default);
            options.Conventions.AuthorizePage("/SystemDatas/Index", MdmServicePermissions.SystemData.Default);
            options.Conventions.AuthorizePage("/SystemConfigs/Index", MdmServicePermissions.SystemConfig.Default);
            options.Conventions.AuthorizePage("/NumberingConfigs/Index", MdmServicePermissions.NumberingConfigs.Default);
            options.Conventions.AuthorizePage("/GeoMasters/Index", MdmServicePermissions.GeoMasters.Default);
            options.Conventions.AuthorizePage("/Streets/Index", MdmServicePermissions.Streets.Default);
            options.Conventions.AuthorizePage("/Companies/Index", MdmServicePermissions.CompanyMasters.Default);
            options.Conventions.AuthorizePage("/Currencies/Index", MdmServicePermissions.Currencies.Default);
            options.Conventions.AuthorizePage("/DimensionMeasurements/Index", MdmServicePermissions.DimensionMeasurements.Default);
            options.Conventions.AuthorizePage("/WeightMeasurements/Index", MdmServicePermissions.WeightMeasurements.Default);
            options.Conventions.AuthorizePage("/VATs/Index", MdmServicePermissions.VATs.Default);
            options.Conventions.AuthorizePage("/UOMs/Index", MdmServicePermissions.UOMs.Default);
            options.Conventions.AuthorizePage("/UOMGroups/Index", MdmServicePermissions.UOMGroups.Default);
            options.Conventions.AuthorizePage("/UOMGroupDetails/Index", MdmServicePermissions.UOMGroupDetails.Default);
            options.Conventions.AuthorizePage("/ItemGroups/Index", MdmServicePermissions.ItemGroups.Default);
            options.Conventions.AuthorizePage("/PriceLists/Index", MdmServicePermissions.PriceLists.Default);
            options.Conventions.AuthorizePage("/PriceListDetails/Index", MdmServicePermissions.PriceListDetails.Default);
            options.Conventions.AuthorizePage("/PriceUpdates/Index", MdmServicePermissions.PriceUpdates.Default);
            options.Conventions.AuthorizePage("/PricelistAssignments/Index", MdmServicePermissions.PriceListAssignments.Default);
            options.Conventions.AuthorizePage("/WorkingPositions/Index", MdmServicePermissions.WorkingPositions.Default);
            options.Conventions.AuthorizePage("/SalesOrgHeaders/Index", MdmServicePermissions.SalesOrgHeaders.Default);
            options.Conventions.AuthorizePage("/SalesOrgHierarchies/Index", MdmServicePermissions.SalesOrgHierarchies.Default);
            options.Conventions.AuthorizePage("/SalesOrgEmpAssignments/Index", MdmServicePermissions.SalesOrgEmpAssignments.Default);
            options.Conventions.AuthorizePage("/CompanyInZones/Index", MdmServicePermissions.CompanyInZones.Default);
            options.Conventions.AuthorizePage("/CustomerInZones/Index", MdmServicePermissions.CustomerInZones.Default);
            options.Conventions.AuthorizePage("/EmployeeInZones/Index", MdmServicePermissions.EmployeeInZones.Default);
            options.Conventions.AuthorizePage("/CustomerAttributes/Index", MdmServicePermissions.CustomerAttributes.Default);
            options.Conventions.AuthorizePage("/CustomerGroups/Index", MdmServicePermissions.CustomerGroups.Default);
            options.Conventions.AuthorizePage("/CustomerGroupByAtts/Index", MdmServicePermissions.CustomerGroupByAtts.Default);
            options.Conventions.AuthorizePage("/CustomerGroupByLists/Index", MdmServicePermissions.CustomerGroupByLists.Default);
            options.Conventions.AuthorizePage("/CustomerGroupByGeos/Index", MdmServicePermissions.CustomerGroupByGeos.Default);
            options.Conventions.AuthorizePage("/CustomerAssignments/Index", MdmServicePermissions.CustomerAssignments.Default);
            options.Conventions.AuthorizePage("/Holidays/Index", MdmServicePermissions.Holidays.Default);
            options.Conventions.AuthorizePage("/HolidayDetails/Index", MdmServicePermissions.HolidayDetails.Default);
            options.Conventions.AuthorizePage("/Routes/Index", MdmServicePermissions.Routes.Default);
            options.Conventions.AuthorizePage("/MCPHeaders/Index", MdmServicePermissions.MCPs.Default);
            options.Conventions.AuthorizePage("/MCPDetails/Index", MdmServicePermissions.MCPs.Default);
            options.Conventions.AuthorizePage("/VisitPlans/Index", MdmServicePermissions.VisitPlans.Default);
            options.Conventions.AuthorizePage("/RouteAssignments/Index", MdmServicePermissions.RouteAssignments.Default);
            options.Conventions.AuthorizePage("/EmployeeProfiles/Index", MdmServicePermissions.EmployeeProfiles.Default);
            options.Conventions.AuthorizePage("/EmployeeImages/Index", MdmServicePermissions.EmployeeProfiles.Default);
            options.Conventions.AuthorizePage("/EmployeeAttachments/Index", MdmServicePermissions.EmployeeProfiles.Default);
            options.Conventions.AuthorizePage("/PriceUpdateDetails/Index", MdmServicePermissions.PriceUpdateDetails.Default);
            options.Conventions.AuthorizePage("/CusAttributeValues/Index", MdmServicePermissions.CustomerAttributes.Default);
            options.Conventions.AuthorizePage("/Customers/Index", MdmServicePermissions.Customers.Default);
            options.Conventions.AuthorizePage("/CustomerContacts/Index", MdmServicePermissions.Customers.Default);
            options.Conventions.AuthorizePage("/CustomerAttachments/Index", MdmServicePermissions.Customers.Default);
            options.Conventions.AuthorizePage("/Vendors/Index", MdmServicePermissions.Vendors.Default);
            options.Conventions.AuthorizePage("/CompanyIdentityUserAssignments/Index", MdmServicePermissions.CompanyIdentityUserAssignments.Default);
            options.Conventions.AuthorizePage("/ItemAttributes/Index", MdmServicePermissions.ItemAttributes.Default);
            options.Conventions.AuthorizePage("/ItemAttributeValues/Index", MdmServicePermissions.ItemAttributeValues.Default);
            options.Conventions.AuthorizePage("/ItemGroupAttributes/Index", MdmServicePermissions.ItemGroups.Default);
            options.Conventions.AuthorizePage("/Items/Index", MdmServicePermissions.Items.Default);
            options.Conventions.AuthorizePage("/ItemImages/Index", MdmServicePermissions.Items.Default);
            options.Conventions.AuthorizePage("/ItemAttachments/Index", MdmServicePermissions.Items.Default);
            options.Conventions.AuthorizePage("/ItemGroupLists/Index", MdmServicePermissions.ItemGroups.Default);
            options.Conventions.AuthorizePage("/CustomerGroupLists/Index", MdmServicePermissions.CustomerGroups.Default);
            options.Conventions.AuthorizePage("/CustomerGroupGeos/Index", MdmServicePermissions.CustomerGroups.Default);
            options.Conventions.AuthorizePage("/CustomerAttributeValues/Index", MdmServicePermissions.CustomerAttributes.Default);
            options.Conventions.AuthorizePage("/CustomerGroupAttributes/Index", MdmServicePermissions.CustomerGroups.Default);
        });
    }
}