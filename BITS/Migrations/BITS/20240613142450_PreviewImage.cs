using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BITS.Migrations.BITS
{
    /// <inheritdoc />
    public partial class PreviewImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewImages",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewImages",
                table: "Product");
        }
    }
}
