using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class TblCartItemMig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Customer_cust_id",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_tbl_product_prod_id",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "tbl_customer");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "tbl_cartitem");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_prod_id",
                table: "tbl_cartitem",
                newName: "IX_tbl_cartitem_prod_id");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_cust_id",
                table: "tbl_cartitem",
                newName: "IX_tbl_cartitem_cust_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_customer",
                table: "tbl_customer",
                column: "customer_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_cartitem",
                table: "tbl_cartitem",
                column: "cart_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_cartitem_tbl_customer_cust_id",
                table: "tbl_cartitem",
                column: "cust_id",
                principalTable: "tbl_customer",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_cartitem_tbl_product_prod_id",
                table: "tbl_cartitem",
                column: "prod_id",
                principalTable: "tbl_product",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_cartitem_tbl_customer_cust_id",
                table: "tbl_cartitem");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_cartitem_tbl_product_prod_id",
                table: "tbl_cartitem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_customer",
                table: "tbl_customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_cartitem",
                table: "tbl_cartitem");

            migrationBuilder.RenameTable(
                name: "tbl_customer",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "tbl_cartitem",
                newName: "CartItem");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_cartitem_prod_id",
                table: "CartItem",
                newName: "IX_CartItem_prod_id");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_cartitem_cust_id",
                table: "CartItem",
                newName: "IX_CartItem_cust_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "customer_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "cart_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Customer_cust_id",
                table: "CartItem",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_tbl_product_prod_id",
                table: "CartItem",
                column: "prod_id",
                principalTable: "tbl_product",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
