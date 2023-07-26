﻿// <auto-generated />
using Async_Inn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Async_Inn.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    [Migration("20230725213022_dd")]
    partial class dd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Async_Inn.Model.Amenities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "AC"
                        },
                        new
                        {
                            Id = 2,
                            Name = "coffeeBar"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Fridge"
                        });
                });

            modelBuilder.Entity("Async_Inn.Model.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "NY",
                            Country = "US",
                            Name = "cont",
                            Phone = "1233442",
                            State = "NY",
                            StreetAddres = "23.st"
                        },
                        new
                        {
                            Id = 2,
                            City = "LA",
                            Country = "US",
                            Name = "res",
                            Phone = "1233444442",
                            State = "LA",
                            StreetAddres = "25.st"
                        },
                        new
                        {
                            Id = 3,
                            City = "TX",
                            Country = "US",
                            Name = "hunt",
                            Phone = "123223442",
                            State = "TX",
                            StreetAddres = "28.st"
                        });
                });

            modelBuilder.Entity("Async_Inn.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Layout = 1,
                            Name = "honey"
                        },
                        new
                        {
                            Id = 2,
                            Layout = 2,
                            Name = "red"
                        },
                        new
                        {
                            Id = 3,
                            Layout = 3,
                            Name = "white"
                        });
                });

            modelBuilder.Entity("Async_Inn.Model.RoomAmeneties", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("AmenetiesId")
                        .HasColumnType("int");

                    b.HasKey("RoomId", "AmenetiesId");

                    b.HasIndex("AmenetiesId");

                    b.ToTable("RoomAmeneties");
                });

            modelBuilder.Entity("Async_Inn.Model.RoomAmeneties", b =>
                {
                    b.HasOne("Async_Inn.Model.Amenities", "Ameneties")
                        .WithMany()
                        .HasForeignKey("AmenetiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Async_Inn.Model.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ameneties");

                    b.Navigation("Room");
                });
#pragma warning restore 612, 618
        }
    }
}
