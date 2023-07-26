using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Async_Inn.Migrations
{
    /// <inheritdoc />
    public partial class dd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RoomAmeneties_AmenetiesId",
                table: "RoomAmeneties",
                column: "AmenetiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmeneties_Amenities_AmenetiesId",
                table: "RoomAmeneties",
                column: "AmenetiesId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmeneties_Rooms_RoomId",
                table: "RoomAmeneties",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmeneties_Amenities_AmenetiesId",
                table: "RoomAmeneties");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmeneties_Rooms_RoomId",
                table: "RoomAmeneties");

            migrationBuilder.DropIndex(
                name: "IX_RoomAmeneties_AmenetiesId",
                table: "RoomAmeneties");
        }
    }
}
