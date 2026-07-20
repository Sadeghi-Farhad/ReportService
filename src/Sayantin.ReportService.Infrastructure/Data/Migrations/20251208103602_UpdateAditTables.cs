using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAditTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EntityName",
                table: "AuditMaster",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "نام Entity که میخواهیم آدیت کنیم",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "نام Entity که میخواهیم آدیت کنیم");

            migrationBuilder.AlterColumn<string>(
                name: "OldValue",
                table: "AuditDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "مقدار قبلی",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "مقدار قبلی");

            migrationBuilder.AlterColumn<string>(
                name: "NewValue",
                table: "AuditDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "مقدار جدید",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "مقدار جدید");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EntityName",
                table: "AuditMaster",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "نام Entity که میخواهیم آدیت کنیم",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "نام Entity که میخواهیم آدیت کنیم");

            migrationBuilder.AlterColumn<string>(
                name: "OldValue",
                table: "AuditDetail",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "مقدار قبلی",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "مقدار قبلی");

            migrationBuilder.AlterColumn<string>(
                name: "NewValue",
                table: "AuditDetail",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "مقدار جدید",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "مقدار جدید");
        }
    }
}
