using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class TblOrderUpdateMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "payment_method",
                table: "tbl_order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "payment_method",
                table: "tbl_order");
        }
    }
}
