using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P01_BillsPaymentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatingDbMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bankAccounts",
                columns: table => new
                {
                    BankAccountId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Balance = table.Column<double>(type: "REAL", nullable: false),
                    BankName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SWIFTCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BankAccounts", x => x.BankAccountId);
                });

            migrationBuilder.CreateTable(
                name: "creditCards",
                columns: table => new
                {
                    CreditCardId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Limit = table.Column<double>(type: "REAL", nullable: false),
                    MoneyOwed = table.Column<double>(type: "REAL", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CreditCards", x => x.CreditCardId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "paymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    paymentType = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BankAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditCardId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentMethods", x => x.Id);
                    table.CheckConstraint("CK_PaymentMethod_BankAccountId", "(BankAccountId IS NOT NULL AND CreditCardId IS NULL) OR (BankAccountId IS NULL AND CreditCardId IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_PaymentMethod_BankAccounts",
                        column: x => x.BankAccountId,
                        principalTable: "bankAccounts",
                        principalColumn: "BankAccountId");
                    table.ForeignKey(
                        name: "FK_PaymentMethod_CreditCards",
                        column: x => x.CreditCardId,
                        principalTable: "creditCards",
                        principalColumn: "CreditCardId");
                    table.ForeignKey(
                        name: "FK_PaymentMethod_Users",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_paymentMethods_BankAccountId",
                table: "paymentMethods",
                column: "BankAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_paymentMethods_CreditCardId",
                table: "paymentMethods",
                column: "CreditCardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_paymentMethods_UserId",
                table: "paymentMethods",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "paymentMethods");

            migrationBuilder.DropTable(
                name: "bankAccounts");

            migrationBuilder.DropTable(
                name: "creditCards");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
