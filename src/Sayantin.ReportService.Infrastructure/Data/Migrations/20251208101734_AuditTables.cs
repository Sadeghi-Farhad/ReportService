using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false, comment: "شناسه Parent Entity که میخواهیم آدیت کنیم"),
                    EntityId = table.Column<int>(type: "int", nullable: false, comment: "شناسه entity که میخواهیم آدیت کنیم"),
                    EntityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "نام Entity که میخواهیم آدیت کنیم"),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "نوع عملیاتی که انجام شده"),
                    PrsCode = table.Column<int>(type: "int", nullable: false, comment: "کد پرسنلی فردی که تغییرات داده"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان انجام تغییرات")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditMasterId = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "نام ویژگی تغییر یافته"),
                    OldValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "مقدار قبلی"),
                    NewValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "مقدار جدید")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditDetail_AuditMaster_AuditMasterId",
                        column: x => x.AuditMasterId,
                        principalTable: "AuditMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditDetail_AuditMasterId",
                table: "AuditDetail",
                column: "AuditMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditDetail");

            migrationBuilder.DropTable(
                name: "AuditMaster");
        }
    }
}
