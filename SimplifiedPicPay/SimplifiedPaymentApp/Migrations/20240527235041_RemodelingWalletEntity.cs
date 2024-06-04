using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplifiedPicPay.Migrations
{
    /// <inheritdoc />
    public partial class RemodelingWalletEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "wallet",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_wallet_cpf",
                table: "wallet",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_wallet_email",
                table: "wallet",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_wallet_cpf",
                table: "wallet");

            migrationBuilder.DropIndex(
                name: "IX_wallet_email",
                table: "wallet");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "wallet");
        }
    }
}
