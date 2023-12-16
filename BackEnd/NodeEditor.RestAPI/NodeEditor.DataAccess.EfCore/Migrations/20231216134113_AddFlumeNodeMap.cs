using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NodeEditor.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddFlumeNodeMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlumeNodeMap",
                table: "NodesGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlumeNodeMap",
                table: "NodesGroups");
        }
    }
}
