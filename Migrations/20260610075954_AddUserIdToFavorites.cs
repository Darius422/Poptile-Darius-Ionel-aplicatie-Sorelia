using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyHealthStore.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FavoriteItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FavoriteItems");
        }
    }
}
