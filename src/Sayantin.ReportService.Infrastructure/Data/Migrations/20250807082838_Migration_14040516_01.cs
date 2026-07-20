using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration_14040516_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "نام کاربر",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "نام کاربر");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "ایمیل کاربر",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "ایمیل کاربر");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Street",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                comment: "خیابان",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Province",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                comment: "استان",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_PostalCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                comment: "کد پستی",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_City",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                comment: "شهر",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublished",
                table: "Blogs",
                type: "bit",
                nullable: false,
                comment: "وضعیت انتشار",
                oldClrType: typeof(bool),
                oldType: "bit");
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
                comment: "نام کاربر",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "نام کاربر");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "ایمیل کاربر",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "ایمیل کاربر");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Street",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "خیابان");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Province",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "استان");

            migrationBuilder.AlterColumn<string>(
                name: "Address_PostalCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "کد پستی");

            migrationBuilder.AlterColumn<string>(
                name: "Address_City",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "شهر");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublished",
                table: "Blogs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت انتشار");
        }
    }
}
