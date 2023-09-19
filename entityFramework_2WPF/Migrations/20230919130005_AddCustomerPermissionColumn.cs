using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entityFramework_2WPF.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerPermissionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Permission",
                table: "Customers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permission",
                table: "Customers");
        }
    }
}
