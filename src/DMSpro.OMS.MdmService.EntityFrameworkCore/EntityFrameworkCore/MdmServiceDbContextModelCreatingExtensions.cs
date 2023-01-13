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
using Volo.Abp.EntityFrameworkCore.Modeling;
using DMSpro.OMS.MdmService.Companies;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.EntityFrameworkCore;

public static class MdmServiceDbContextModelCreatingExtensions
{
    public static void ConfigureMdmService(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(MdmServiceConsts.DbTablePrefix + "YourEntities", MdmServiceConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Street>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Streets", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Street.TenantId));
        b.Property(x => x.Name).HasColumnName(nameof(Street.Name)).IsRequired().HasMaxLength(StreetConsts.NameMaxLength);
    });
        builder.Entity<Currency>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Currencies", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Currency.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(Currency.Code)).IsRequired().HasMaxLength(CurrencyConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(Currency.Name)).HasMaxLength(CurrencyConsts.NameMaxLength);
    });

        builder.Entity<GeoMaster>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "GeoMasters", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(GeoMaster.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(GeoMaster.Code)).IsRequired();
        b.Property(x => x.ERPCode).HasColumnName(nameof(GeoMaster.ERPCode));
        b.Property(x => x.Name).HasColumnName(nameof(GeoMaster.Name)).IsRequired().HasMaxLength(GeoMasterConsts.NameMaxLength);
        b.Property(x => x.Level).HasColumnName(nameof(GeoMaster.Level));
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<DimensionMeasurement>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "DimensionMeasurements", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(DimensionMeasurement.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(DimensionMeasurement.Code)).IsRequired().HasMaxLength(DimensionMeasurementConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(DimensionMeasurement.Name)).IsRequired().HasMaxLength(DimensionMeasurementConsts.NameMaxLength);
        b.Property(x => x.Value).HasColumnName(nameof(DimensionMeasurement.Value)).IsRequired();
    });
        builder.Entity<WeightMeasurement>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "WeightMeasurements", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(WeightMeasurement.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(WeightMeasurement.Code)).IsRequired();
        b.Property(x => x.Name).HasColumnName(nameof(WeightMeasurement.Name)).IsRequired().HasMaxLength(WeightMeasurementConsts.NameMaxLength);
        b.Property(x => x.Value).HasColumnName(nameof(WeightMeasurement.Value)).IsRequired();
    });
        builder.Entity<VAT>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "VATs", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(VAT.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(VAT.Code)).IsRequired().HasMaxLength(VATConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(VAT.Name)).IsRequired().HasMaxLength(VATConsts.NameMaxLength);
        b.Property(x => x.Rate).HasColumnName(nameof(VAT.Rate)).IsRequired().HasMaxLength(VATConsts.RateMaxLength);
    });
        builder.Entity<SalesChannel>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "SalesChannels", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(SalesChannel.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(SalesChannel.Code)).IsRequired().HasMaxLength(SalesChannelConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(SalesChannel.Name)).IsRequired().HasMaxLength(SalesChannelConsts.NameMaxLength);
        b.Property(x => x.Description).HasColumnName(nameof(SalesChannel.Description));
        b.Property(x => x.Active).HasColumnName(nameof(SalesChannel.Active));
    });
        builder.Entity<UOM>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "UOMs", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(UOM.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(UOM.Code)).IsRequired().HasMaxLength(UOMConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(UOM.Name)).IsRequired().HasMaxLength(UOMConsts.NameMaxLength);
    });
        builder.Entity<UOMGroup>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "UOMGroups", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(UOMGroup.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(UOMGroup.Code)).IsRequired().HasMaxLength(UOMGroupConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(UOMGroup.Name)).IsRequired().HasMaxLength(UOMGroupConsts.NameMaxLength);
    });
        builder.Entity<UOMGroupDetail>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "UOMGroupDetails", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(UOMGroupDetail.TenantId));
        b.Property(x => x.AltQty).HasColumnName(nameof(UOMGroupDetail.AltQty));
        b.Property(x => x.BaseQty).HasColumnName(nameof(UOMGroupDetail.BaseQty));
        b.Property(x => x.Active).HasColumnName(nameof(UOMGroupDetail.Active));
        b.HasOne<UOMGroup>().WithMany().IsRequired().HasForeignKey(x => x.UOMGroupId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<UOM>().WithMany().IsRequired().HasForeignKey(x => x.AltUOMId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<UOM>().WithMany().IsRequired().HasForeignKey(x => x.BaseUOMId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<WorkingPosition>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "WorkingPositions", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(WorkingPosition.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(WorkingPosition.Code)).IsRequired().HasMaxLength(WorkingPositionConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(WorkingPosition.Name)).IsRequired();
        b.Property(x => x.Description).HasColumnName(nameof(WorkingPosition.Description));
        b.Property(x => x.Active).HasColumnName(nameof(WorkingPosition.Active));
    });

        builder.Entity<EmployeeProfile>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "EmployeeProfiles", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(EmployeeProfile.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(EmployeeProfile.Code)).IsRequired().HasMaxLength(EmployeeProfileConsts.CodeMaxLength);
        b.Property(x => x.ERPCode).HasColumnName(nameof(EmployeeProfile.ERPCode));
        b.Property(x => x.FirstName).HasColumnName(nameof(EmployeeProfile.FirstName)).IsRequired().HasMaxLength(EmployeeProfileConsts.FirstNameMaxLength);
        b.Property(x => x.LastName).HasColumnName(nameof(EmployeeProfile.LastName));
        b.Property(x => x.DateOfBirth).HasColumnName(nameof(EmployeeProfile.DateOfBirth));
        b.Property(x => x.IdCardNumber).HasColumnName(nameof(EmployeeProfile.IdCardNumber));
        b.Property(x => x.Email).HasColumnName(nameof(EmployeeProfile.Email));
        b.Property(x => x.Phone).HasColumnName(nameof(EmployeeProfile.Phone));
        b.Property(x => x.Address).HasColumnName(nameof(EmployeeProfile.Address));
        b.Property(x => x.Active).HasColumnName(nameof(EmployeeProfile.Active));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(EmployeeProfile.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(EmployeeProfile.EndDate));
        b.Property(x => x.IdentityUserId).HasColumnName(nameof(EmployeeProfile.IdentityUserId));
        b.HasOne<WorkingPosition>().WithMany().HasForeignKey(x => x.WorkingPositionId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<SystemData>().WithMany().HasForeignKey(x => x.EmployeeTypeId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<PriceList>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "PriceLists", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(PriceList.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(PriceList.Code)).IsRequired().HasMaxLength(PriceListConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(PriceList.Name));
        b.Property(x => x.Active).HasColumnName(nameof(PriceList.Active));
        b.Property(x => x.ArithmeticOperation).HasColumnName(nameof(PriceList.ArithmeticOperation));
        b.Property(x => x.ArithmeticFactor).HasColumnName(nameof(PriceList.ArithmeticFactor));
        b.Property(x => x.ArithmeticFactorType).HasColumnName(nameof(PriceList.ArithmeticFactorType));
        b.Property(x => x.IsFirstPriceList).HasColumnName(nameof(PriceList.IsFirstPriceList));
        b.HasOne<PriceList>().WithMany().HasForeignKey(x => x.BasePriceListId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<PriceUpdate>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "PriceUpdates", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(PriceUpdate.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(PriceUpdate.Code)).IsRequired().HasMaxLength(PriceUpdateConsts.CodeMaxLength);
        b.Property(x => x.Description).HasColumnName(nameof(PriceUpdate.Description));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(PriceUpdate.EffectiveDate));
        b.Property(x => x.Status).HasColumnName(nameof(PriceUpdate.Status));
        b.Property(x => x.UpdateStatusDate).HasColumnName(nameof(PriceUpdate.UpdateStatusDate));
        b.HasOne<PriceList>().WithMany().IsRequired().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<PriceUpdateDetail>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "PriceUpdateDetails", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(PriceUpdateDetail.TenantId));
        b.Property(x => x.PriceBeforeUpdate).HasColumnName(nameof(PriceUpdateDetail.PriceBeforeUpdate));
        b.Property(x => x.NewPrice).HasColumnName(nameof(PriceUpdateDetail.NewPrice));
        b.Property(x => x.UpdatedDate).HasColumnName(nameof(PriceUpdateDetail.UpdatedDate));
        b.HasOne<PriceUpdate>().WithMany().IsRequired().HasForeignKey(x => x.PriceUpdateId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<PriceListDetail>().WithMany().IsRequired().HasForeignKey(x => x.PriceListDetailId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<PricelistAssignment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "PricelistAssignments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(PricelistAssignment.TenantId));
        b.Property(x => x.Description).HasColumnName(nameof(PricelistAssignment.Description));
        b.HasOne<PriceList>().WithMany().IsRequired().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CustomerGroup>().WithMany().IsRequired().HasForeignKey(x => x.CustomerGroupId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<EmployeeImage>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "EmployeeImages", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(EmployeeImage.TenantId));
        b.Property(x => x.Description).HasColumnName(nameof(EmployeeImage.Description));
        b.Property(x => x.url).HasColumnName(nameof(EmployeeImage.url)).IsRequired();
        b.Property(x => x.Active).HasColumnName(nameof(EmployeeImage.Active));
        b.Property(x => x.IsAvatar).HasColumnName(nameof(EmployeeImage.IsAvatar));
        b.HasOne<EmployeeProfile>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeProfileId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<EmployeeAttachment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "EmployeeAttachments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(EmployeeAttachment.TenantId));
        b.Property(x => x.url).HasColumnName(nameof(EmployeeAttachment.url)).IsRequired();
        b.Property(x => x.Description).HasColumnName(nameof(EmployeeAttachment.Description));
        b.Property(x => x.Active).HasColumnName(nameof(EmployeeAttachment.Active));
        b.HasOne<EmployeeProfile>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeProfileId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<SalesOrgHeader>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "SalesOrgHeaders", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(SalesOrgHeader.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(SalesOrgHeader.Code)).IsRequired().HasMaxLength(SalesOrgHeaderConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(SalesOrgHeader.Name));
        b.Property(x => x.Active).HasColumnName(nameof(SalesOrgHeader.Active));
    });

        builder.Entity<SalesOrgEmpAssignment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "SalesOrgEmpAssignments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(SalesOrgEmpAssignment.TenantId));
        b.Property(x => x.IsBase).HasColumnName(nameof(SalesOrgEmpAssignment.IsBase));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(SalesOrgEmpAssignment.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(SalesOrgEmpAssignment.EndDate));
        b.HasOne<SalesOrgHierarchy>().WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHierarchyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<EmployeeProfile>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeProfileId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<SalesOrgHierarchy>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "SalesOrgHierarchies", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(SalesOrgHierarchy.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(SalesOrgHierarchy.Code)).IsRequired().HasMaxLength(SalesOrgHierarchyConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(SalesOrgHierarchy.Name));
        b.Property(x => x.Level).HasColumnName(nameof(SalesOrgHierarchy.Level)).HasMaxLength(SalesOrgHierarchyConsts.LevelMaxLength);
        b.Property(x => x.IsRoute).HasColumnName(nameof(SalesOrgHierarchy.IsRoute));
        b.Property(x => x.IsSellingZone).HasColumnName(nameof(SalesOrgHierarchy.IsSellingZone));
        b.Property(x => x.HierarchyCode).HasColumnName(nameof(SalesOrgHierarchy.HierarchyCode)).IsRequired();
        b.Property(x => x.Active).HasColumnName(nameof(SalesOrgHierarchy.Active));
        b.HasOne<SalesOrgHeader>().WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHeaderId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<SalesOrgHierarchy>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<EmployeeInZone>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "EmployeeInZones", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(EmployeeInZone.TenantId));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(EmployeeInZone.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(EmployeeInZone.EndDate));
        b.HasOne<SalesOrgHierarchy>().WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHierarchyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<EmployeeProfile>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<CustomerAttribute>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerAttributes", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerAttribute.TenantId));
        b.Property(x => x.AttrNo).HasColumnName(nameof(CustomerAttribute.AttrNo)).HasMaxLength(CustomerAttributeConsts.AttrNoMaxLength);
        b.Property(x => x.AttrName).HasColumnName(nameof(CustomerAttribute.AttrName)).IsRequired().HasMaxLength(CustomerAttributeConsts.AttrNameMaxLength);
        b.Property(x => x.HierarchyLevel).HasColumnName(nameof(CustomerAttribute.HierarchyLevel)).HasMaxLength(CustomerAttributeConsts.HierarchyLevelMaxLength);
        b.Property(x => x.Active).HasColumnName(nameof(CustomerAttribute.Active));
    });

        builder.Entity<CusAttributeValue>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CusAttributeValues", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CusAttributeValue.TenantId));
        b.Property(x => x.AttrValName).HasColumnName(nameof(CusAttributeValue.AttrValName)).IsRequired().HasMaxLength(CusAttributeValueConsts.AttrValNameMaxLength);
        b.HasOne<CustomerAttribute>().WithMany().IsRequired().HasForeignKey(x => x.CustomerAttributeId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.ParentCusAttributeValueId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<CustomerInZone>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerInZones", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerInZone.TenantId));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(CustomerInZone.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(CustomerInZone.EndDate));
        b.HasOne<SalesOrgHierarchy>().WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHierarchyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<CustomerContact>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerContacts", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerContact.TenantId));
        b.Property(x => x.Title).HasColumnName(nameof(CustomerContact.Title));
        b.Property(x => x.FirstName).HasColumnName(nameof(CustomerContact.FirstName));
        b.Property(x => x.LastName).HasColumnName(nameof(CustomerContact.LastName));
        b.Property(x => x.Gender).HasColumnName(nameof(CustomerContact.Gender));
        b.Property(x => x.DateOfBirth).HasColumnName(nameof(CustomerContact.DateOfBirth));
        b.Property(x => x.Phone).HasColumnName(nameof(CustomerContact.Phone));
        b.Property(x => x.Email).HasColumnName(nameof(CustomerContact.Email));
        b.Property(x => x.Address).HasColumnName(nameof(CustomerContact.Address));
        b.Property(x => x.IdentityNumber).HasColumnName(nameof(CustomerContact.IdentityNumber));
        b.Property(x => x.BankName).HasColumnName(nameof(CustomerContact.BankName));
        b.Property(x => x.BankAccName).HasColumnName(nameof(CustomerContact.BankAccName));
        b.Property(x => x.BankAccNumber).HasColumnName(nameof(CustomerContact.BankAccNumber));
        b.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<CustomerAttachment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerAttachments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerAttachment.TenantId));
        b.Property(x => x.url).HasColumnName(nameof(CustomerAttachment.url)).IsRequired();
        b.Property(x => x.Description).HasColumnName(nameof(CustomerAttachment.Description));
        b.Property(x => x.Active).HasColumnName(nameof(CustomerAttachment.Active));
        b.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<CustomerAssignment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerAssignments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerAssignment.TenantId));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(CustomerAssignment.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(CustomerAssignment.EndDate));
        b.HasOne<Company>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<CustomerGroupByGeo>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerGroupByGeos", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerGroupByGeo.TenantId));
        b.Property(x => x.Active).HasColumnName(nameof(CustomerGroupByGeo.Active));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(CustomerGroupByGeo.EffectiveDate));
        b.HasOne<CustomerGroup>().WithMany().IsRequired().HasForeignKey(x => x.CustomerGroupId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().IsRequired().HasForeignKey(x => x.GeoMasterId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<CustomerGroupByList>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerGroupByLists", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerGroupByList.TenantId));
        b.Property(x => x.Active).HasColumnName(nameof(CustomerGroupByList.Active));
        b.HasOne<CustomerGroup>().WithMany().IsRequired().HasForeignKey(x => x.CustomerGroupId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<CustomerGroupByAtt>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerGroupByAtts", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerGroupByAtt.TenantId));
        b.Property(x => x.ValueCode).HasColumnName(nameof(CustomerGroupByAtt.ValueCode));
        b.Property(x => x.ValueName).HasColumnName(nameof(CustomerGroupByAtt.ValueName));
        b.HasOne<CustomerGroup>().WithMany().IsRequired().HasForeignKey(x => x.CustomerGroupId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().IsRequired().HasForeignKey(x => x.CusAttributeValueId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<CustomerGroup>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerGroups", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerGroup.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(CustomerGroup.Code)).IsRequired().HasMaxLength(CustomerGroupConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(CustomerGroup.Name));
        b.Property(x => x.Active).HasColumnName(nameof(CustomerGroup.Active));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(CustomerGroup.EffectiveDate));
        b.Property(x => x.GroupBy).HasColumnName(nameof(CustomerGroup.GroupBy));
        b.Property(x => x.Status).HasColumnName(nameof(CustomerGroup.Status));
    });
        builder.Entity<Holiday>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Holidays", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Holiday.TenantId));
        b.Property(x => x.Year).HasColumnName(nameof(Holiday.Year)).IsRequired().HasMaxLength(HolidayConsts.YearMaxLength);
        b.Property(x => x.Description).HasColumnName(nameof(Holiday.Description)).IsRequired();
    });
        builder.Entity<HolidayDetail>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "HolidayDetails", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(HolidayDetail.TenantId));
        b.Property(x => x.StartDate).HasColumnName(nameof(HolidayDetail.StartDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(HolidayDetail.EndDate));
        b.Property(x => x.Description).HasColumnName(nameof(HolidayDetail.Description));
        b.HasOne<Holiday>().WithMany().IsRequired().HasForeignKey(x => x.HolidayId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<Route>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Routes", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Route.TenantId));
        b.Property(x => x.CheckIn).HasColumnName(nameof(Route.CheckIn));
        b.Property(x => x.CheckOut).HasColumnName(nameof(Route.CheckOut));
        b.Property(x => x.GPSLock).HasColumnName(nameof(Route.GPSLock));
        b.Property(x => x.OutRoute).HasColumnName(nameof(Route.OutRoute));
        b.HasOne<SystemData>().WithMany().IsRequired().HasForeignKey(x => x.RouteTypeId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemGroup>().WithMany().IsRequired().HasForeignKey(x => x.ItemGroupId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<SalesOrgHierarchy>().WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHierarchyId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<MCPDetail>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "MCPDetails", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(MCPDetail.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(MCPDetail.Code)).IsRequired().HasMaxLength(MCPDetailConsts.CodeMaxLength);
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(MCPDetail.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(MCPDetail.EndDate));
        b.Property(x => x.Distance).HasColumnName(nameof(MCPDetail.Distance));
        b.Property(x => x.VisitOrder).HasColumnName(nameof(MCPDetail.VisitOrder));
        b.Property(x => x.Monday).HasColumnName(nameof(MCPDetail.Monday));
        b.Property(x => x.Tuesday).HasColumnName(nameof(MCPDetail.Tuesday));
        b.Property(x => x.Wednesday).HasColumnName(nameof(MCPDetail.Wednesday));
        b.Property(x => x.Thursday).HasColumnName(nameof(MCPDetail.Thursday));
        b.Property(x => x.Friday).HasColumnName(nameof(MCPDetail.Friday));
        b.Property(x => x.Saturday).HasColumnName(nameof(MCPDetail.Saturday));
        b.Property(x => x.Sunday).HasColumnName(nameof(MCPDetail.Sunday));
        b.Property(x => x.Week1).HasColumnName(nameof(MCPDetail.Week1));
        b.Property(x => x.Week2).HasColumnName(nameof(MCPDetail.Week2));
        b.Property(x => x.Week3).HasColumnName(nameof(MCPDetail.Week3));
        b.Property(x => x.Week4).HasColumnName(nameof(MCPDetail.Week4));
        b.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<MCPHeader>().WithMany().IsRequired().HasForeignKey(x => x.MCPHeaderId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<RouteAssignment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "RouteAssignments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(RouteAssignment.TenantId));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(RouteAssignment.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(RouteAssignment.EndDate));
        b.HasOne<SalesOrgHierarchy>().WithMany().IsRequired().HasForeignKey(x => x.RouteId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<EmployeeProfile>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<SystemData>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "SystemDatas", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(SystemData.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(SystemData.Code)).IsRequired().HasMaxLength(SystemDataConsts.CodeMaxLength);
        b.Property(x => x.ValueCode).HasColumnName(nameof(SystemData.ValueCode)).IsRequired().HasMaxLength(SystemDataConsts.ValueCodeMaxLength);
        b.Property(x => x.ValueName).HasColumnName(nameof(SystemData.ValueName)).IsRequired().HasMaxLength(SystemDataConsts.ValueNameMaxLength);
    });

        builder.Entity<NumberingConfig>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "NumberingConfigs", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(NumberingConfig.TenantId));
        b.Property(x => x.StartNumber).HasColumnName(nameof(NumberingConfig.StartNumber));
        b.Property(x => x.Prefix).HasColumnName(nameof(NumberingConfig.Prefix));
        b.Property(x => x.Suffix).HasColumnName(nameof(NumberingConfig.Suffix));
        b.Property(x => x.Length).HasColumnName(nameof(NumberingConfig.Length));
        b.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<SystemData>().WithMany().HasForeignKey(x => x.SystemDataId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<SystemConfig>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "SystemConfigs", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(SystemConfig.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(SystemConfig.Code)).IsRequired().HasMaxLength(SystemConfigConsts.CodeMaxLength);
        b.Property(x => x.Description).HasColumnName(nameof(SystemConfig.Description)).IsRequired().HasMaxLength(SystemConfigConsts.DescriptionMaxLength);
        b.Property(x => x.Value).HasColumnName(nameof(SystemConfig.Value)).IsRequired().HasMaxLength(SystemConfigConsts.ValueMaxLength);
        b.Property(x => x.DefaultValue).HasColumnName(nameof(SystemConfig.DefaultValue)).IsRequired().HasMaxLength(SystemConfigConsts.DefaultValueMaxLength);
        b.Property(x => x.EditableByTenant).HasColumnName(nameof(SystemConfig.EditableByTenant));
        b.Property(x => x.ControlType).HasColumnName(nameof(SystemConfig.ControlType));
        b.Property(x => x.DataSource).HasColumnName(nameof(SystemConfig.DataSource));
    });

        builder.Entity<Customer>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Customers", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Customer.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(Customer.Code)).IsRequired().HasMaxLength(CustomerConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(Customer.Name)).HasMaxLength(CustomerConsts.NameMaxLength);
        b.Property(x => x.Phone1).HasColumnName(nameof(Customer.Phone1)).HasMaxLength(CustomerConsts.Phone1MaxLength);
        b.Property(x => x.Phone2).HasColumnName(nameof(Customer.Phone2)).HasMaxLength(CustomerConsts.Phone2MaxLength);
        b.Property(x => x.erpCode).HasColumnName(nameof(Customer.erpCode)).HasMaxLength(CustomerConsts.erpCodeMaxLength);
        b.Property(x => x.License).HasColumnName(nameof(Customer.License)).HasMaxLength(CustomerConsts.LicenseMaxLength);
        b.Property(x => x.TaxCode).HasColumnName(nameof(Customer.TaxCode)).HasMaxLength(CustomerConsts.TaxCodeMaxLength);
        b.Property(x => x.vatName).HasColumnName(nameof(Customer.vatName)).HasMaxLength(CustomerConsts.vatNameMaxLength);
        b.Property(x => x.vatAddress).HasColumnName(nameof(Customer.vatAddress)).HasMaxLength(CustomerConsts.vatAddressMaxLength);
        b.Property(x => x.Active).HasColumnName(nameof(Customer.Active));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(Customer.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(Customer.EndDate));
        b.Property(x => x.CreditLimit).HasColumnName(nameof(Customer.CreditLimit));
        b.Property(x => x.IsCompany).HasColumnName(nameof(Customer.IsCompany));
        b.Property(x => x.WarehouseId).HasColumnName(nameof(Customer.WarehouseId));
        b.Property(x => x.Street).HasColumnName(nameof(Customer.Street)).HasMaxLength(CustomerConsts.StreetMaxLength);
        b.Property(x => x.Address).HasColumnName(nameof(Customer.Address)).HasMaxLength(CustomerConsts.AddressMaxLength);
        b.Property(x => x.Latitude).HasColumnName(nameof(Customer.Latitude)).HasMaxLength(CustomerConsts.LatitudeMaxLength);
        b.Property(x => x.Longitude).HasColumnName(nameof(Customer.Longitude)).HasMaxLength(CustomerConsts.LongitudeMaxLength);
        b.Property(x => x.SFACustomerCode).HasColumnName(nameof(Customer.SFACustomerCode)).IsRequired().HasMaxLength(CustomerConsts.SFACustomerCodeMaxLength);
        b.Property(x => x.LastOrderDate).HasColumnName(nameof(Customer.LastOrderDate));
        b.HasOne<SystemData>().WithMany().HasForeignKey(x => x.PaymentTermId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Company>().WithMany().HasForeignKey(x => x.LinkedCompanyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<PriceList>().WithMany().IsRequired().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster0Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster1Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster2Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster3Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster4Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute0Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute1Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute2Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute3Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute4Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute5Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute6Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute7Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute8Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute9Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute10Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute11Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute12Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute13Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute14Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute15Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute16Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute1I7d).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute18Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute19Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Customer>().WithMany().HasForeignKey(x => x.PaymentId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<CompanyIdentityUserAssignment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CompanyIdentityUserAssignments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CompanyIdentityUserAssignment.TenantId));
        b.Property(x => x.IdentityUserId).HasColumnName(nameof(CompanyIdentityUserAssignment.IdentityUserId));
        b.HasOne<Company>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<CompanyInZone>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CompanyInZones", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CompanyInZone.TenantId));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(CompanyInZone.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(CompanyInZone.EndDate));
        b.Property(x => x.IsBase).HasColumnName(nameof(CompanyInZone.IsBase));
        b.HasOne<SalesOrgHierarchy>().WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHierarchyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Company>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<ItemGroup>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemGroups", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(ItemGroup.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(ItemGroup.Code)).IsRequired().HasMaxLength(ItemGroupConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(ItemGroup.Name)).IsRequired();
        b.Property(x => x.Description).HasColumnName(nameof(ItemGroup.Description)).HasMaxLength(ItemGroupConsts.DescriptionMaxLength);
        b.Property(x => x.Type).HasColumnName(nameof(ItemGroup.Type));
        b.Property(x => x.Status).HasColumnName(nameof(ItemGroup.Status));
    });

        builder.Entity<ItemAttributeValue>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemAttributeValues", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(ItemAttributeValue.TenantId));
        b.Property(x => x.AttrValName).HasColumnName(nameof(ItemAttributeValue.AttrValName)).IsRequired().HasMaxLength(ItemAttributeValueConsts.AttrValNameMaxLength);
        b.HasOne<ItemAttribute>().WithMany().IsRequired().HasForeignKey(x => x.ItemAttributeId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<ItemAttribute>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemAttributes", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(ItemAttribute.TenantId));
        b.Property(x => x.AttrNo).HasColumnName(nameof(ItemAttribute.AttrNo)).HasMaxLength(ItemAttributeConsts.AttrNoMaxLength);
        b.Property(x => x.AttrName).HasColumnName(nameof(ItemAttribute.AttrName)).IsRequired().HasMaxLength(ItemAttributeConsts.AttrNameMaxLength);
        b.Property(x => x.HierarchyLevel).HasColumnName(nameof(ItemAttribute.HierarchyLevel));
        b.Property(x => x.Active).HasColumnName(nameof(ItemAttribute.Active));
        b.Property(x => x.IsSellingCategory).HasColumnName(nameof(ItemAttribute.IsSellingCategory));
    });

        builder.Entity<ItemGroupAttribute>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemGroupAttributes", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(ItemGroupAttribute.TenantId));
        b.Property(x => x.dummy).HasColumnName(nameof(ItemGroupAttribute.dummy)).IsRequired().HasMaxLength(ItemGroupAttributeConsts.dummyMaxLength);
        b.HasOne<ItemGroup>().WithMany().IsRequired().HasForeignKey(x => x.ItemGroupId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr0Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr1Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr2Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr3Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr4Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr6Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr7Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr8Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr9Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr10Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr11Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr12Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr13Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr14Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr15Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr16Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr17Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr18Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr19Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr5Id).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<Item>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Items", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Item.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(Item.Code)).IsRequired().HasMaxLength(ItemConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(Item.Name)).IsRequired().HasMaxLength(ItemConsts.NameMaxLength);
        b.Property(x => x.ShortName).HasColumnName(nameof(Item.ShortName)).HasMaxLength(ItemConsts.ShortNameMaxLength);
        b.Property(x => x.ERPCode).HasColumnName(nameof(Item.ERPCode)).HasMaxLength(ItemConsts.ERPCodeMaxLength);
        b.Property(x => x.Barcode).HasColumnName(nameof(Item.Barcode)).HasMaxLength(ItemConsts.BarcodeMaxLength);
        b.Property(x => x.IsPurchasable).HasColumnName(nameof(Item.IsPurchasable));
        b.Property(x => x.IsSaleable).HasColumnName(nameof(Item.IsSaleable));
        b.Property(x => x.IsInventoriable).HasColumnName(nameof(Item.IsInventoriable));
        b.Property(x => x.BasePrice).HasColumnName(nameof(Item.BasePrice));
        b.Property(x => x.Active).HasColumnName(nameof(Item.Active));
        b.Property(x => x.ManageItemBy).HasColumnName(nameof(Item.ManageItemBy));
        b.Property(x => x.ExpiredType).HasColumnName(nameof(Item.ExpiredType));
        b.Property(x => x.ExpiredValue).HasColumnName(nameof(Item.ExpiredValue));
        b.Property(x => x.IssueMethod).HasColumnName(nameof(Item.IssueMethod));
        b.Property(x => x.CanUpdate).HasColumnName(nameof(Item.CanUpdate));
        b.HasOne<SystemData>().WithMany().IsRequired().HasForeignKey(x => x.ItemTypeId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<VAT>().WithMany().IsRequired().HasForeignKey(x => x.VatId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<UOMGroup>().WithMany().IsRequired().HasForeignKey(x => x.UomGroupId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<UOMGroupDetail>().WithMany().IsRequired().HasForeignKey(x => x.InventoryUOMId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<UOMGroupDetail>().WithMany().IsRequired().HasForeignKey(x => x.PurUOMId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<UOMGroupDetail>().WithMany().IsRequired().HasForeignKey(x => x.SalesUOMId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr0Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr1Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr2Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr3Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr4Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr5Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr6Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr7Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr8Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr9Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr10Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr11Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr12Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr13Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr14Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr15Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr16Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr17Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr18Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemAttributeValue>().WithMany().HasForeignKey(x => x.Attr19Id).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<PriceListDetail>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "PriceListDetails", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(PriceListDetail.TenantId));
        b.Property(x => x.Price).HasColumnName(nameof(PriceListDetail.Price));
        b.Property(x => x.BasedOnPrice).HasColumnName(nameof(PriceListDetail.BasedOnPrice));
        b.Property(x => x.Description).HasColumnName(nameof(PriceListDetail.Description)).IsRequired();
        b.HasOne<PriceList>().WithMany().IsRequired().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<UOM>().WithMany().IsRequired().HasForeignKey(x => x.UOMId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Item>().WithMany().IsRequired().HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<ItemImage>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemImages", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(ItemImage.TenantId));
        b.Property(x => x.Description).HasColumnName(nameof(ItemImage.Description)).HasMaxLength(ItemImageConsts.DescriptionMaxLength);
        b.Property(x => x.Url).HasColumnName(nameof(ItemImage.Url)).IsRequired().HasMaxLength(ItemImageConsts.UrlMaxLength);
        b.Property(x => x.Active).HasColumnName(nameof(ItemImage.Active));
        b.Property(x => x.DisplayOrder).HasColumnName(nameof(ItemImage.DisplayOrder));
        b.HasOne<Item>().WithMany().IsRequired().HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<ItemAttachment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemAttachments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(ItemAttachment.TenantId));
        b.Property(x => x.Description).HasColumnName(nameof(ItemAttachment.Description)).HasMaxLength(ItemAttachmentConsts.DescriptionMaxLength);
        b.Property(x => x.Url).HasColumnName(nameof(ItemAttachment.Url)).IsRequired().HasMaxLength(ItemAttachmentConsts.UrlMaxLength);
        b.Property(x => x.Active).HasColumnName(nameof(ItemAttachment.Active));
        b.HasOne<Item>().WithMany().IsRequired().HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<ItemGroupList>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemGroupLists", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(ItemGroupList.TenantId));
        b.Property(x => x.Rate).HasColumnName(nameof(ItemGroupList.Rate));
        b.Property(x => x.Price).HasColumnName(nameof(ItemGroupList.Price));
        b.HasOne<ItemGroup>().WithMany().IsRequired().HasForeignKey(x => x.ItemGroupId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Item>().WithMany().IsRequired().HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<UOM>().WithMany().IsRequired().HasForeignKey(x => x.UomId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<MCPHeader>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "MCPHeaders", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(MCPHeader.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(MCPHeader.Code)).IsRequired().HasMaxLength(MCPHeaderConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(MCPHeader.Name));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(MCPHeader.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(MCPHeader.EndDate));
        b.HasOne<SalesOrgHierarchy>().WithMany().IsRequired().HasForeignKey(x => x.RouteId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Company>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemGroup>().WithMany().HasForeignKey(x => x.ItemGroupId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<Company>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Companies", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Company.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(Company.Code)).IsRequired().HasMaxLength(CompanyConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(Company.Name)).IsRequired().HasMaxLength(CompanyConsts.NameMaxLength);
        b.Property(x => x.Street).HasColumnName(nameof(Company.Street)).IsRequired();
        b.Property(x => x.Address).HasColumnName(nameof(Company.Address)).IsRequired().HasMaxLength(CompanyConsts.AddressMaxLength);
        b.Property(x => x.Phone).HasColumnName(nameof(Company.Phone)).HasMaxLength(CompanyConsts.PhoneMaxLength);
        b.Property(x => x.License).HasColumnName(nameof(Company.License)).HasMaxLength(CompanyConsts.LicenseMaxLength);
        b.Property(x => x.TaxCode).HasColumnName(nameof(Company.TaxCode)).HasMaxLength(CompanyConsts.TaxCodeMaxLength);
        b.Property(x => x.VATName).HasColumnName(nameof(Company.VATName));
        b.Property(x => x.VATAddress).HasColumnName(nameof(Company.VATAddress));
        b.Property(x => x.ERPCode).HasColumnName(nameof(Company.ERPCode)).HasMaxLength(CompanyConsts.ERPCodeMaxLength);
        b.Property(x => x.Active).HasColumnName(nameof(Company.Active));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(Company.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(Company.EndDate));
        b.Property(x => x.IsHO).HasColumnName(nameof(Company.IsHO));
        b.Property(x => x.Latitude).HasColumnName(nameof(Company.Latitude));
        b.Property(x => x.Longitude).HasColumnName(nameof(Company.Longitude));
        b.Property(x => x.ContactName).HasColumnName(nameof(Company.ContactName));
        b.Property(x => x.ContactPhone).HasColumnName(nameof(Company.ContactPhone));
        b.HasOne<Company>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoLevel0Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoLevel1Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoLevel2Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoLevel3Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoLevel4Id).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<VisitPlan>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "VisitPlans", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(VisitPlan.TenantId));
        b.Property(x => x.DateVisit).HasColumnName(nameof(VisitPlan.DateVisit));
        b.Property(x => x.Distance).HasColumnName(nameof(VisitPlan.Distance));
        b.Property(x => x.VisitOrder).HasColumnName(nameof(VisitPlan.VisitOrder));
        b.Property(x => x.DayOfWeek).HasColumnName(nameof(VisitPlan.DayOfWeek));
        b.Property(x => x.Week).HasColumnName(nameof(VisitPlan.Week));
        b.Property(x => x.Month).HasColumnName(nameof(VisitPlan.Month));
        b.Property(x => x.Year).HasColumnName(nameof(VisitPlan.Year));
        b.HasOne<MCPDetail>().WithMany().IsRequired().HasForeignKey(x => x.MCPDetailId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<SalesOrgHierarchy>().WithMany().IsRequired().HasForeignKey(x => x.RouteId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Company>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<ItemGroup>().WithMany().HasForeignKey(x => x.ItemGroupId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<Vendor>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Vendors", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Vendor.TenantId));
        b.Property(x => x.Code).HasColumnName(nameof(Vendor.Code)).IsRequired().HasMaxLength(VendorConsts.CodeMaxLength);
        b.Property(x => x.Name).HasColumnName(nameof(Vendor.Name)).IsRequired().HasMaxLength(VendorConsts.NameMaxLength);
        b.Property(x => x.ShortName).HasColumnName(nameof(Vendor.ShortName)).IsRequired().HasMaxLength(VendorConsts.ShortNameMaxLength);
        b.Property(x => x.Phone1).HasColumnName(nameof(Vendor.Phone1));
        b.Property(x => x.Phone2).HasColumnName(nameof(Vendor.Phone2));
        b.Property(x => x.ERPCode).HasColumnName(nameof(Vendor.ERPCode));
        b.Property(x => x.Active).HasColumnName(nameof(Vendor.Active));
        b.Property(x => x.EndDate).HasColumnName(nameof(Vendor.EndDate));
        b.Property(x => x.LinkedCompany).HasColumnName(nameof(Vendor.LinkedCompany)).HasMaxLength(VendorConsts.LinkedCompanyMaxLength);
        b.Property(x => x.WarehouseId).HasColumnName(nameof(Vendor.WarehouseId));
        b.Property(x => x.Street).HasColumnName(nameof(Vendor.Street));
        b.Property(x => x.Address).HasColumnName(nameof(Vendor.Address));
        b.Property(x => x.Latitude).HasColumnName(nameof(Vendor.Latitude));
        b.Property(x => x.Longitude).HasColumnName(nameof(Vendor.Longitude));
        b.HasOne<PriceList>().WithMany().IsRequired().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster0Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster1Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster2Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster3Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<GeoMaster>().WithMany().HasForeignKey(x => x.GeoMaster4Id).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Company>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
    });
    }
}