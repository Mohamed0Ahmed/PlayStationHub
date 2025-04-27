using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentSessionIdToGuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserBranch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserBranch",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserBranch",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserBranch",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "UserBranch",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "UserBranch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "UserBranch",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentSessionId",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "CurrentSessionId",
                table: "Guests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab1efbe1-f4a1-49c1-a9da-b62ee3073257", "AQAAAAIAAYagAAAAEHVrLtP4wHwqxJIGj+OVZ8NXZGlzzWWfgrZ3U4SBUGYm/rWPQEzJEDJZ89wHynCNow==", "f2bccd30-dac4-4927-8d2f-c3e9752467be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "603f01b5-23e1-478a-8020-2318b7481a7c", "AQAAAAIAAYagAAAAEAqSE+EvUZYmFaLvjsg9gcHPfWg2JmF9zJaLNJkgdx+xgiC55j9z1Qj/UkoDW1+jow==", "24e94b3a-ccc9-4b30-a1ba-cf7af1c2b0be" });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 26, 18, 8, 2, 70, DateTimeKind.Utc).AddTicks(7270), new DateTime(2025, 4, 26, 18, 8, 2, 70, DateTimeKind.Utc).AddTicks(7270) });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 26, 18, 8, 2, 70, DateTimeKind.Utc).AddTicks(7171), new DateTime(2025, 4, 26, 18, 8, 2, 70, DateTimeKind.Utc).AddTicks(7177) });

            migrationBuilder.UpdateData(
                table: "UserStores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 26, 18, 8, 2, 70, DateTimeKind.Utc).AddTicks(7458), new DateTime(2025, 4, 26, 18, 8, 2, 70, DateTimeKind.Utc).AddTicks(7459) });
        }
    }
}
