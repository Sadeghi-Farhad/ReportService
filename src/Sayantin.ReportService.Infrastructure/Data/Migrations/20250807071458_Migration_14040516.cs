using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration_14040516 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "نام کاربر",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "ایمیل کاربر",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Birthday",
                table: "Users",
                type: "date",
                nullable: false,
                comment: "تاریخ تولد کاربر",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                comment: "شناسه کاربر",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Blogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "عنوان بلاگ",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Blogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "متن بلاگ",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Blogs",
                type: "int",
                nullable: false,
                comment: "شناسه نویسنده",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Blogs",
                type: "int",
                nullable: false,
                comment: "شناسه بلاگ",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "نام کاربر");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "ایمیل کاربر");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Birthday",
                table: "Users",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldComment: "تاریخ تولد کاربر");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "شناسه کاربر")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Blogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "عنوان بلاگ");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Blogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "متن بلاگ");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Blogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "شناسه نویسنده");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Blogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "شناسه بلاگ")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
