﻿// <auto-generated />
using System;
using CinemaNS.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cinema.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230416062815_Nullables")]
    partial class Nullables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaNS.Entities.Cinema", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("DistrictId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Cinemas");
                });

            modelBuilder.Entity("CinemaNS.Entities.Country", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CinemaNS.Entities.District", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("CinemaNS.Entities.Genre", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("CinemaNS.Entities.Hall", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("CinemaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("CinemaNS.Entities.Movie", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<long?>("GenreId")
                        .HasColumnType("bigint");

                    b.Property<int>("Minutes")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("GenreId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CinemaNS.Entities.MovieActor", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("ActorId")
                        .HasColumnType("bigint");

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieActors");
                });

            modelBuilder.Entity("CinemaNS.Entities.MovieOperator", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<long?>("OperatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("OperatorId");

                    b.ToTable("MovieOperators");
                });

            modelBuilder.Entity("CinemaNS.Entities.MovieProducer", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProducerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("ProducerId");

                    b.ToTable("MovieProducers");
                });

            modelBuilder.Entity("CinemaNS.Entities.Person", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("CinemaNS.Entities.Place", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<long?>("RowId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RowId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("CinemaNS.Entities.Row", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("HallId")
                        .HasColumnType("bigint");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.ToTable("Rows");
                });

            modelBuilder.Entity("CinemaNS.Entities.Session", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("Money");

                    b.Property<long?>("HallId")
                        .HasColumnType("bigint");

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("MovieId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("CinemaNS.Entities.Cinema", b =>
                {
                    b.HasOne("CinemaNS.Entities.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId");

                    b.Navigation("District");
                });

            modelBuilder.Entity("CinemaNS.Entities.Hall", b =>
                {
                    b.HasOne("CinemaNS.Entities.Cinema", "Cinema")
                        .WithMany("Halls")
                        .HasForeignKey("CinemaId");

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("CinemaNS.Entities.Movie", b =>
                {
                    b.HasOne("CinemaNS.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("CinemaNS.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");

                    b.Navigation("Country");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("CinemaNS.Entities.MovieActor", b =>
                {
                    b.HasOne("CinemaNS.Entities.Person", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId");

                    b.HasOne("CinemaNS.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaNS.Entities.MovieOperator", b =>
                {
                    b.HasOne("CinemaNS.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.HasOne("CinemaNS.Entities.Person", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId");

                    b.Navigation("Movie");

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("CinemaNS.Entities.MovieProducer", b =>
                {
                    b.HasOne("CinemaNS.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.HasOne("CinemaNS.Entities.Person", "Producer")
                        .WithMany()
                        .HasForeignKey("ProducerId");

                    b.Navigation("Movie");

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("CinemaNS.Entities.Place", b =>
                {
                    b.HasOne("CinemaNS.Entities.Row", "Row")
                        .WithMany("Places")
                        .HasForeignKey("RowId");

                    b.Navigation("Row");
                });

            modelBuilder.Entity("CinemaNS.Entities.Row", b =>
                {
                    b.HasOne("CinemaNS.Entities.Hall", "Hall")
                        .WithMany("Rows")
                        .HasForeignKey("HallId");

                    b.Navigation("Hall");
                });

            modelBuilder.Entity("CinemaNS.Entities.Session", b =>
                {
                    b.HasOne("CinemaNS.Entities.Hall", "Hall")
                        .WithMany("Sessions")
                        .HasForeignKey("HallId");

                    b.HasOne("CinemaNS.Entities.Movie", "Movie")
                        .WithMany("Sessions")
                        .HasForeignKey("MovieId");

                    b.Navigation("Hall");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaNS.Entities.Cinema", b =>
                {
                    b.Navigation("Halls");
                });

            modelBuilder.Entity("CinemaNS.Entities.Hall", b =>
                {
                    b.Navigation("Rows");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("CinemaNS.Entities.Movie", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("CinemaNS.Entities.Row", b =>
                {
                    b.Navigation("Places");
                });
#pragma warning restore 612, 618
        }
    }
}
