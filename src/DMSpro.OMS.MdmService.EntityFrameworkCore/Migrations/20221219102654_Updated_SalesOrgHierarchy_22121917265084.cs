using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_SalesOrgHierarchy_22121917265084 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "SalesOrgHierarchies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrgHierarchies_ParentId",
                table: "SalesOrgHierarchies",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrgHierarchies_SalesOrgHierarchies_ParentId",
                table: "SalesOrgHierarchies",
                column: "ParentId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrgHierarchies_SalesOrgHierarchies_ParentId",
                table: "SalesOrgHierarchies");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrgHierarchies_ParentId",
                table: "SalesOrgHierarchies");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "SalesOrgHierarchies");
        }
    }
}
