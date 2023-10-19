using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Installment.Migrations
{
    /// <inheritdoc />
    public partial class ComapnyAndProductUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "OriginalPrice");

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Plans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "OriginalPrice",
                table: "Products",
                newName: "Price");
        }
    }
}
