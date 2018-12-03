using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPreExam.Migrations
{
    public partial class DataNotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionItems",
                columns: table => new
                {
                    AuctionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemNumber = table.Column<int>(maxLength: 7, nullable: false),
                    ItemDescription = table.Column<string>(maxLength: 256, nullable: true),
                    RatingPrice = table.Column<int>(nullable: false),
                    BidPrice = table.Column<int>(nullable: false),
                    BidCustomName = table.Column<string>(maxLength: 100, nullable: true),
                    BidCustomePhone = table.Column<string>(maxLength: 20, nullable: true),
                    BidTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItems", x => x.AuctionId);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    BidId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemNumber = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    CustomName = table.Column<string>(maxLength: 30, nullable: false),
                    CustomPhone = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.BidId);
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
