using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindMatchPAS.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToProposals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ProjectProposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ProjectProposals");
        }
    }
}
