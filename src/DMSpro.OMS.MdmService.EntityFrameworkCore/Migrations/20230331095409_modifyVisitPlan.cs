using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class modifyVisitPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitPlans_Companies_CompanyId",
                table: "VisitPlans");

            migrationBuilder.DropIndex(
                name: "IX_VisitPlans_CompanyId",
                table: "VisitPlans");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "VisitPlans");

            migrationBuilder.AddColumn<bool>(
                name: "IsCommando",
                table: "VisitPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCommando",
                table: "VisitPlans");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "VisitPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VisitPlans_CompanyId",
                table: "VisitPlans",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitPlans_Companies_CompanyId",
                table: "VisitPlans",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
