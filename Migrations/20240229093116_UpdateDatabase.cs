using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace h5blazorwebapp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CPR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CprNr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CPR__3214EC07194505D0", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Item = table.Column<byte[]>(type: "varbinary(2049)", maxLength: 2049, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TodoList__3214EC07B2FCC953", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CPR");

            migrationBuilder.DropTable(
                name: "TodoList");
        }
    }
}
