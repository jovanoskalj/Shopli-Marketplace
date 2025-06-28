using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0192ebf8-3cfa-4e39-8533-0f9c520458fa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("29b167c1-63ee-402f-963c-ed40afafbf09"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4a453f15-aca5-497a-9bd1-b6cbc3def31a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("63c9b63d-d4d0-4713-86fb-3e7a26424a27"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6fe79590-3799-46b7-a7c5-a25fefd895a0"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "DisplayOrder", "IconCssClass", "IsActive", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("02361439-7da1-4785-9dae-8c0b2e16eb87"), null, new DateTime(2025, 6, 28, 9, 6, 22, 380, DateTimeKind.Utc).AddTicks(5590), "Electronic devices and gadgets", 1, "fas fa-laptop", true, null, null, "Electronics" },
                    { new Guid("02cc8088-8c16-425e-a2f6-5c6ef4a37474"), null, new DateTime(2025, 6, 28, 9, 6, 22, 380, DateTimeKind.Utc).AddTicks(5620), "Fashion and apparel", 2, "fas fa-tshirt", true, null, null, "Clothing" },
                    { new Guid("a098e585-c66a-4155-bf9c-a25eaf5a2700"), null, new DateTime(2025, 6, 28, 9, 6, 22, 380, DateTimeKind.Utc).AddTicks(5620), "Books and literature", 3, "fas fa-book", true, null, null, "Books" },
                    { new Guid("f620b5e0-0e9b-499c-ab11-50f364f41715"), null, new DateTime(2025, 6, 28, 9, 6, 22, 380, DateTimeKind.Utc).AddTicks(5620), "Home improvement and gardening", 4, "fas fa-home", true, null, null, "Home & Garden" },
                    { new Guid("fda09838-abb2-4647-b44a-f1afc0f6ba67"), null, new DateTime(2025, 6, 28, 9, 6, 22, 380, DateTimeKind.Utc).AddTicks(5630), "Sports and outdoor activities", 5, "fas fa-football-ball", true, null, null, "Sports" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("02361439-7da1-4785-9dae-8c0b2e16eb87"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("02cc8088-8c16-425e-a2f6-5c6ef4a37474"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a098e585-c66a-4155-bf9c-a25eaf5a2700"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f620b5e0-0e9b-499c-ab11-50f364f41715"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fda09838-abb2-4647-b44a-f1afc0f6ba67"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "DisplayOrder", "IconCssClass", "IsActive", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("0192ebf8-3cfa-4e39-8533-0f9c520458fa"), null, new DateTime(2025, 6, 28, 8, 54, 4, 858, DateTimeKind.Utc).AddTicks(9560), "Electronic devices and gadgets", 1, "fas fa-laptop", true, null, null, "Electronics" },
                    { new Guid("29b167c1-63ee-402f-963c-ed40afafbf09"), null, new DateTime(2025, 6, 28, 8, 54, 4, 858, DateTimeKind.Utc).AddTicks(9580), "Home improvement and gardening", 4, "fas fa-home", true, null, null, "Home & Garden" },
                    { new Guid("4a453f15-aca5-497a-9bd1-b6cbc3def31a"), null, new DateTime(2025, 6, 28, 8, 54, 4, 858, DateTimeKind.Utc).AddTicks(9580), "Books and literature", 3, "fas fa-book", true, null, null, "Books" },
                    { new Guid("63c9b63d-d4d0-4713-86fb-3e7a26424a27"), null, new DateTime(2025, 6, 28, 8, 54, 4, 858, DateTimeKind.Utc).AddTicks(9580), "Fashion and apparel", 2, "fas fa-tshirt", true, null, null, "Clothing" },
                    { new Guid("6fe79590-3799-46b7-a7c5-a25fefd895a0"), null, new DateTime(2025, 6, 28, 8, 54, 4, 858, DateTimeKind.Utc).AddTicks(9590), "Sports and outdoor activities", 5, "fas fa-football-ball", true, null, null, "Sports" }
                });
        }
    }
}
