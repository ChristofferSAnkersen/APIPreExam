using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPreExam.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionItems",
                columns: table => new
                {
                    ItemNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemDescription = table.Column<string>(maxLength: 150, nullable: false),
                    RatingPrice = table.Column<int>(nullable: false),
                    BidPrice = table.Column<int>(nullable: true),
                    BidCustomName = table.Column<string>(nullable: true),
                    BidCustomePhone = table.Column<string>(nullable: true),
                    BidTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItems", x => x.ItemNumber);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    ItemNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Price = table.Column<int>(nullable: false),
                    CustomName = table.Column<string>(maxLength: 50, nullable: false),
                    CustomPhone = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.ItemNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionItems");

            migrationBuilder.DropTable(
                name: "Bids");
        }
    }
}
