using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gNdgd.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartTableColumnUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserI",
                table: "ShoppingCarts",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ShoppingCarts",
                newName: "UserI");
        }
    }
}
