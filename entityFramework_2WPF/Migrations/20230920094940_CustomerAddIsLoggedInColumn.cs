using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entityFramework_2WPF.Migrations
{
    /// <inheritdoc />
    public partial class CustomerAddIsLoggedInColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLoggedIn",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLoggedIn",
                table: "Customers");
        }
    }
}
