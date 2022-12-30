using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataPointsDaily",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HighestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LowestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OpeningPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPointsDaily", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataPointsIntraDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HighestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LowestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OpeningPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPointsIntraDay", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataPointsDaily");

            migrationBuilder.DropTable(
                name: "DataPointsIntraDay");
        }
    }
}
