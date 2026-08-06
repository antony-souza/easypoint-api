using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationIdToStoreUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "StoreUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE "StoreUsers" AS store_users
                SET "OrganizationId" = stores."OrganizationId"
                FROM "Stores" AS stores
                WHERE stores."Id" = store_users."StoreId";
                """);

            migrationBuilder.Sql("""
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM "StoreUsers"
                        WHERE "OrganizationId" IS NULL
                    ) THEN
                        RAISE EXCEPTION
                            'There are store-user links without a valid store organization.';
                    END IF;
                END $$;
                """);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "StoreUsers",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreUsers_OrganizationId",
                table: "StoreUsers",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreUsers_Organizations_OrganizationId",
                table: "StoreUsers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreUsers_Organizations_OrganizationId",
                table: "StoreUsers");

            migrationBuilder.DropIndex(
                name: "IX_StoreUsers_OrganizationId",
                table: "StoreUsers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StoreUsers");
        }
    }
}
