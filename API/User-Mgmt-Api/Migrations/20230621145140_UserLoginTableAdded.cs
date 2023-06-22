using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Mgmt_Api.Migrations
{
    public partial class UserLoginTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userLogins_Users_UserId",
                table: "userLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userLogins",
                table: "userLogins");

            migrationBuilder.DropIndex(
                name: "IX_userLogins_UserId",
                table: "userLogins");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "userLogins");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "userLogins");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "userLogins");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "userLogins",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "userLogins",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "userLogins");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "userLogins");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "userLogins",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "userLogins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "userLogins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_userLogins",
                table: "userLogins",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_userLogins_UserId",
                table: "userLogins",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_userLogins_Users_UserId",
                table: "userLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
