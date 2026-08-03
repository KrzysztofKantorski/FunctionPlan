using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Meetings_OrganizerId",
                table: "Meetings",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Users_OrganizerId",
                table: "Meetings",
                column: "OrganizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Users_OrganizerId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_OrganizerId",
                table: "Meetings");
        }
    }
}
