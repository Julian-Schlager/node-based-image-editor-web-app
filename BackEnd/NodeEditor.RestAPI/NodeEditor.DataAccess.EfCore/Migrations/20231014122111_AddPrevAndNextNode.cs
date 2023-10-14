using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NodeEditor.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddPrevAndNextNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreviousNodeId",
                table: "Nodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_PreviousNodeId",
                table: "Nodes",
                column: "PreviousNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Nodes_PreviousNodeId",
                table: "Nodes",
                column: "PreviousNodeId",
                principalTable: "Nodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Nodes_PreviousNodeId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_PreviousNodeId",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "PreviousNodeId",
                table: "Nodes");
        }
    }
}
