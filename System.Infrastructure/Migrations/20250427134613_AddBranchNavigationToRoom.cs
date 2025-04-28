using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchNavigationToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d61a737-5193-47e1-8be8-640e84d55aac", "AQAAAAIAAYagAAAAEMk4+4jaZ31YmcpcbTinQa1mLHOi6H1ghE705pfbyryypMc5FjZqD8mVkKNCxrs/jA==", "4c04146f-0525-49ef-830f-eb4e87d55286" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6918086a-c917-4276-abbf-721031bd8181", "AQAAAAIAAYagAAAAEK7iLAbZcOilYEinr29c+SudJ6Wpan25KBlNtp+gbJvg6w9KEtIqXPuOtjp2uAOHcw==", "8dd67a52-5552-44ab-ab6b-80ad52c48565" });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 27, 13, 46, 12, 691, DateTimeKind.Utc).AddTicks(8413), new DateTime(2025, 4, 27, 13, 46, 12, 691, DateTimeKind.Utc).AddTicks(8413) });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 27, 13, 46, 12, 691, DateTimeKind.Utc).AddTicks(8326), new DateTime(2025, 4, 27, 13, 46, 12, 691, DateTimeKind.Utc).AddTicks(8331) });

            migrationBuilder.UpdateData(
                table: "UserStores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 27, 13, 46, 12, 691, DateTimeKind.Utc).AddTicks(8467), new DateTime(2025, 4, 27, 13, 46, 12, 691, DateTimeKind.Utc).AddTicks(8467) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd15164e-0dfb-477b-add0-8582bb4b91ba", "AQAAAAIAAYagAAAAEFFdEDc0+uRAO8n7Cbt9iqDdM7SyLY48hB+0BYBipbUU3wvY4pyomwIdE8shZmjUNA==", "bd786366-863c-42f2-8fc0-ff0f7f489e36" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f81d0448-3bc4-4b87-bc26-7711609f9c23", "AQAAAAIAAYagAAAAEAyyF5plCd27Q6MRvNoCKM9ul+9d6dY5008UKDfOz9Z+jKPAGMogd4ZamyaVjc2wxw==", "47d710a6-3204-490d-8fae-030fdee6a2f5" });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 27, 9, 38, 5, 837, DateTimeKind.Utc).AddTicks(718), new DateTime(2025, 4, 27, 9, 38, 5, 837, DateTimeKind.Utc).AddTicks(719) });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 27, 9, 38, 5, 837, DateTimeKind.Utc).AddTicks(492), new DateTime(2025, 4, 27, 9, 38, 5, 837, DateTimeKind.Utc).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "UserStores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 27, 9, 38, 5, 837, DateTimeKind.Utc).AddTicks(3608), new DateTime(2025, 4, 27, 9, 38, 5, 837, DateTimeKind.Utc).AddTicks(3610) });
        }
    }
}
