using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public static partial class SystemDataConsts
    {
        public static List<(string Code, string ValueCode, string ValueName)> SeedData = new()
        {
            //Seed ItemType - MD02
            (Code: "MD02", ValueCode: "I", ValueName: "Item"),
            (Code: "MD02", ValueCode: "P", ValueName: "POSM"),
            (Code: "MD02", ValueCode: "D", ValueName: "Discount"),
            (Code: "MD02", ValueCode: "V", ValueName: "Voucher"),

            //MD05 - Payment Term
            (Code: "MD05", ValueCode: "5", ValueName: "5"),
            (Code: "MD05", ValueCode: "7", ValueName: "7"),
            (Code: "MD05", ValueCode: "10", ValueName: "10"),
            (Code: "MD05", ValueCode: "15", ValueName: "15"),
            (Code: "MD05", ValueCode: "30", ValueName: "30"),
            (Code: "MD05", ValueCode: "45", ValueName: "45"),
            (Code: "MD05", ValueCode: "60", ValueName: "60"),

            //MD06 - Route Type
            (Code: "MD06", ValueCode: "1", ValueName: "Sales"),
            (Code: "MD06", ValueCode: "2", ValueName: "Delivery"),
            (Code: "MD06", ValueCode: "3", ValueName: "Display"),
            (Code: "MD06", ValueCode: "4", ValueName: "Audit"),

            //MD07 - Item Group Applicable 
            (Code: "MD07", ValueCode: "1", ValueName: "KPI"),
            (Code: "MD07", ValueCode: "2", ValueName: "Promotion (On/Off)"),
            (Code: "MD07", ValueCode: "3", ValueName: "Stock Count"),

            //MD09 - Sale Material Type
            (Code: "MD09", ValueCode: "1", ValueName: "Sales"),
            (Code: "MD09", ValueCode: "2", ValueName: "Training"),

            //IN01 - Inventory Transaction Type
            (Code: "IN01", ValueCode: "1", ValueName: "Item"),
            (Code: "IN01", ValueCode: "2", ValueName: "Promotion"),
            (Code: "IN01", ValueCode: "3", ValueName: "Sampling"),
            (Code: "IN01", ValueCode: "4", ValueName: "Incentive"),

            //SY01 - Object Type
            // Documents for SalesOrder
            (Code: "SY01", ValueCode: "S1", ValueName: "Sales Orders"),
            (Code: "SY01", ValueCode: "S2", ValueName: "Deliveries"),
            (Code: "SY01", ValueCode: "S3", ValueName: "A/R Invoices"),
            (Code: "SY01", ValueCode: "S4", ValueName: "A/R Credit Memos"),
            (Code: "SY01", ValueCode: "S5", ValueName: "Return Orders"),
            (Code: "SY01", ValueCode: "S6", ValueName: "Sales Requests"),
            // Documents for PurchaseOrder
            (Code: "SY01", ValueCode: "P1", ValueName: "Purchase Requests"),
            (Code: "SY01", ValueCode: "P2", ValueName: "Purchase Orders"),
            (Code: "SY01", ValueCode: "P3", ValueName: "A/P Invoices"),
            (Code: "SY01", ValueCode: "P4", ValueName: "A/P Credit Memos"),
            // General Objects
            (Code: "SY01", ValueCode: "M1", ValueName: "Customers"),
            (Code: "SY01", ValueCode: "M2", ValueName: "Employees"),
            (Code: "SY01", ValueCode: "M3", ValueName: "Routes"),
            (Code: "SY01", ValueCode: "M4", ValueName: "Items"),
            (Code: "SY01", ValueCode: "M5", ValueName: "Vendors"),
            (Code: "SY01", ValueCode: "M6", ValueName: "SalesOrgHierarchies"),
            // Inventory
            (Code: "SY01", ValueCode: "I1", ValueName: "Goods Receipts"),
            (Code: "SY01", ValueCode: "I2", ValueName: "Goods Issues"),
            (Code: "SY01", ValueCode: "I3", ValueName: "Inventory Transfers"),
            (Code: "SY01", ValueCode: "I4", ValueName: "Inventory Countings"),
            (Code: "SY01", ValueCode: "I5", ValueName: "Inventory Adjustments"),
        };
    }
}