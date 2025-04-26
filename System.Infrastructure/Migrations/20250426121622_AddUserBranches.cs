using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserBranches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBranch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBranch_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3", null, "BranchManager", "BRANCHMANAGER" });

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
                name: "IX_UserBranch_BranchId",
                table: "UserBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBranch_UserId",
                table: "UserBranch",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBranch");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8651660-81b0-4921-8292-ea1e1c59d2c7", "AQAAAAIAAYagAAAAENEaW0ch3bxI03Zc5J2i6ppgkK9fPpp5WlJIaZdKskBn5Cs3g/7SPpCyq/Dw9tKf9g==", "7f7d8bf9-59e9-4d2a-bf14-08556474cbd7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77899fe3-c734-4b05-a9e4-72034d7917e3", "AQAAAAIAAYagAAAAEOh62CpW6isu50ckULHa8EUEVyvJfEeYnBMMl6NX9IAkqUzgGoPsJfvST3erBf8/Sw==", "cee1bbfb-e844-4853-9879-791037a4d910" });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 26, 11, 34, 59, 704, DateTimeKind.Utc).AddTicks(568), new DateTime(2025, 4, 26, 11, 34, 59, 704, DateTimeKind.Utc).AddTicks(569) });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "LastModifiedOn" },
                values: new object[] { new DateTime(2025, 4, 26, 11, 34, 59, 703, DateTimeKind.Utc).AddTicks(9577), new DateTime(2025, 4, 26, 11, 34, 59, 703, DateTimeKind.Utc).AddTicks(9581) });
        }
    }
}
