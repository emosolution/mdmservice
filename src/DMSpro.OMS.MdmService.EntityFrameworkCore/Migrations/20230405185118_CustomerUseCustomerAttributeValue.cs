using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class CustomerUseCustomerAttributeValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute0Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute10Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute11Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute12Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute13Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute14Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute15Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute16Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute17Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute18Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute19Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute1Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute2Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute3Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute4Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute5Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute6Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute7Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute8Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute9Id",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Attribute9Id",
                table: "Customers",
                newName: "Attr9Id");

            migrationBuilder.RenameColumn(
                name: "Attribute8Id",
                table: "Customers",
                newName: "Attr8Id");

            migrationBuilder.RenameColumn(
                name: "Attribute7Id",
                table: "Customers",
                newName: "Attr7Id");

            migrationBuilder.RenameColumn(
                name: "Attribute6Id",
                table: "Customers",
                newName: "Attr6Id");

            migrationBuilder.RenameColumn(
                name: "Attribute5Id",
                table: "Customers",
                newName: "Attr5Id");

            migrationBuilder.RenameColumn(
                name: "Attribute4Id",
                table: "Customers",
                newName: "Attr4Id");

            migrationBuilder.RenameColumn(
                name: "Attribute3Id",
                table: "Customers",
                newName: "Attr3Id");

            migrationBuilder.RenameColumn(
                name: "Attribute2Id",
                table: "Customers",
                newName: "Attr2Id");

            migrationBuilder.RenameColumn(
                name: "Attribute1Id",
                table: "Customers",
                newName: "Attr1Id");

            migrationBuilder.RenameColumn(
                name: "Attribute19Id",
                table: "Customers",
                newName: "Attr19Id");

            migrationBuilder.RenameColumn(
                name: "Attribute18Id",
                table: "Customers",
                newName: "Attr18Id");

            migrationBuilder.RenameColumn(
                name: "Attribute17Id",
                table: "Customers",
                newName: "Attr17Id");

            migrationBuilder.RenameColumn(
                name: "Attribute16Id",
                table: "Customers",
                newName: "Attr16Id");

            migrationBuilder.RenameColumn(
                name: "Attribute15Id",
                table: "Customers",
                newName: "Attr15Id");

            migrationBuilder.RenameColumn(
                name: "Attribute14Id",
                table: "Customers",
                newName: "Attr14Id");

            migrationBuilder.RenameColumn(
                name: "Attribute13Id",
                table: "Customers",
                newName: "Attr13Id");

            migrationBuilder.RenameColumn(
                name: "Attribute12Id",
                table: "Customers",
                newName: "Attr12Id");

            migrationBuilder.RenameColumn(
                name: "Attribute11Id",
                table: "Customers",
                newName: "Attr11Id");

            migrationBuilder.RenameColumn(
                name: "Attribute10Id",
                table: "Customers",
                newName: "Attr10Id");

            migrationBuilder.RenameColumn(
                name: "Attribute0Id",
                table: "Customers",
                newName: "Attr0Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute9Id",
                table: "Customers",
                newName: "IX_Customers_Attr9Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute8Id",
                table: "Customers",
                newName: "IX_Customers_Attr8Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute7Id",
                table: "Customers",
                newName: "IX_Customers_Attr7Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute6Id",
                table: "Customers",
                newName: "IX_Customers_Attr6Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute5Id",
                table: "Customers",
                newName: "IX_Customers_Attr5Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute4Id",
                table: "Customers",
                newName: "IX_Customers_Attr4Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute3Id",
                table: "Customers",
                newName: "IX_Customers_Attr3Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute2Id",
                table: "Customers",
                newName: "IX_Customers_Attr2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute1Id",
                table: "Customers",
                newName: "IX_Customers_Attr1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute19Id",
                table: "Customers",
                newName: "IX_Customers_Attr19Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute18Id",
                table: "Customers",
                newName: "IX_Customers_Attr18Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute17Id",
                table: "Customers",
                newName: "IX_Customers_Attr17Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute16Id",
                table: "Customers",
                newName: "IX_Customers_Attr16Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute15Id",
                table: "Customers",
                newName: "IX_Customers_Attr15Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute14Id",
                table: "Customers",
                newName: "IX_Customers_Attr14Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute13Id",
                table: "Customers",
                newName: "IX_Customers_Attr13Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute12Id",
                table: "Customers",
                newName: "IX_Customers_Attr12Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute11Id",
                table: "Customers",
                newName: "IX_Customers_Attr11Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute10Id",
                table: "Customers",
                newName: "IX_Customers_Attr10Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute0Id",
                table: "Customers",
                newName: "IX_Customers_Attr0Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr0Id",
                table: "Customers",
                column: "Attr0Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr10Id",
                table: "Customers",
                column: "Attr10Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr11Id",
                table: "Customers",
                column: "Attr11Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr12Id",
                table: "Customers",
                column: "Attr12Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr13Id",
                table: "Customers",
                column: "Attr13Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr14Id",
                table: "Customers",
                column: "Attr14Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr15Id",
                table: "Customers",
                column: "Attr15Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr16Id",
                table: "Customers",
                column: "Attr16Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr17Id",
                table: "Customers",
                column: "Attr17Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr18Id",
                table: "Customers",
                column: "Attr18Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr19Id",
                table: "Customers",
                column: "Attr19Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr1Id",
                table: "Customers",
                column: "Attr1Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr2Id",
                table: "Customers",
                column: "Attr2Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr3Id",
                table: "Customers",
                column: "Attr3Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr4Id",
                table: "Customers",
                column: "Attr4Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr5Id",
                table: "Customers",
                column: "Attr5Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr6Id",
                table: "Customers",
                column: "Attr6Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr7Id",
                table: "Customers",
                column: "Attr7Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr8Id",
                table: "Customers",
                column: "Attr8Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr9Id",
                table: "Customers",
                column: "Attr9Id",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr0Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr10Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr11Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr12Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr13Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr14Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr15Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr16Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr17Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr18Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr19Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr1Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr2Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr3Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr4Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr5Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr6Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr7Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr8Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAttributeValues_Attr9Id",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Attr9Id",
                table: "Customers",
                newName: "Attribute9Id");

            migrationBuilder.RenameColumn(
                name: "Attr8Id",
                table: "Customers",
                newName: "Attribute8Id");

            migrationBuilder.RenameColumn(
                name: "Attr7Id",
                table: "Customers",
                newName: "Attribute7Id");

            migrationBuilder.RenameColumn(
                name: "Attr6Id",
                table: "Customers",
                newName: "Attribute6Id");

            migrationBuilder.RenameColumn(
                name: "Attr5Id",
                table: "Customers",
                newName: "Attribute5Id");

            migrationBuilder.RenameColumn(
                name: "Attr4Id",
                table: "Customers",
                newName: "Attribute4Id");

            migrationBuilder.RenameColumn(
                name: "Attr3Id",
                table: "Customers",
                newName: "Attribute3Id");

            migrationBuilder.RenameColumn(
                name: "Attr2Id",
                table: "Customers",
                newName: "Attribute2Id");

            migrationBuilder.RenameColumn(
                name: "Attr1Id",
                table: "Customers",
                newName: "Attribute1Id");

            migrationBuilder.RenameColumn(
                name: "Attr19Id",
                table: "Customers",
                newName: "Attribute19Id");

            migrationBuilder.RenameColumn(
                name: "Attr18Id",
                table: "Customers",
                newName: "Attribute18Id");

            migrationBuilder.RenameColumn(
                name: "Attr17Id",
                table: "Customers",
                newName: "Attribute17Id");

            migrationBuilder.RenameColumn(
                name: "Attr16Id",
                table: "Customers",
                newName: "Attribute16Id");

            migrationBuilder.RenameColumn(
                name: "Attr15Id",
                table: "Customers",
                newName: "Attribute15Id");

            migrationBuilder.RenameColumn(
                name: "Attr14Id",
                table: "Customers",
                newName: "Attribute14Id");

            migrationBuilder.RenameColumn(
                name: "Attr13Id",
                table: "Customers",
                newName: "Attribute13Id");

            migrationBuilder.RenameColumn(
                name: "Attr12Id",
                table: "Customers",
                newName: "Attribute12Id");

            migrationBuilder.RenameColumn(
                name: "Attr11Id",
                table: "Customers",
                newName: "Attribute11Id");

            migrationBuilder.RenameColumn(
                name: "Attr10Id",
                table: "Customers",
                newName: "Attribute10Id");

            migrationBuilder.RenameColumn(
                name: "Attr0Id",
                table: "Customers",
                newName: "Attribute0Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr9Id",
                table: "Customers",
                newName: "IX_Customers_Attribute9Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr8Id",
                table: "Customers",
                newName: "IX_Customers_Attribute8Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr7Id",
                table: "Customers",
                newName: "IX_Customers_Attribute7Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr6Id",
                table: "Customers",
                newName: "IX_Customers_Attribute6Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr5Id",
                table: "Customers",
                newName: "IX_Customers_Attribute5Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr4Id",
                table: "Customers",
                newName: "IX_Customers_Attribute4Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr3Id",
                table: "Customers",
                newName: "IX_Customers_Attribute3Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr2Id",
                table: "Customers",
                newName: "IX_Customers_Attribute2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr1Id",
                table: "Customers",
                newName: "IX_Customers_Attribute1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr19Id",
                table: "Customers",
                newName: "IX_Customers_Attribute19Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr18Id",
                table: "Customers",
                newName: "IX_Customers_Attribute18Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr17Id",
                table: "Customers",
                newName: "IX_Customers_Attribute17Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr16Id",
                table: "Customers",
                newName: "IX_Customers_Attribute16Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr15Id",
                table: "Customers",
                newName: "IX_Customers_Attribute15Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr14Id",
                table: "Customers",
                newName: "IX_Customers_Attribute14Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr13Id",
                table: "Customers",
                newName: "IX_Customers_Attribute13Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr12Id",
                table: "Customers",
                newName: "IX_Customers_Attribute12Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr11Id",
                table: "Customers",
                newName: "IX_Customers_Attribute11Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr10Id",
                table: "Customers",
                newName: "IX_Customers_Attribute10Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attr0Id",
                table: "Customers",
                newName: "IX_Customers_Attribute0Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute0Id",
                table: "Customers",
                column: "Attribute0Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute10Id",
                table: "Customers",
                column: "Attribute10Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute11Id",
                table: "Customers",
                column: "Attribute11Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute12Id",
                table: "Customers",
                column: "Attribute12Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute13Id",
                table: "Customers",
                column: "Attribute13Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute14Id",
                table: "Customers",
                column: "Attribute14Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute15Id",
                table: "Customers",
                column: "Attribute15Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute16Id",
                table: "Customers",
                column: "Attribute16Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute17Id",
                table: "Customers",
                column: "Attribute17Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute18Id",
                table: "Customers",
                column: "Attribute18Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute19Id",
                table: "Customers",
                column: "Attribute19Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute1Id",
                table: "Customers",
                column: "Attribute1Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute2Id",
                table: "Customers",
                column: "Attribute2Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute3Id",
                table: "Customers",
                column: "Attribute3Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute4Id",
                table: "Customers",
                column: "Attribute4Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute5Id",
                table: "Customers",
                column: "Attribute5Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute6Id",
                table: "Customers",
                column: "Attribute6Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute7Id",
                table: "Customers",
                column: "Attribute7Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute8Id",
                table: "Customers",
                column: "Attribute8Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute9Id",
                table: "Customers",
                column: "Attribute9Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");
        }
    }
}
