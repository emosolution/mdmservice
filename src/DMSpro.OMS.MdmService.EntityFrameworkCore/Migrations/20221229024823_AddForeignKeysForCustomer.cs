using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class AddForeignKeysForCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MCPDetails_CustomerId",
                table: "MCPDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInZones_CustomerId",
                table: "CustomerInZones",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByLists_CustomerId",
                table: "CustomerGroupByLists",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAttachments_CustomerId",
                table: "CustomerAttachments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAssignments_CustomerId",
                table: "CustomerAssignments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAssignments_Customers_CustomerId",
                table: "CustomerAssignments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAttachments_Customers_CustomerId",
                table: "CustomerAttachments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContacts_Customers_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByLists_Customers_CustomerId",
                table: "CustomerGroupByLists",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInZones_Customers_CustomerId",
                table: "CustomerInZones",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MCPDetails_Customers_CustomerId",
                table: "MCPDetails",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAssignments_Customers_CustomerId",
                table: "CustomerAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAttachments_Customers_CustomerId",
                table: "CustomerAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContacts_Customers_CustomerId",
                table: "CustomerContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByLists_Customers_CustomerId",
                table: "CustomerGroupByLists");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInZones_Customers_CustomerId",
                table: "CustomerInZones");

            migrationBuilder.DropForeignKey(
                name: "FK_MCPDetails_Customers_CustomerId",
                table: "MCPDetails");

            migrationBuilder.DropIndex(
                name: "IX_MCPDetails_CustomerId",
                table: "MCPDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInZones_CustomerId",
                table: "CustomerInZones");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByLists_CustomerId",
                table: "CustomerGroupByLists");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAttachments_CustomerId",
                table: "CustomerAttachments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAssignments_CustomerId",
                table: "CustomerAssignments");
        }
    }
}
