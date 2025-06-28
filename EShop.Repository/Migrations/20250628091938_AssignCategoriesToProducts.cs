using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AssignCategoriesToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("000696a4-c616-469b-824d-7376830dc668"), null, new DateTime(2025, 6, 28, 9, 19, 38, 471, DateTimeKind.Utc).AddTicks(3640), "Electronic devices and gadgets", 1, "fas fa-laptop", true, null, null, "Electronics" },
                    { new Guid("1345a1ed-3884-49e5-9f2c-aebbc2a32d86"), null, new DateTime(2025, 6, 28, 9, 19, 38, 471, DateTimeKind.Utc).AddTicks(3680), "Books and literature", 3, "fas fa-book", true, null, null, "Books" },
                    { new Guid("af78b94f-4367-4f0d-9b00-1b676640f0af"), null, new DateTime(2025, 6, 28, 9, 19, 38, 471, DateTimeKind.Utc).AddTicks(3680), "Sports and outdoor activities", 5, "fas fa-football-ball", true, null, null, "Sports" },
                    { new Guid("b4c2b448-cfc4-46f0-b9a1-74411da04da3"), null, new DateTime(2025, 6, 28, 9, 19, 38, 471, DateTimeKind.Utc).AddTicks(3680), "Home improvement and gardening", 4, "fas fa-home", true, null, null, "Home & Garden" },
                    { new Guid("d3776c65-65ee-49ec-a098-2e0d52097e9e"), null, new DateTime(2025, 6, 28, 9, 19, 38, 471, DateTimeKind.Utc).AddTicks(3670), "Fashion and apparel", 2, "fas fa-tshirt", true, null, null, "Clothing" }
                });

            // Assign existing products to categories based on their names/descriptions
            // Electronics category
            migrationBuilder.Sql(@"
                UPDATE Products 
                SET CategoryId = '000696a4-c616-469b-824d-7376830dc668' 
                WHERE CategoryId IS NULL 
                AND (LOWER(ProductName) LIKE '%laptop%' 
                    OR LOWER(ProductName) LIKE '%computer%' 
                    OR LOWER(ProductName) LIKE '%phone%' 
                    OR LOWER(ProductName) LIKE '%tablet%' 
                    OR LOWER(ProductName) LIKE '%electronic%'
                    OR LOWER(ProductName) LIKE '%gaming%'
                    OR LOWER(ProductName) LIKE '%tech%'
                    OR LOWER(ProductDescription) LIKE '%electronic%'
                    OR LOWER(ProductDescription) LIKE '%digital%'
                    OR LOWER(ProductDescription) LIKE '%tech%')
            ");

            // Books category  
            migrationBuilder.Sql(@"
                UPDATE Products 
                SET CategoryId = '1345a1ed-3884-49e5-9f2c-aebbc2a32d86' 
                WHERE CategoryId IS NULL 
                AND (LOWER(ProductName) LIKE '%book%' 
                    OR LOWER(ProductName) LIKE '%novel%' 
                    OR LOWER(ProductName) LIKE '%guide%'
                    OR LOWER(ProductName) LIKE '%manual%'
                    OR LOWER(ProductDescription) LIKE '%book%'
                    OR LOWER(ProductDescription) LIKE '%read%'
                    OR LOWER(ProductDescription) LIKE '%author%')
            ");

            // Clothing category
            migrationBuilder.Sql(@"
                UPDATE Products 
                SET CategoryId = 'd3776c65-65ee-49ec-a098-2e0d52097e9e' 
                WHERE CategoryId IS NULL 
                AND (LOWER(ProductName) LIKE '%shirt%' 
                    OR LOWER(ProductName) LIKE '%dress%' 
                    OR LOWER(ProductName) LIKE '%pants%'
                    OR LOWER(ProductName) LIKE '%jacket%'
                    OR LOWER(ProductName) LIKE '%shoes%'
                    OR LOWER(ProductName) LIKE '%clothing%'
                    OR LOWER(ProductDescription) LIKE '%wear%'
                    OR LOWER(ProductDescription) LIKE '%fashion%'
                    OR LOWER(ProductDescription) LIKE '%clothing%')
            ");

            // Sports category
            migrationBuilder.Sql(@"
                UPDATE Products 
                SET CategoryId = 'af78b94f-4367-4f0d-9b00-1b676640f0af' 
                WHERE CategoryId IS NULL 
                AND (LOWER(ProductName) LIKE '%sport%' 
                    OR LOWER(ProductName) LIKE '%ball%' 
                    OR LOWER(ProductName) LIKE '%fitness%'
                    OR LOWER(ProductName) LIKE '%exercise%'
                    OR LOWER(ProductName) LIKE '%outdoor%'
                    OR LOWER(ProductDescription) LIKE '%sport%'
                    OR LOWER(ProductDescription) LIKE '%fitness%'
                    OR LOWER(ProductDescription) LIKE '%exercise%')
            ");

            // Home & Garden category
            migrationBuilder.Sql(@"
                UPDATE Products 
                SET CategoryId = 'b4c2b448-cfc4-46f0-b9a1-74411da04da3' 
                WHERE CategoryId IS NULL 
                AND (LOWER(ProductName) LIKE '%home%' 
                    OR LOWER(ProductName) LIKE '%garden%' 
                    OR LOWER(ProductName) LIKE '%kitchen%'
                    OR LOWER(ProductName) LIKE '%furniture%'
                    OR LOWER(ProductName) LIKE '%decor%'
                    OR LOWER(ProductDescription) LIKE '%home%'
                    OR LOWER(ProductDescription) LIKE '%garden%'
                    OR LOWER(ProductDescription) LIKE '%house%')
            ");

            // Assign any remaining products to Electronics as default
            migrationBuilder.Sql(@"
                UPDATE Products 
                SET CategoryId = '000696a4-c616-469b-824d-7376830dc668' 
                WHERE CategoryId IS NULL
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("000696a4-c616-469b-824d-7376830dc668"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1345a1ed-3884-49e5-9f2c-aebbc2a32d86"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("af78b94f-4367-4f0d-9b00-1b676640f0af"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b4c2b448-cfc4-46f0-b9a1-74411da04da3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d3776c65-65ee-49ec-a098-2e0d52097e9e"));

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
    }
}
