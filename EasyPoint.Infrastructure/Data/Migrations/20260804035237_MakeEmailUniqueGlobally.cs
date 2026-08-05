using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPoint.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmailUniqueGlobally : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StoreId_NormalizedEmail",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StoreId_NormalizedEmail",
                table: "AspNetUsers",
                columns: new[] { "StoreId", "NormalizedEmail" },
                unique: true);
        }
    }
}
