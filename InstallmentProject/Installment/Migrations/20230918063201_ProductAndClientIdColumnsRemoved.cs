using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Installment.Migrations
{
    /// <inheritdoc />
    public partial class ProductAndClientIdColumnsRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "InstallmentPlan");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "InstallmentPlan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "InstallmentPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "InstallmentPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
