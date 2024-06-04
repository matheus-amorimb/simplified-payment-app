using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplifiedPicPay.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentityAndAddWallet2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wallet_User_UserId",
                table: "wallet");

            migrationBuilder.DropIndex(
                name: "IX_wallet_cpf",
                table: "wallet");

            migrationBuilder.DropColumn(
                name: "cpf",
                table: "wallet");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "wallet",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_wallet_UserId",
                table: "wallet",
                newName: "IX_wallet_user_id");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cpf",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_cpf",
                table: "User",
                column: "cpf",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_wallet_User_user_id",
                table: "wallet",
                column: "user_id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wallet_User_user_id",
                table: "wallet");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_cpf",
                table: "User");

            migrationBuilder.DropColumn(
                name: "cpf",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "wallet",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_wallet_user_id",
                table: "wallet",
                newName: "IX_wallet_UserId");

            migrationBuilder.AddColumn<string>(
                name: "cpf",
                table: "wallet",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_wallet_cpf",
                table: "wallet",
                column: "cpf",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_wallet_User_UserId",
                table: "wallet",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
