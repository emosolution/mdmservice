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
            b.Property(x => x.ERPCode).HasColumnName(nameof(GeoMaster.ERPCode)).HasMaxLength(GeoMasterConsts.ERPCodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(GeoMaster.Name)).IsRequired().HasMaxLength(GeoMasterConsts.NameMaxLength);
            b.Property(x => x.Level).HasColumnName(nameof(GeoMaster.Level));
            b.HasOne<GeoMaster>(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<DimensionMeasurement>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "DimensionMeasurements", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(DimensionMeasurement.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(DimensionMeasurement.Code)).IsRequired().HasMaxLength(DimensionMeasurementConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(DimensionMeasurement.Name)).HasMaxLength(DimensionMeasurementConsts.NameMaxLength);
            b.Property(x => x.Value).HasColumnType("decimal(19,2)").HasColumnName(nameof(DimensionMeasurement.Value));
        });
        builder.Entity<WeightMeasurement>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "WeightMeasurements", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(WeightMeasurement.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(WeightMeasurement.Code)).IsRequired();
            b.Property(x => x.Name).HasColumnName(nameof(WeightMeasurement.Name)).HasMaxLength(WeightMeasurementConsts.NameMaxLength);
            b.Property(x => x.Value).HasColumnType("decimal(19,2)").HasColumnName(nameof(WeightMeasurement.Value));
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
            b.HasMany(x => x.Details).WithOne(x => x.UOMGroup).HasForeignKey(x => x.UOMGroupId).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<UOMGroupDetail>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "UOMGroupDetails", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(UOMGroupDetail.TenantId));
            b.Property(x => x.AltQty).HasColumnName(nameof(UOMGroupDetail.AltQty));
            b.Property(x => x.BaseQty).HasColumnName(nameof(UOMGroupDetail.BaseQty));
            b.Property(x => x.Active).HasColumnName(nameof(UOMGroupDetail.Active));
            b.HasOne<UOMGroup>(x => x.UOMGroup).WithMany(x => x.Details).IsRequired().HasForeignKey(x => x.UOMGroupId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<UOM>(x => x.AltUOM).WithMany().IsRequired().HasForeignKey(x => x.AltUOMId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<UOM>(x => x.BaseUOM).WithMany().IsRequired().HasForeignKey(x => x.BaseUOMId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<WorkingPosition>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "WorkingPositions", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(WorkingPosition.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(WorkingPosition.Code)).IsRequired().HasMaxLength(WorkingPositionConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(WorkingPosition.Name)).HasMaxLength(WorkingPositionConsts.NameMaxLength);
            b.Property(x => x.Description).HasColumnName(nameof(WorkingPosition.Description)).HasMaxLength(WorkingPositionConsts.DescriptionMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(WorkingPosition.Active));
        });
        builder.Entity<EmployeeProfile>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "EmployeeProfiles", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(EmployeeProfile.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(EmployeeProfile.Code)).IsRequired().HasMaxLength(EmployeeProfileConsts.CodeMaxLength);
            b.Property(x => x.ERPCode).HasColumnName(nameof(EmployeeProfile.ERPCode)).HasMaxLength(EmployeeProfileConsts.ERPCodeMaxLength);
            b.Property(x => x.FirstName).HasColumnName(nameof(EmployeeProfile.FirstName)).IsRequired().HasMaxLength(EmployeeProfileConsts.FirstNameMaxLength);
            b.Property(x => x.LastName).HasColumnName(nameof(EmployeeProfile.LastName)).HasMaxLength(EmployeeProfileConsts.LastNameMaxLength);
            b.Property(x => x.DateOfBirth).HasColumnName(nameof(EmployeeProfile.DateOfBirth));
            b.Property(x => x.IdCardNumber).HasColumnName(nameof(EmployeeProfile.IdCardNumber)).HasMaxLength(EmployeeProfileConsts.IdCardNumberMaxLength);
            b.Property(x => x.Email).HasColumnName(nameof(EmployeeProfile.Email)).HasMaxLength(EmployeeProfileConsts.EmailMaxLength);
            b.Property(x => x.Phone).HasColumnName(nameof(EmployeeProfile.Phone)).HasMaxLength(EmployeeProfileConsts.PhoneMaxLength);
            b.Property(x => x.Address).HasColumnName(nameof(EmployeeProfile.Address)).HasMaxLength(EmployeeProfileConsts.AddressMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(EmployeeProfile.Active));
            b.Property(x => x.EffectiveDate).HasColumnName(nameof(EmployeeProfile.EffectiveDate));
            b.Property(x => x.EndDate).HasColumnName(nameof(EmployeeProfile.EndDate));
            b.Property(x => x.IdentityUserId).HasColumnName(nameof(EmployeeProfile.IdentityUserId));
            b.HasOne<WorkingPosition>(x => x.WorkingPosition).WithMany().HasForeignKey(x => x.WorkingPositionId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<SystemData>(x => x.EmployeeType).WithMany().HasForeignKey(x => x.EmployeeTypeId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<PriceList>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "PriceLists", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(PriceList.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(PriceList.Code)).IsRequired().HasMaxLength(PriceListConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(PriceList.Name)).HasMaxLength(PriceListConsts.NameMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(PriceList.Active));
            b.Property(x => x.ArithmeticOperation).HasColumnName(nameof(PriceList.ArithmeticOperation));
            b.Property(x => x.ArithmeticFactor).HasColumnName(nameof(PriceList.ArithmeticFactor));
            b.Property(x => x.ArithmeticFactorType).HasColumnName(nameof(PriceList.ArithmeticFactorType));
            b.Property(x => x.IsBase).HasColumnName(nameof(PriceList.IsBase));
            b.Property(x => x.IsDefaultForCustomer).HasColumnName(nameof(PriceList.IsDefaultForCustomer));
            b.Property(x => x.IsReleased).HasColumnName(nameof(PriceList.IsReleased));
            b.Property(x => x.ReleasedDate).HasColumnName(nameof(PriceList.ReleasedDate));
            b.Property(x => x.IsDefaultForVendor).HasColumnName(nameof(PriceList.IsDefaultForVendor));
            b.HasOne<PriceList>(x => x.BasePriceList).WithMany().HasForeignKey(x => x.BasePriceListId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<PriceUpdate>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "PriceUpdates", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(PriceUpdate.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(PriceUpdate.Code)).IsRequired().HasMaxLength(PriceUpdateConsts.CodeMaxLength);
            b.Property(x => x.Description).HasColumnName(nameof(PriceUpdate.Description)).HasMaxLength(PriceUpdateConsts.DescriptionMaxLength);
            b.Property(x => x.EffectiveDate).HasColumnName(nameof(PriceUpdate.EffectiveDate));
            b.Property(x => x.Status).HasColumnName(nameof(PriceUpdate.Status));
            b.Property(x => x.UpdateStatusDate).HasColumnName(nameof(PriceUpdate.UpdateStatusDate));
            b.HasOne<PriceList>(x => x.PriceList).WithMany().IsRequired().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<PriceUpdateDetail>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "PriceUpdateDetails", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(PriceUpdateDetail.TenantId));
            b.Property(x => x.PriceBeforeUpdate).HasColumnName(nameof(PriceUpdateDetail.PriceBeforeUpdate));
            b.Property(x => x.NewPrice).HasColumnName(nameof(PriceUpdateDetail.NewPrice));
            b.Property(x => x.UpdatedDate).HasColumnName(nameof(PriceUpdateDetail.UpdatedDate));
            b.HasOne<PriceUpdate>(x => x.PriceUpdate).WithMany().IsRequired().HasForeignKey(x => x.PriceUpdateId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<PriceListDetail>(x => x.PriceListDetail).WithMany().IsRequired().HasForeignKey(x => x.PriceListDetailId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<PricelistAssignment>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "PricelistAssignments", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(PricelistAssignment.TenantId));
            b.Property(x => x.Description).HasColumnName(nameof(PricelistAssignment.Description)).HasMaxLength(PricelistAssignmentConsts.DescriptionMaxLength);
            b.Property(x => x.ReleasedDate).HasColumnName(nameof(PricelistAssignment.ReleasedDate));
            b.Property(x => x.IsReleased).HasColumnName(nameof(PricelistAssignment.IsReleased));
            b.HasOne<PriceList>(x => x.PriceList).WithMany().IsRequired().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<CustomerGroup>(x => x.CustomerGroup).WithMany().IsRequired().HasForeignKey(x => x.CustomerGroupId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<SalesOrgHeader>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "SalesOrgHeaders", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(SalesOrgHeader.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(SalesOrgHeader.Code)).IsRequired().HasMaxLength(SalesOrgHeaderConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(SalesOrgHeader.Name)).HasMaxLength(SalesOrgHeaderConsts.NameMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(SalesOrgHeader.Active));
            b.Property(x => x.Status).HasColumnName(nameof(SalesOrgHeader.Status));
        });

        builder.Entity<SalesOrgEmpAssignment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "SalesOrgEmpAssignments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(SalesOrgEmpAssignment.TenantId));
        b.Property(x => x.IsBase).HasColumnName(nameof(SalesOrgEmpAssignment.IsBase));
        b.Property(x => x.EffectiveDate).HasColumnName(nameof(SalesOrgEmpAssignment.EffectiveDate));
        b.Property(x => x.EndDate).HasColumnName(nameof(SalesOrgEmpAssignment.EndDate));
        b.Property(x => x.HierarchyCode).HasColumnName(nameof(SalesOrgEmpAssignment.HierarchyCode)).HasMaxLength(SalesOrgEmpAssignmentConsts.HierarchyCodeMaxLength);
        b.HasOne<SalesOrgHierarchy>(x => x.SalesOrgHierarchy).WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHierarchyId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<EmployeeProfile>(x => x.EmployeeProfile).WithMany().IsRequired().HasForeignKey(x => x.EmployeeProfileId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<SalesOrgHierarchy>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "SalesOrgHierarchies", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(SalesOrgHierarchy.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(SalesOrgHierarchy.Code)).IsRequired().HasMaxLength(SalesOrgHierarchyConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(SalesOrgHierarchy.Name)).HasMaxLength(SalesOrgHierarchyConsts.NameMaxLength);
            b.Property(x => x.Level).HasColumnName(nameof(SalesOrgHierarchy.Level)).HasMaxLength(SalesOrgHierarchyConsts.LevelMaxLength);
            b.Property(x => x.IsRoute).HasColumnName(nameof(SalesOrgHierarchy.IsRoute));
            b.Property(x => x.IsSellingZone).HasColumnName(nameof(SalesOrgHierarchy.IsSellingZone));
            b.Property(x => x.HierarchyCode).HasColumnName(nameof(SalesOrgHierarchy.HierarchyCode)).IsRequired().HasMaxLength(SalesOrgHierarchyConsts.HierarchyCodeMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(SalesOrgHierarchy.Active));
            b.HasOne<SalesOrgHeader>(x => x.SalesOrgHeader).WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHeaderId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<SalesOrgHierarchy>(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
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
            b.HasOne<CustomerAttribute>(x => x.CustomerAttribute).WithMany().IsRequired().HasForeignKey(x => x.CustomerAttributeId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<CusAttributeValue>(x => x.Parent).WithMany().HasForeignKey(x => x.ParentCusAttributeValueId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CustomerInZone>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerInZones", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CustomerInZone.TenantId));
            b.Property(x => x.EffectiveDate).HasColumnName(nameof(CustomerInZone.EffectiveDate));
            b.Property(x => x.EndDate).HasColumnName(nameof(CustomerInZone.EndDate));
            b.HasOne<SalesOrgHierarchy>(x => x.SalesOrgHierarchy).WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHierarchyId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Customer>(x => x.Customer).WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        });
        
        builder.Entity<CustomerContact>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerContacts", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CustomerContact.TenantId));
            b.Property(x => x.Title).HasColumnName(nameof(CustomerContact.Title));
            b.Property(x => x.FirstName).HasColumnName(nameof(CustomerContact.FirstName)).HasMaxLength(CustomerContactConsts.FirstNameMaxLength);
            b.Property(x => x.LastName).HasColumnName(nameof(CustomerContact.LastName)).HasMaxLength(CustomerContactConsts.LastNameMaxLength);
            b.Property(x => x.Gender).HasColumnName(nameof(CustomerContact.Gender));
            b.Property(x => x.DateOfBirth).HasColumnName(nameof(CustomerContact.DateOfBirth));
            b.Property(x => x.Phone).HasColumnName(nameof(CustomerContact.Phone)).HasMaxLength(CustomerContactConsts.PhoneMaxLength);
            b.Property(x => x.Email).HasColumnName(nameof(CustomerContact.Email)).HasMaxLength(CustomerContactConsts.EmailMaxLength);
            b.Property(x => x.Address).HasColumnName(nameof(CustomerContact.Address)).HasMaxLength(CustomerContactConsts.AddressMaxLength);
            b.Property(x => x.IdentityNumber).HasColumnName(nameof(CustomerContact.IdentityNumber)).HasMaxLength(CustomerContactConsts.IdentityNumberMaxLength);
            b.Property(x => x.BankName).HasColumnName(nameof(CustomerContact.BankName)).HasMaxLength(CustomerContactConsts.BankNameMaxLength);
            b.Property(x => x.BankAccName).HasColumnName(nameof(CustomerContact.BankAccName)).HasMaxLength(CustomerContactConsts.BankAccNameMaxLength);
            b.Property(x => x.BankAccNumber).HasColumnName(nameof(CustomerContact.BankAccNumber)).HasMaxLength(CustomerContactConsts.BankAccNumberMaxLength);
            b.HasOne<Customer>(x => x.Customer).WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CustomerAssignment>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerAssignments", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CustomerAssignment.TenantId));
            b.Property(x => x.EffectiveDate).HasColumnName(nameof(CustomerAssignment.EffectiveDate));
            b.Property(x => x.EndDate).HasColumnName(nameof(CustomerAssignment.EndDate));
            b.HasOne<Company>(x => x.Company).WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Customer>(x => x.Customer).WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CustomerGroupByGeo>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerGroupByGeos", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CustomerGroupByGeo.TenantId));
            b.Property(x => x.Active).HasColumnName(nameof(CustomerGroupByGeo.Active));
            b.Property(x => x.EffectiveDate).HasColumnName(nameof(CustomerGroupByGeo.EffectiveDate));
            b.HasOne<CustomerGroup>(x => x.CustomerGroup).WithMany().IsRequired().HasForeignKey(x => x.CustomerGroupId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster0).WithMany().HasForeignKey(x => x.GeoMaster0Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster1).WithMany().HasForeignKey(x => x.GeoMaster1Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster2).WithMany().HasForeignKey(x => x.GeoMaster2Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster3).WithMany().HasForeignKey(x => x.GeoMaster3Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster4).WithMany().HasForeignKey(x => x.GeoMaster4Id).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CustomerGroupByList>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerGroupByLists", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CustomerGroupByList.TenantId));
            b.Property(x => x.Active).HasColumnName(nameof(CustomerGroupByList.Active));
            b.HasOne<CustomerGroup>(x => x.CustomerGroup).WithMany().IsRequired().HasForeignKey(x => x.CustomerGroupId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Customer>(x => x.Customer).WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<CustomerGroupByAtt>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerGroupByAtts", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CustomerGroupByAtt.TenantId));
            b.Property(x => x.ValueCode).HasColumnName(nameof(CustomerGroupByAtt.ValueCode)).HasMaxLength(CustomerGroupByAttConsts.ValueCodeMaxLength);
            b.Property(x => x.ValueName).HasColumnName(nameof(CustomerGroupByAtt.ValueName)).HasMaxLength(CustomerGroupByAttConsts.ValueNameMaxLength);
            b.HasOne<CustomerGroup>(x => x.CustomerGroup).WithMany().IsRequired().HasForeignKey(x => x.CustomerGroupId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<CusAttributeValue>(x => x.CusAttributeValue).WithMany().IsRequired().HasForeignKey(x => x.CusAttributeValueId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<CustomerGroup>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerGroups", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CustomerGroup.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(CustomerGroup.Code)).IsRequired().HasMaxLength(CustomerGroupConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(CustomerGroup.Name)).HasMaxLength(CustomerGroupConsts.NameMaxLength);
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
                b.Property(x => x.Description).HasColumnName(nameof(Holiday.Description)).IsRequired().HasMaxLength(HolidayConsts.DescriptionMaxLength);
            });
        builder.Entity<HolidayDetail>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "HolidayDetails", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(HolidayDetail.TenantId));
            b.Property(x => x.StartDate).HasColumnName(nameof(HolidayDetail.StartDate));
            b.Property(x => x.EndDate).HasColumnName(nameof(HolidayDetail.EndDate));
            b.Property(x => x.Description).HasColumnName(nameof(HolidayDetail.Description)).HasMaxLength(HolidayDetailConsts.DescriptionMaxLength);
            b.HasOne<Holiday>(x => x.Holiday).WithMany().IsRequired().HasForeignKey(x => x.HolidayId).OnDelete(DeleteBehavior.NoAction);
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
            b.HasOne<Customer>(x => x.Customer).WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<MCPHeader>(x => x.MCPHeader).WithMany().IsRequired().HasForeignKey(x => x.MCPHeaderId).OnDelete(DeleteBehavior.NoAction);
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
            b.Property(x => x.Prefix).HasColumnName(nameof(NumberingConfig.Prefix)).HasMaxLength(NumberingConfigConsts.PrefixMaxLength);
            b.Property(x => x.Suffix).HasColumnName(nameof(NumberingConfig.Suffix)).HasMaxLength(NumberingConfigConsts.SuffixMaxLength);
            b.Property(x => x.PaddingZeroNumber).HasColumnName(nameof(NumberingConfig.PaddingZeroNumber));
            b.Property(x => x.Description).HasColumnName(nameof(NumberingConfig.Description)).HasMaxLength(NumberingConfigConsts.DescriptionMaxLength);
            b.HasOne<SystemData>(x => x.SystemData).WithMany().HasForeignKey(x => x.SystemDataId).OnDelete(DeleteBehavior.NoAction);
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
            b.Property(x => x.DataSource).HasColumnName(nameof(SystemConfig.DataSource)).HasMaxLength(SystemConfigConsts.DataSourceMaxLength);
        });

        builder.Entity<CompanyIdentityUserAssignment>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CompanyIdentityUserAssignments", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CompanyIdentityUserAssignment.TenantId));
            b.Property(x => x.IdentityUserId).HasColumnName(nameof(CompanyIdentityUserAssignment.IdentityUserId));
            b.Property(x => x.CurrentlySelected).HasColumnName(nameof(CompanyIdentityUserAssignment.CurrentlySelected));
            b.HasOne<Company>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<CompanyInZone>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CompanyInZones", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CompanyInZone.TenantId));
            b.Property(x => x.EffectiveDate).HasColumnName(nameof(CompanyInZone.EffectiveDate));
            b.Property(x => x.EndDate).HasColumnName(nameof(CompanyInZone.EndDate));
            b.HasOne<SalesOrgHierarchy>(x => x.SalesOrgHierarchy).WithMany().IsRequired().HasForeignKey(x => x.SalesOrgHierarchyId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Company>(x => x.Company).WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<ItemGroup>(x => x.ItemGroup).WithMany().HasForeignKey(x => x.ItemGroupId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<ItemGroup>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemGroups", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(ItemGroup.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(ItemGroup.Code)).IsRequired().HasMaxLength(ItemGroupConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(ItemGroup.Name)).IsRequired().HasMaxLength(ItemGroupConsts.NameMaxLength);
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
            b.HasOne<ItemAttribute>(x => x.ItemAttribute).WithMany().IsRequired().HasForeignKey(x => x.ItemAttributeId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<ItemAttributeValue>(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
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
            b.Property(x => x.erpCode).HasColumnName(nameof(Item.erpCode)).HasMaxLength(ItemConsts.erpCodeMaxLength);
            b.Property(x => x.Barcode).HasColumnName(nameof(Item.Barcode)).HasMaxLength(ItemConsts.BarcodeMaxLength);
            b.Property(x => x.IsPurchasable).HasColumnName(nameof(Item.IsPurchasable));
            b.Property(x => x.IsSaleable).HasColumnName(nameof(Item.IsSaleable));
            b.Property(x => x.IsInventoriable).HasColumnName(nameof(Item.IsInventoriable));
            b.Property(x => x.BasePrice).HasColumnType("decimal(19,2)").HasColumnName(nameof(Item.BasePrice));
            b.Property(x => x.PurUnitRate).HasColumnType("decimal(19,2)").HasColumnName(nameof(Item.PurUnitRate));
            b.Property(x => x.SalesUnitRate).HasColumnType("decimal(19,2)").HasColumnName(nameof(Item.SalesUnitRate));
            b.Property(x => x.Active).HasColumnName(nameof(Item.Active));
            b.Property(x => x.ManageItemBy).HasColumnName(nameof(Item.ManageItemBy));
            b.Property(x => x.ExpiredType).HasColumnName(nameof(Item.ExpiredType));
            b.Property(x => x.ExpiredValue).HasColumnName(nameof(Item.ExpiredValue));
            b.Property(x => x.IssueMethod).HasColumnName(nameof(Item.IssueMethod));
            b.Property(x => x.CanUpdate).HasColumnName(nameof(Item.CanUpdate));
            b.HasOne<SystemData>(x => x.ItemType).WithMany().IsRequired().HasForeignKey(x => x.ItemTypeId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<VAT>(x => x.VAT).WithMany().IsRequired().HasForeignKey(x => x.VatId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<UOMGroup>(x => x.UOMGroup).WithMany().IsRequired().HasForeignKey(x => x.UomGroupId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<UOM>(x => x.InventoryUOM).WithMany().IsRequired().HasForeignKey(x => x.InventoryUOMId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<UOM>(x => x.PurUOM).WithMany().IsRequired().HasForeignKey(x => x.PurUOMId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<UOM>(x => x.SalesUOM).WithMany().IsRequired().HasForeignKey(x => x.SalesUOMId).OnDelete(DeleteBehavior.NoAction);
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
            b.Property(x => x.Price).HasColumnType("decimal(19,2)").HasColumnName(nameof(PriceListDetail.Price));
            b.Property(x => x.BasedOnPrice).HasColumnType("decimal(19,2)").HasColumnName(nameof(PriceListDetail.BasedOnPrice));
            b.Property(x => x.Description).HasColumnName(nameof(PriceListDetail.Description)).IsRequired();
            b.HasOne<PriceList>(x => x.PriceList).WithMany().IsRequired().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<UOM>(x => x.UOM).WithMany().IsRequired().HasForeignKey(x => x.UOMId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Item>(x => x.Item).WithMany().IsRequired().HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<ItemGroupList>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemGroupLists", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(ItemGroupList.TenantId));
            b.Property(x => x.Rate).HasColumnName(nameof(ItemGroupList.Rate));
            b.Property(x => x.Price).HasColumnType("decimal(19,2)").HasColumnName(nameof(ItemGroupList.Price));
            b.HasOne<ItemGroup>(x => x.ItemGroup).WithMany().IsRequired().HasForeignKey(x => x.ItemGroupId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Item>(x => x.Item).WithMany().IsRequired().HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<UOM>(x => x.UOM).WithMany().IsRequired().HasForeignKey(x => x.UomId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<MCPHeader>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "MCPHeaders", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(MCPHeader.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(MCPHeader.Code)).IsRequired().HasMaxLength(MCPHeaderConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(MCPHeader.Name)).HasMaxLength(MCPHeaderConsts.NameMaxLength);
            b.Property(x => x.EffectiveDate).HasColumnName(nameof(MCPHeader.EffectiveDate));
            b.Property(x => x.EndDate).HasColumnName(nameof(MCPHeader.EndDate));
            b.HasOne<SalesOrgHierarchy>(x => x.SalesOrgHierarchy).WithMany().IsRequired().HasForeignKey(x => x.RouteId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Company>(x => x.Company).WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<ItemGroup>(x => x.ItemGroup).WithMany().HasForeignKey(x => x.ItemGroupId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<Company>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Companies", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(Company.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(Company.Code)).IsRequired().HasMaxLength(CompanyConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(Company.Name)).IsRequired().HasMaxLength(CompanyConsts.NameMaxLength);
            b.Property(x => x.Street).HasColumnName(nameof(Company.Street)).HasMaxLength(CompanyConsts.StreetMaxLength);
            b.Property(x => x.Address).HasColumnName(nameof(Company.Address)).HasMaxLength(CompanyConsts.AddressMaxLength);
            b.Property(x => x.Phone).HasColumnName(nameof(Company.Phone)).HasMaxLength(CompanyConsts.PhoneMaxLength);
            b.Property(x => x.License).HasColumnName(nameof(Company.License)).HasMaxLength(CompanyConsts.LicenseMaxLength);
            b.Property(x => x.TaxCode).HasColumnName(nameof(Company.TaxCode)).HasMaxLength(CompanyConsts.TaxCodeMaxLength);
            b.Property(x => x.VATName).HasColumnName(nameof(Company.VATName)).HasMaxLength(CompanyConsts.VATNameMaxLength);
            b.Property(x => x.VATAddress).HasColumnName(nameof(Company.VATAddress)).HasMaxLength(CompanyConsts.VATAddressMaxLength);
            b.Property(x => x.ERPCode).HasColumnName(nameof(Company.ERPCode)).HasMaxLength(CompanyConsts.ERPCodeMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(Company.Active));
            b.Property(x => x.EffectiveDate).HasColumnName(nameof(Company.EffectiveDate));
            b.Property(x => x.EndDate).HasColumnName(nameof(Company.EndDate));
            b.Property(x => x.IsHO).HasColumnName(nameof(Company.IsHO));
            b.Property(x => x.Latitude).HasColumnName(nameof(Company.Latitude)).HasMaxLength(CompanyConsts.LatitudeMaxLength);
            b.Property(x => x.Longitude).HasColumnName(nameof(Company.Longitude)).HasMaxLength(CompanyConsts.LongitudeMaxLength);
            b.Property(x => x.ContactName).HasColumnName(nameof(Company.ContactName)).HasMaxLength(CompanyConsts.ContactNameMaxLength);
            b.Property(x => x.ContactPhone).HasColumnName(nameof(Company.ContactPhone)).HasMaxLength(CompanyConsts.ContactPhoneMaxLength);
            b.HasOne<Company>(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoLevel0).WithMany().HasForeignKey(x => x.GeoLevel0Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoLevel1).WithMany().HasForeignKey(x => x.GeoLevel1Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoLevel2).WithMany().HasForeignKey(x => x.GeoLevel2Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoLevel3).WithMany().HasForeignKey(x => x.GeoLevel3Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoLevel4).WithMany().HasForeignKey(x => x.GeoLevel4Id).OnDelete(DeleteBehavior.NoAction);
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
            b.Property(x => x.IsCommando).HasColumnName(nameof(VisitPlan.IsCommando));
            b.HasOne<MCPDetail>(x => x.MCPDetail).WithMany().IsRequired().HasForeignKey(x => x.MCPDetailId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Customer>(x => x.Customer).WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<SalesOrgHierarchy>(x => x.Route).WithMany().IsRequired().HasForeignKey(x => x.RouteId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<ItemGroup>(x => x.ItemGroup).WithMany().HasForeignKey(x => x.ItemGroupId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Vendor>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "Vendors", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(Vendor.TenantId));
            b.Property(x => x.Code).HasColumnName(nameof(Vendor.Code)).IsRequired().HasMaxLength(VendorConsts.CodeMaxLength);
            b.Property(x => x.Name).HasColumnName(nameof(Vendor.Name)).IsRequired().HasMaxLength(VendorConsts.NameMaxLength);
            b.Property(x => x.ShortName).HasColumnName(nameof(Vendor.ShortName)).HasMaxLength(VendorConsts.ShortNameMaxLength);
            b.Property(x => x.Phone1).HasColumnName(nameof(Vendor.Phone1)).HasMaxLength(VendorConsts.Phone1MaxLength);
            b.Property(x => x.Phone2).HasColumnName(nameof(Vendor.Phone2)).HasMaxLength(VendorConsts.Phone2MaxLength);
            b.Property(x => x.ERPCode).HasColumnName(nameof(Vendor.ERPCode)).HasMaxLength(VendorConsts.ERPCodeMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(Vendor.Active));
            b.Property(x => x.EndDate).HasColumnName(nameof(Vendor.EndDate));
            b.Property(x => x.Street).HasColumnName(nameof(Vendor.Street)).HasMaxLength(VendorConsts.StreetMaxLength);
            b.Property(x => x.Address).HasColumnName(nameof(Vendor.Address)).HasMaxLength(VendorConsts.AddressMaxLength);
            b.Property(x => x.Latitude).HasColumnName(nameof(Vendor.Latitude)).HasMaxLength(VendorConsts.LatitudeMaxLength);
            b.Property(x => x.Longitude).HasColumnName(nameof(Vendor.Longitude)).HasMaxLength(VendorConsts.LongitudeMaxLength);
            b.HasOne<PriceList>(x => x.PriceList).WithMany().IsRequired().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster0).WithMany().HasForeignKey(x => x.GeoMaster0Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster1).WithMany().HasForeignKey(x => x.GeoMaster1Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster2).WithMany().HasForeignKey(x => x.GeoMaster2Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster3).WithMany().HasForeignKey(x => x.GeoMaster3Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster4).WithMany().HasForeignKey(x => x.GeoMaster4Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Company>(x => x.Company).WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Company>(x => x.LinkedCompany).WithMany().HasForeignKey(x => x.LinkedCompanyId).OnDelete(DeleteBehavior.NoAction);
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
            b.Property(x => x.SFACustomerCode).HasColumnName(nameof(Customer.SFACustomerCode)).HasMaxLength(CustomerConsts.SFACustomerCodeMaxLength);
            b.Property(x => x.LastOrderDate).HasColumnName(nameof(Customer.LastOrderDate));
            b.HasOne<SystemData>().WithMany().HasForeignKey(x => x.PaymentTermId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Company>().WithMany().HasForeignKey(x => x.LinkedCompanyId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<PriceList>(x => x.PriceList).WithMany().HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster0).WithMany().HasForeignKey(x => x.GeoMaster0Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster1).WithMany().HasForeignKey(x => x.GeoMaster1Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster2).WithMany().HasForeignKey(x => x.GeoMaster2Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster3).WithMany().HasForeignKey(x => x.GeoMaster3Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<GeoMaster>(x => x.GeoMaster4).WithMany().HasForeignKey(x => x.GeoMaster4Id).OnDelete(DeleteBehavior.NoAction);
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
            b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute17Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute18Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<CusAttributeValue>().WithMany().HasForeignKey(x => x.Attribute19Id).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Customer>().WithMany().HasForeignKey(x => x.PaymentId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<ItemAttachment>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemAttachments", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(ItemAttachment.TenantId));
            b.Property(x => x.Description).HasColumnName(nameof(ItemAttachment.Description)).HasMaxLength(ItemAttachmentConsts.DescriptionMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(ItemAttachment.Active));
            b.Property(x => x.FileId).HasColumnName(nameof(ItemAttachment.FileId));
            b.HasOne<Item>(x => x.Item).WithMany().IsRequired().HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<ItemImage>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "ItemImages", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(ItemImage.TenantId));
            b.Property(x => x.Description).HasColumnName(nameof(ItemImage.Description)).HasMaxLength(ItemImageConsts.DescriptionMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(ItemImage.Active));
            b.Property(x => x.DisplayOrder).HasColumnName(nameof(ItemImage.DisplayOrder));
            b.Property(x => x.FileId).HasColumnName(nameof(ItemImage.FileId));
            b.HasOne<Item>(x => x.Item).WithMany().IsRequired().HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<CustomerAttachment>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerAttachments", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(CustomerAttachment.TenantId));
            b.Property(x => x.Description).HasColumnName(nameof(CustomerAttachment.Description)).HasMaxLength(CustomerAttachmentConsts.DescriptionMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(CustomerAttachment.Active));
            b.Property(x => x.FileId).HasColumnName(nameof(CustomerAttachment.FileId));
            b.HasOne<Customer>(x => x.Customer).WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<EmployeeAttachment>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "EmployeeAttachments", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(EmployeeAttachment.TenantId));
            b.Property(x => x.Description).HasColumnName(nameof(EmployeeAttachment.Description)).HasMaxLength(EmployeeAttachmentConsts.DescriptionMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(EmployeeAttachment.Active));
            b.Property(x => x.FileId).HasColumnName(nameof(EmployeeAttachment.FileId));
            b.HasOne<EmployeeProfile>(x => x.EmployeeProfile).WithMany().IsRequired().HasForeignKey(x => x.EmployeeProfileId).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<EmployeeImage>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "EmployeeImages", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(EmployeeImage.TenantId));
            b.Property(x => x.Description).HasColumnName(nameof(EmployeeImage.Description)).HasMaxLength(EmployeeImageConsts.DescriptionMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(EmployeeImage.Active));
            b.Property(x => x.IsAvatar).HasColumnName(nameof(EmployeeImage.IsAvatar));
            b.Property(x => x.FileId).HasColumnName(nameof(EmployeeImage.FileId));
            b.HasOne<EmployeeProfile>(x => x.EmployeeProfile).WithMany().IsRequired().HasForeignKey(x => x.EmployeeProfileId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CustomerImage>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CustomerImages", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CustomerImage.TenantId));
        b.Property(x => x.Description).HasColumnName(nameof(CustomerImage.Description)).HasMaxLength(CustomerImageConsts.DescriptionMaxLength);
        b.Property(x => x.Active).HasColumnName(nameof(CustomerImage.Active));
        b.Property(x => x.IsAvatar).HasColumnName(nameof(CustomerImage.IsAvatar));
        b.Property(x => x.IsPOSM).HasColumnName(nameof(CustomerImage.IsPOSM));
        b.Property(x => x.FileId).HasColumnName(nameof(CustomerImage.FileId));
        b.HasOne<Customer>(x => x.Customer).WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Item>(x => x.ItemPOSM).WithMany().HasForeignKey(x => x.POSMItemId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<CompanyIdentityUserAssignment>(b =>
    {
        b.ToTable(MdmServiceDbProperties.DbTablePrefix + "CompanyIdentityUserAssignments", MdmServiceDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CompanyIdentityUserAssignment.TenantId));
        b.Property(x => x.IdentityUserId).HasColumnName(nameof(CompanyIdentityUserAssignment.IdentityUserId));
        b.Property(x => x.CurrentlySelected).HasColumnName(nameof(CompanyIdentityUserAssignment.CurrentlySelected));
        b.HasOne<Company>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<NumberingConfigDetail>(b =>
        {
            b.ToTable(MdmServiceDbProperties.DbTablePrefix + "NumberingConfigDetails", MdmServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(NumberingConfigDetail.TenantId));
            b.Property(x => x.Description).HasColumnName(nameof(NumberingConfigDetail.Description)).HasMaxLength(NumberingConfigConsts.DescriptionMaxLength);
            b.Property(x => x.Prefix).HasColumnName(nameof(NumberingConfigDetail.Prefix)).HasMaxLength(NumberingConfigConsts.PrefixMaxLength);
            b.Property(x => x.PaddingZeroNumber).HasColumnName(nameof(NumberingConfigDetail.PaddingZeroNumber));
            b.Property(x => x.Suffix).HasColumnName(nameof(NumberingConfigDetail.Suffix)).HasMaxLength(NumberingConfigConsts.SuffixMaxLength);
            b.Property(x => x.Active).HasColumnName(nameof(NumberingConfigDetail.Active));
            b.Property(x => x.CurrentNumber).HasColumnName(nameof(NumberingConfigDetail.CurrentNumber));
            b.HasOne<NumberingConfig>(x => x.NumberingConfig).WithMany().IsRequired().HasForeignKey(x => x.NumberingConfigId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Company>(x => x.Company).WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
        });
    }
}