using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplifiedPicPay.Migrations
{
    /// <inheritdoc />
    public partial class FixingWalletTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WALLET",
                table: "WALLET");

            migrationBuilder.RenameTable(
                name: "WALLET",
                newName: "wallet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_wallet",
                table: "wallet",
                column: "wallet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_wallet",
                table: "wallet");

            migrationBuilder.RenameTable(
                name: "wallet",
                newName: "WALLET");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WALLET",
                table: "WALLET",
                column: "wallet_id");
        }
    }
}
