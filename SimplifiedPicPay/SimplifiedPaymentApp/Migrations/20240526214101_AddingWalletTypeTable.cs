using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimplifiedPicPay.Migrations
{
    /// <inheritdoc />
    public partial class AddingWalletTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "wallet");

            migrationBuilder.AddColumn<int>(
                name: "wallet_type_id",
                table: "wallet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "wallet_type",
                columns: table => new
                {
                    wallet_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallet_type", x => x.wallet_type_id);
                });

            migrationBuilder.InsertData(
                table: "wallet_type",
                columns: new[] { "wallet_type_id", "description" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Merchant" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_wallet_wallet_type_id",
                table: "wallet",
                column: "wallet_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_wallet_wallet_type_wallet_type_id",
                table: "wallet",
                column: "wallet_type_id",
                principalTable: "wallet_type",
                principalColumn: "wallet_type_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wallet_wallet_type_wallet_type_id",
                table: "wallet");

            migrationBuilder.DropTable(
                name: "wallet_type");

            migrationBuilder.DropIndex(
                name: "IX_wallet_wallet_type_id",
                table: "wallet");

            migrationBuilder.DropColumn(
                name: "wallet_type_id",
                table: "wallet");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "wallet",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
