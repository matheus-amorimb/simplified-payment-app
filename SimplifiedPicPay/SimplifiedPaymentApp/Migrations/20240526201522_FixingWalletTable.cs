using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplifiedPicPay.Migrations
{
    /// <inheritdoc />
    public partial class FixingWalletTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

            migrationBuilder.RenameTable(
                name: "Wallet",
                newName: "WALLET");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WALLET",
                table: "WALLET",
                column: "wallet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WALLET",
                table: "WALLET");

            migrationBuilder.RenameTable(
                name: "WALLET",
                newName: "Wallet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet",
                column: "wallet_id");
        }
    }
}
