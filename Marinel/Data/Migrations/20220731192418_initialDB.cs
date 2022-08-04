using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolDraftWebsite.Migrations
{
    public partial class initialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PinkAndCheckUniformPrice = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassFeeInfoItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    NumberEnrolled = table.Column<int>(nullable: false),
                    NumberPaid = table.Column<int>(nullable: false),
                    AmountReceived = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassFeeInfoItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedingInfoItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    TotalCollected = table.Column<long>(nullable: false),
                    NumberOfStudents = table.Column<int>(nullable: false),
                    Revenue = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingInfoItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ClassId = table.Column<int>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedingExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseAmount = table.Column<long>(nullable: false),
                    ExpenseReason = table.Column<string>(nullable: true),
                    FeedingInfoItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedingExpenses_FeedingInfoItems_FeedingInfoItemId",
                        column: x => x.FeedingInfoItemId,
                        principalTable: "FeedingInfoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    InventoryTypeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CostPerUnit = table.Column<long>(nullable: false),
                    SellingPrice = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryItems_InventoryTypes_InventoryTypeId",
                        column: x => x.InventoryTypeId,
                        principalTable: "InventoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PandCUniformSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePaid = table.Column<DateTime>(nullable: false),
                    ReceivedDate = table.Column<DateTime>(nullable: false),
                    CheckYardsQuantity = table.Column<int>(nullable: false),
                    PinkYardsQuantity = table.Column<int>(nullable: false),
                    Revenue = table.Column<long>(nullable: false),
                    PaidInFull = table.Column<bool>(nullable: false),
                    Received = table.Column<bool>(nullable: false),
                    SeamstressPaid = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PandCUniformSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PandCUniformSales_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    NoOfBooksSold = table.Column<int>(nullable: false),
                    Revenue = table.Column<long>(nullable: false),
                    InventoryItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookSales_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniformSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    StockDate = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Revenue = table.Column<long>(nullable: false),
                    PaidInFull = table.Column<bool>(nullable: false),
                    Received = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    InventoryItemId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniformSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniformSales_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniformSales_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name", "PinkAndCheckUniformPrice" },
                values: new object[,]
                {
                    { 1, "Creche", 95L },
                    { 15, "JHS 3", 110L },
                    { 14, "JHS 2", 110L },
                    { 13, "JHS 1", 110L },
                    { 12, "CLASS 6", 105L },
                    { 11, "CLASS 5", 105L },
                    { 9, "CLASS 3", 105L },
                    { 10, "CLASS 4", 105L },
                    { 7, "CLASS 1", 105L },
                    { 6, "KG2", 100L },
                    { 5, "KG1", 100L },
                    { 4, "Nursery 2B", 100L },
                    { 3, "Nursery 2A", 100L },
                    { 2, "Nursery", 100L },
                    { 8, "CLASS 2", 105L }
                });

            migrationBuilder.InsertData(
                table: "ExpenseType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Feeding" });

            migrationBuilder.InsertData(
                table: "InventoryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Material" },
                    { 1, "Book" },
                    { 2, "Uniform" },
                    { 4, "Other" }
                });

            migrationBuilder.InsertData(
                table: "InventoryItems",
                columns: new[] { "Id", "CostPerUnit", "InventoryTypeId", "Name", "Quantity", "SellingPrice" },
                values: new object[,]
                {
                    { 1, 10L, 1, "SMALL NOTEBOOK", 100, 10L },
                    { 2, 10L, 1, "MEDIUM NOTEBOOK", 100, 10L },
                    { 3, 10L, 1, "EXERCISE BOOKS", 100, 10L },
                    { 4, 10L, 2, "CRECHE UNIFORM", 100, 10L },
                    { 5, 10L, 2, "WEDNESDAY WEAR UNIFORM", 100, 10L },
                    { 6, 10L, 2, "FRIDAY WEAR UNIFORM", 100, 10L },
                    { 7, 0L, 3, "PINK MATERIAL", 100, 0L },
                    { 8, 0L, 3, "CKECK MATERIAL", 100, 0L }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ClassId", "DOB", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elijah" },
                    { 4, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TJ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookSales_InventoryItemId",
                table: "BookSales",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingExpenses_FeedingInfoItemId",
                table: "FeedingExpenses",
                column: "FeedingInfoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_InventoryTypeId",
                table: "InventoryItems",
                column: "InventoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PandCUniformSales_StudentId",
                table: "PandCUniformSales",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_UniformSales_InventoryItemId",
                table: "UniformSales",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UniformSales_StudentId",
                table: "UniformSales",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookSales");

            migrationBuilder.DropTable(
                name: "ClassFeeInfoItems");

            migrationBuilder.DropTable(
                name: "ExpenseType");

            migrationBuilder.DropTable(
                name: "FeedingExpenses");

            migrationBuilder.DropTable(
                name: "PandCUniformSales");

            migrationBuilder.DropTable(
                name: "UniformSales");

            migrationBuilder.DropTable(
                name: "FeedingInfoItems");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "InventoryTypes");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
