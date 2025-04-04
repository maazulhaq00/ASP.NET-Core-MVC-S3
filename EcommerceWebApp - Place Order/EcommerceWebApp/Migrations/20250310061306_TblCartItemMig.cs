using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class TblCartItemMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_customer",
                table: "tbl_customer");

            migrationBuilder.RenameTable(
                name: "tbl_customer",
                newName: "Customer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "customer_id");

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    cart_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prod_id = table.Column<int>(type: "int", nullable: false),
                    cust_id = table.Column<int>(type: "int", nullable: false),
                    product_quantity = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.cart_id);
                    table.ForeignKey(
                        name: "FK_CartItem_Customer_cust_id",
                        column: x => x.cust_id,
                        principalTable: "Customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_tbl_product_prod_id",
                        column: x => x.prod_id,
                        principalTable: "tbl_product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_cust_id",
                table: "CartItem",
                column: "cust_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_prod_id",
                table: "CartItem",
                column: "prod_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "tbl_customer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_customer",
                table: "tbl_customer",
                column: "customer_id");
        }
    }
}
