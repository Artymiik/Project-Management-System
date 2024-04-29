using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pms_cs.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeAppUserId",
                table: "Reports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeAppUserId",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
