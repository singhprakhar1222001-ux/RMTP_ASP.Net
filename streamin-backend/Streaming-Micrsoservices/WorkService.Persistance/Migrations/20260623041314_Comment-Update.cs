using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkService.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CommentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkHistory");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "Workitem",
                newName: "WorkStatus");

            migrationBuilder.AddColumn<bool>(
                name: "IsOverDue",
                table: "Workitem",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Workitem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkitemId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Workitem_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Workitem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Workitem_WorkitemId",
                        column: x => x.WorkitemId,
                        principalTable: "Workitem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    TypeOfEvent = table.Column<string>(type: "text", nullable: false),
                    IsError = table.Column<bool>(type: "boolean", nullable: false),
                    OccuredOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WorkId",
                table: "Comments",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WorkitemId",
                table: "Comments",
                column: "WorkitemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "IsOverDue",
                table: "Workitem");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Workitem");

            migrationBuilder.RenameColumn(
                name: "WorkStatus",
                table: "Workitem",
                newName: "comment");

            migrationBuilder.CreateTable(
                name: "WorkHistory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Assignecomment = table.Column<string>(type: "text", nullable: false),
                    AssignedId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<int>(type: "integer", nullable: false),
                    LogTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ManagerComment = table.Column<string>(type: "text", nullable: false),
                    PreviousState = table.Column<int>(type: "integer", nullable: false),
                    WorkItemID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHistory", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkHistory_Workitem_WorkItemID",
                        column: x => x.WorkItemID,
                        principalTable: "Workitem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistory_Assignecomment",
                table: "WorkHistory",
                column: "Assignecomment");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistory_WorkItemID",
                table: "WorkHistory",
                column: "WorkItemID");
        }
    }
}
