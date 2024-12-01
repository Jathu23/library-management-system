using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_management_system.Migrations
{
    /// <inheritdoc />
    public partial class changetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SubscriptionPlan",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "PaymentDuration",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "SubscriptionPlan");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "PaymentDuration",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
