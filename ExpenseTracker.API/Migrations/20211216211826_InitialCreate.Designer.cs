// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseTracker.API.Migrations
{
    [DbContext(typeof(ExpenseDb))]
    [Migration("20211216211826_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ExpenseItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExpenseItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 24.14m,
                            Date = new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Toilet Paper"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 799.49m,
                            Date = new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "New TV"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 94.67m,
                            Date = new DateTime(2021, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Cable Internet"
                        },
                        new
                        {
                            Id = 4,
                            Amount = 450.00m,
                            Date = new DateTime(2021, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "New Desk (Wooden)"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
