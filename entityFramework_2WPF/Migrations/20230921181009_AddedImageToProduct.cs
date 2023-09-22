using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entityFramework_2WPF.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Products",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Products");
        }
    }
}
