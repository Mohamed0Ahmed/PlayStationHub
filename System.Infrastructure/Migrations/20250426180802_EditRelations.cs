using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Branches_BranchId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Customers_CustomerId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Guests_GuestId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Rooms_RoomId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Rooms_RoomId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Branches_BranchId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Products_StoreId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SessionToken",
                table: "Guests");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserStores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserStores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserStores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserStores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "UserStores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "UserStores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "UserStores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Guests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedOn", "IsDeleted", "IsHidden", "LastModifiedBy", "LastModifiedOn" },
                values: new object[] { null, new DateTime(2025, 4, 26, 18, 8, 2, 70, DateTimeKind.Utc).AddTicks(7458), null, false, false, null, new DateTime(2025, 4, 26, 18, 8, 2, 70, DateTimeKind.Utc).AddTicks(7459) });

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Branches_BranchId",
                table: "Guests",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Customers_CustomerId",
                table: "HelpRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Guests_GuestId",
                table: "HelpRequests",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Rooms_RoomId",
                table: "HelpRequests",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Rooms_RoomId",
                table: "Orders",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Branches_BranchId",
                table: "Rooms",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Branches_BranchId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Customers_CustomerId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Guests_GuestId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Rooms_RoomId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Rooms_RoomId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Branches_BranchId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserStores");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserStores");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserStores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserStores");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "UserStores");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "UserStores");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "UserStores");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionToken",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb9b3fdf-ddd3-4578-b47f-e7d0161de54e", "AQAAAAIAAYagAAAAEGv2qv1mUks9wso6dJQjpPwilf095v3X7aZ3tDf6rFEHGfnqviw0791OSwI5MFi/Ww==", "6ac43906-de4f-4904-b12f-f166101cb496" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59492b14-351f-4df9-9564-a986a0eae145", "AQAAAAIAAYagAAAAEC9FskAx1agy2iM7Qi+j5V45PDb/YISVnGQIVQ6XoMLi9bEoa9BJ6Ad9SbdGTxU0/w==", "dbf0c21c-4bbc-4409-a36c-980a331e2c57" });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 26, 12, 16, 22, 61, DateTimeKind.Utc).AddTicks(8734), new DateTime(2025, 4, 26, 12, 16, 22, 61, DateTimeKind.Utc).AddTicks(8735) });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 26, 12, 16, 22, 61, DateTimeKind.Utc).AddTicks(8646), new DateTime(2025, 4, 26, 12, 16, 22, 61, DateTimeKind.Utc).AddTicks(8651) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId",
                table: "Products",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Branches_BranchId",
                table: "Guests",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Customers_CustomerId",
                table: "HelpRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Guests_GuestId",
                table: "HelpRequests",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Rooms_RoomId",
                table: "HelpRequests",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Rooms_RoomId",
                table: "Orders",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Branches_BranchId",
                table: "Rooms",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
