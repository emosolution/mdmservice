using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerGroupAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerGroupAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CustomerGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Attr0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr5Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr6Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr7Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr8Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr9Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr10Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr11Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr12Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr13Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr14Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr15Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr16Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr17Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr18Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr19Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroupAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr0Id",
                        column: x => x.Attr0Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr10Id",
                        column: x => x.Attr10Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr11Id",
                        column: x => x.Attr11Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr12Id",
                        column: x => x.Attr12Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr13Id",
                        column: x => x.Attr13Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr14Id",
                        column: x => x.Attr14Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr15Id",
                        column: x => x.Attr15Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr16Id",
                        column: x => x.Attr16Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr17Id",
                        column: x => x.Attr17Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr18Id",
                        column: x => x.Attr18Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr19Id",
                        column: x => x.Attr19Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr1Id",
                        column: x => x.Attr1Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr2Id",
                        column: x => x.Attr2Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr3Id",
                        column: x => x.Attr3Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr4Id",
                        column: x => x.Attr4Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr5Id",
                        column: x => x.Attr5Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr6Id",
                        column: x => x.Attr6Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr7Id",
                        column: x => x.Attr7Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr8Id",
                        column: x => x.Attr8Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerAttributeValues_Attr9Id",
                        column: x => x.Attr9Id,
                        principalTable: "CustomerAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupAttributes_CustomerGroups_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "CustomerGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr0Id",
                table: "CustomerGroupAttributes",
                column: "Attr0Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr10Id",
                table: "CustomerGroupAttributes",
                column: "Attr10Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr11Id",
                table: "CustomerGroupAttributes",
                column: "Attr11Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr12Id",
                table: "CustomerGroupAttributes",
                column: "Attr12Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr13Id",
                table: "CustomerGroupAttributes",
                column: "Attr13Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr14Id",
                table: "CustomerGroupAttributes",
                column: "Attr14Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr15Id",
                table: "CustomerGroupAttributes",
                column: "Attr15Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr16Id",
                table: "CustomerGroupAttributes",
                column: "Attr16Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr17Id",
                table: "CustomerGroupAttributes",
                column: "Attr17Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr18Id",
                table: "CustomerGroupAttributes",
                column: "Attr18Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr19Id",
                table: "CustomerGroupAttributes",
                column: "Attr19Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr1Id",
                table: "CustomerGroupAttributes",
                column: "Attr1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr2Id",
                table: "CustomerGroupAttributes",
                column: "Attr2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr3Id",
                table: "CustomerGroupAttributes",
                column: "Attr3Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr4Id",
                table: "CustomerGroupAttributes",
                column: "Attr4Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr5Id",
                table: "CustomerGroupAttributes",
                column: "Attr5Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr6Id",
                table: "CustomerGroupAttributes",
                column: "Attr6Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr7Id",
                table: "CustomerGroupAttributes",
                column: "Attr7Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr8Id",
                table: "CustomerGroupAttributes",
                column: "Attr8Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_Attr9Id",
                table: "CustomerGroupAttributes",
                column: "Attr9Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupAttributes_CustomerGroupId",
                table: "CustomerGroupAttributes",
                column: "CustomerGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerGroupAttributes");
        }
    }
}
