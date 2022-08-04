using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolDraftWebsite.Migrations
{
    public partial class changePandCRevenueToTotalCollected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revenue",
                table: "PandCUniformSales");

            migrationBuilder.AddColumn<long>(
                name: "TotalCollected",
                table: "PandCUniformSales",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "CHECK MATERIAL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCollected",
                table: "PandCUniformSales");

            migrationBuilder.AddColumn<long>(
                name: "Revenue",
                table: "PandCUniformSales",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "CKECK MATERIAL");
        }
    }
}
