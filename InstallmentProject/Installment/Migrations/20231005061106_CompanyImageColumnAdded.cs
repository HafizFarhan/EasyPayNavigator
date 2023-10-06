using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Installment.Migrations
{
    /// <inheritdoc />
    public partial class CompanyImageColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "LogoImage",
                table: "Companies",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoImage",
                table: "Companies");
        }
    }
}
