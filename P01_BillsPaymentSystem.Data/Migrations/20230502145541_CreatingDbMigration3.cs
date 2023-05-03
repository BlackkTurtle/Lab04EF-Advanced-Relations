using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P01_BillsPaymentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatingDbMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paymentType",
                table: "paymentMethods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "paymentType",
                table: "paymentMethods",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
