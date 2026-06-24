using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkService.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OutboxTypeDuplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfEvent",
                table: "OutboxMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeOfEvent",
                table: "OutboxMessages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
