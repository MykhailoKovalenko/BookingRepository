using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingRooms.Migrations
{
    public partial class ChangeRoomIsProjector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isProjector",
                table: "Rooms",
                newName: "IsProjector");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsProjector",
                table: "Rooms",
                newName: "isProjector");
        }
    }
}
