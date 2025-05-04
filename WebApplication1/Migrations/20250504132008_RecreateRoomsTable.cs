using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class RecreateRoomsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Drop the FK from Reservations → Rooms (adjust the constraint name if yours is different)
            migrationBuilder.Sql(@"
        IF OBJECT_ID(N'dbo.Reservations_Rooms_FK','F') IS NOT NULL
            ALTER TABLE dbo.Reservations DROP CONSTRAINT [FK_Reservations_Rooms_RoomId];
    ");
            // 2) (Optionally) drop the index on Reservations.RoomId if EF created one:
            migrationBuilder.Sql(@"
        IF EXISTS (
          SELECT 1 FROM sys.indexes 
          WHERE name = 'IX_Reservations_RoomId' AND object_id = OBJECT_ID('dbo.Reservations')
        )
          DROP INDEX IX_Reservations_RoomId ON dbo.Reservations;
    ");

            // 3) Now drop the Rooms table
            migrationBuilder.DropTable(name: "Rooms");

            // 4) Recreate Rooms exactly as your model defines
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfBeds = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    Quality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            // 5) Re-create the FK from Reservations → Rooms
            migrationBuilder.Sql(@"
        ALTER TABLE dbo.Reservations
        ADD CONSTRAINT [FK_Reservations_Rooms_RoomId]
        FOREIGN KEY (RoomId)
        REFERENCES dbo.Rooms (RoomId)
        ON DELETE CASCADE;
    ");
        }

    }
}

