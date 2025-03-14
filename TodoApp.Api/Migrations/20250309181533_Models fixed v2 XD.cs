using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Modelsfixedv2XD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tasks_TaskListId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TaskListId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskListId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "TaskState",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Tasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserModelId",
                table: "Tasks",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserModelId",
                table: "Tasks",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserModelId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserModelId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskState",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "TaskListId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TaskListId",
                table: "Users",
                column: "TaskListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tasks_TaskListId",
                table: "Users",
                column: "TaskListId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
