using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaInAction.OrderingService.Migrations
{
    public partial class AddedProductCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "OrderItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "OrderItems");
        }
    }
}
