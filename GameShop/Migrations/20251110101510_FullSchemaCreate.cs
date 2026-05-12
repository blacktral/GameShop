using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShop.Migrations
{
    /// <inheritdoc />
    public partial class FullSchemaCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "game",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "external_id",
                table: "game",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "game",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "publisher",
                table: "game",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "rating",
                table: "game",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "trailer_url",
                table: "game",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "game",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "game");

            migrationBuilder.DropColumn(
                name: "external_id",
                table: "game");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "game");

            migrationBuilder.DropColumn(
                name: "publisher",
                table: "game");

            migrationBuilder.DropColumn(
                name: "rating",
                table: "game");

            migrationBuilder.DropColumn(
                name: "trailer_url",
                table: "game");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "game");
        }
    }
}
