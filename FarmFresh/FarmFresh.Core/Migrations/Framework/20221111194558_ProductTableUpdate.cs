using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Core.Migrations.Framework
{
    /// <inheritdoc />
    public partial class ProductTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "IsFeatured",
                table: "Products",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "KeyInformation",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyInformation",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Products",
                newName: "IsFeatured");
        }
    }
}
