﻿// <auto-generated />
using System;
using APIPreExam.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APIPreExam.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    partial class AuctionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("APIPreExam.Models.AuctionItem", b =>
                {
                    b.Property<int>("ItemNumber")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BidCustomName");

                    b.Property<string>("BidCustomePhone");

                    b.Property<int?>("BidPrice");

                    b.Property<DateTime>("BidTimeStamp");

                    b.Property<string>("ItemDescription")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("RatingPrice");

                    b.HasKey("ItemNumber");

                    b.ToTable("AuctionItems");
                });

            modelBuilder.Entity("APIPreExam.Models.Bid", b =>
                {
                    b.Property<int>("ItemNumber")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("CustomPhone")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<int>("Price");

                    b.HasKey("ItemNumber");

                    b.ToTable("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}
