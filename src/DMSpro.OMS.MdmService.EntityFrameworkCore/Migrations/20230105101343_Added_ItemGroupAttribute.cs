using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_ItemGroupAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemGroupAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    dummy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ItemGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Attr0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_ItemGroupAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr0Id",
                        column: x => x.Attr0Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr10Id",
                        column: x => x.Attr10Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr11Id",
                        column: x => x.Attr11Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr12Id",
                        column: x => x.Attr12Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr13Id",
                        column: x => x.Attr13Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr14Id",
                        column: x => x.Attr14Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr15Id",
                        column: x => x.Attr15Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr16Id",
                        column: x => x.Attr16Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr17Id",
                        column: x => x.Attr17Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr18Id",
                        column: x => x.Attr18Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr19Id",
                        column: x => x.Attr19Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr1Id",
                        column: x => x.Attr1Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr2Id",
                        column: x => x.Attr2Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr3Id",
                        column: x => x.Attr3Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr4Id",
                        column: x => x.Attr4Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr6Id",
                        column: x => x.Attr6Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr7Id",
                        column: x => x.Attr7Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr8Id",
                        column: x => x.Attr8Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr9Id",
                        column: x => x.Attr9Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttributes_ItemGroups_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr0Id",
                table: "ItemGroupAttributes",
                column: "Attr0Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr10Id",
                table: "ItemGroupAttributes",
                column: "Attr10Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr11Id",
                table: "ItemGroupAttributes",
                column: "Attr11Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr12Id",
                table: "ItemGroupAttributes",
                column: "Attr12Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr13Id",
                table: "ItemGroupAttributes",
                column: "Attr13Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr14Id",
                table: "ItemGroupAttributes",
                column: "Attr14Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr15Id",
                table: "ItemGroupAttributes",
                column: "Attr15Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr16Id",
                table: "ItemGroupAttributes",
                column: "Attr16Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr17Id",
                table: "ItemGroupAttributes",
                column: "Attr17Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr18Id",
                table: "ItemGroupAttributes",
                column: "Attr18Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr19Id",
                table: "ItemGroupAttributes",
                column: "Attr19Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr1Id",
                table: "ItemGroupAttributes",
                column: "Attr1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr2Id",
                table: "ItemGroupAttributes",
                column: "Attr2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr3Id",
                table: "ItemGroupAttributes",
                column: "Attr3Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr4Id",
                table: "ItemGroupAttributes",
                column: "Attr4Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr6Id",
                table: "ItemGroupAttributes",
                column: "Attr6Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr7Id",
                table: "ItemGroupAttributes",
                column: "Attr7Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr8Id",
                table: "ItemGroupAttributes",
                column: "Attr8Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr9Id",
                table: "ItemGroupAttributes",
                column: "Attr9Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_ItemGroupId",
                table: "ItemGroupAttributes",
                column: "ItemGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemGroupAttributes");
        }
    }
}
