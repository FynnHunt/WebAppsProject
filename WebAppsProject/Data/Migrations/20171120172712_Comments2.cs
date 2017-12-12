using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppsProject.Data.Migrations
{
    public partial class Comments2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Announcement_AnnouncementId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Comment",
                newName: "Date");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Announcement",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_UserId",
                table: "Announcement",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_AspNetUsers_UserId",
                table: "Announcement",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Announcement_AnnouncementId",
                table: "Comment",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_AspNetUsers_UserId",
                table: "Announcement");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Announcement_AnnouncementId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UserId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_UserId",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Announcement");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Comment",
                newName: "date");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                table: "Comment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Comment",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Announcement_AnnouncementId",
                table: "Comment",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
