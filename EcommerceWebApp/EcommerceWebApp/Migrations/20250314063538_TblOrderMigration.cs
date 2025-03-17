using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class TblOrderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "order_id",
                table: "tbl_cartitem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "tbl_order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cust_id = table.Column<int>(type: "int", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zip_code = table.Column<int>(type: "int", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_order", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_tbl_order_tbl_customer_cust_id",
                        column: x => x.cust_id,
                        principalTable: "tbl_customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cartitem_order_id",
                table: "tbl_cartitem",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_cust_id",
                table: "tbl_order",
                column: "cust_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_cartitem_tbl_order_order_id",
                table: "tbl_cartitem",
                column: "order_id",
                principalTable: "tbl_order",
                principalColumn: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_cartitem_tbl_order_order_id",
                table: "tbl_cartitem");

            migrationBuilder.DropTable(
                name: "tbl_order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_cartitem_order_id",
                table: "tbl_cartitem");

            migrationBuilder.AlterColumn<int>(
                name: "order_id",
                table: "tbl_cartitem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
