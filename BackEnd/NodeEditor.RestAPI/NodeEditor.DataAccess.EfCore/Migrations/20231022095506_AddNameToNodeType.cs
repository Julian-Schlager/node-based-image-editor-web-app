using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NodeEditor.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToNodeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "NodeTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "NodeTypes");
        }
    }
}
