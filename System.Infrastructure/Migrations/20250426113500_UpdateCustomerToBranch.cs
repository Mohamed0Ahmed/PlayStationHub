using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerToBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPoints_Customers_CustomerId",
                table: "CustomerPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Stores_StoreId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Stores_StoreId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPoints_CustomerId",
                table: "CustomerPoints");

            migrationBuilder.RenameColumn(
                name: "StoreName",
                table: "Stores",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_StoreName",
                table: "Stores",
                newName: "IX_Stores_Name");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Customers",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_StoreId_PhoneNumber",
                table: "Customers",
                newName: "IX_Customers_BranchId_PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Rewards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "PointsSettings",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", null, "Owner", "OWNER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8651660-81b0-4921-8292-ea1e1c59d2c7", "AQAAAAIAAYagAAAAENEaW0ch3bxI03Zc5J2i6ppgkK9fPpp5WlJIaZdKskBn5Cs3g/7SPpCyq/Dw9tKf9g==", "7f7d8bf9-59e9-4d2a-bf14-08556474cbd7" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2", 0, "77899fe3-c734-4b05-a9e4-72034d7917e3", "owner@system.com", true, false, null, "OWNER@SYSTEM.COM", "OWNER@SYSTEM.COM", "AQAAAAIAAYagAAAAEOh62CpW6isu50ckULHa8EUEVyvJfEeYnBMMl6NX9IAkqUzgGoPsJfvST3erBf8/Sw==", null, false, "cee1bbfb-e844-4853-9879-791037a4d910", false, "owner@system.com" });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedOn", "DeletedOn", "IsDeleted", "IsHidden", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, "123 Main St", null, new DateTime(2025, 4, 26, 11, 34, 59, 703, DateTimeKind.Utc).AddTicks(9577), null, false, false, null, new DateTime(2025, 4, 26, 11, 34, 59, 703, DateTimeKind.Utc).AddTicks(9581), "Main Store" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "2" });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "BranchName", "CreatedBy", "CreatedOn", "DeletedOn", "IsDeleted", "IsHidden", "LastModifiedBy", "LastModifiedOn", "StoreId" },
                values: new object[] { 1, "Branch 1", null, new DateTime(2025, 4, 26, 11, 34, 59, 704, DateTimeKind.Utc).AddTicks(568), null, false, false, null, new DateTime(2025, 4, 26, 11, 34, 59, 704, DateTimeKind.Utc).AddTicks(569), 1 });

            migrationBuilder.InsertData(
                table: "UserStores",
                columns: new[] { "Id", "StoreId", "UserId" },
                values: new object[] { 1, 1, "2" });

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_StoreId",
                table: "Rewards",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId",
                table: "Products",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsSettings_StoreId",
                table: "PointsSettings",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPoints_CustomerId_BranchId",
                table: "CustomerPoints",
                columns: new[] { "CustomerId", "BranchId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPoints_Customers_CustomerId",
                table: "CustomerPoints",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Branches_BranchId",
                table: "Customers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Stores_StoreId",
                table: "Guests",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PointsSettings_Stores_StoreId",
                table: "PointsSettings",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rewards_Stores_StoreId",
                table: "Rewards",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPoints_Customers_CustomerId",
                table: "CustomerPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Branches_BranchId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Stores_StoreId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_PointsSettings_Stores_StoreId",
                table: "PointsSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Rewards_Stores_StoreId",
                table: "Rewards");

            migrationBuilder.DropIndex(
                name: "IX_Rewards_StoreId",
                table: "Rewards");

            migrationBuilder.DropIndex(
                name: "IX_Products_StoreId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_PointsSettings_StoreId",
                table: "PointsSettings");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPoints_CustomerId_BranchId",
                table: "CustomerPoints");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserStores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PointsSettings");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Stores",
                newName: "StoreName");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_Name",
                table: "Stores",
                newName: "IX_Stores_StoreName");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Customers",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BranchId_PhoneNumber",
                table: "Customers",
                newName: "IX_Customers_StoreId_PhoneNumber");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6da9a41b-13db-4f9e-b789-bc4a8e479bd1", "AQAAAAIAAYagAAAAEOQSi4+Xi2sg4k3+Pon71UJvFHLQdb4wW6Y5bDFXDgrcHLn2U6ITFbJOavX3wYlcpQ==", "ced23e76-eaf3-4570-a941-3bf95aae7cc2" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPoints_CustomerId",
                table: "CustomerPoints",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPoints_Customers_CustomerId",
                table: "CustomerPoints",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Stores_StoreId",
                table: "Customers",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Stores_StoreId",
                table: "Guests",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
