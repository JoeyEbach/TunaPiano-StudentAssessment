﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TunaPianoStudentAssessment.Migrations
{
    [DbContext(typeof(TunaPianoStudentAssessmentDbContext))]
    [Migration("20240305023358_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TunaPianoStudentAssessment.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 34,
                            Bio = "Greatest rapper of all time",
                            Name = "Kanye West"
                        },
                        new
                        {
                            Id = 2,
                            Age = 46,
                            Bio = "90s rock band",
                            Name = "ColdPlay"
                        },
                        new
                        {
                            Id = 3,
                            Age = 64,
                            Bio = "King of Pop",
                            Name = "Michael Jackson"
                        },
                        new
                        {
                            Id = 4,
                            Age = 50,
                            Bio = "Country Superstar",
                            Name = "Keith Urban"
                        });
                });

            modelBuilder.Entity("TunaPianoStudentAssessment.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Hip Hop"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Rock"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Pop"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Country"
                        });
                });

            modelBuilder.Entity("TunaPianoStudentAssessment.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Album")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ArtistId")
                        .HasColumnType("integer");

                    b.Property<int?>("GenreId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Length")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("Songs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Album = "Black Skinhead",
                            ArtistId = 1,
                            Length = 2.32m,
                            Title = "Power"
                        },
                        new
                        {
                            Id = 2,
                            Album = "XO",
                            ArtistId = 2,
                            Length = 3.43m,
                            Title = "ColdPlay"
                        },
                        new
                        {
                            Id = 3,
                            Album = "Scream",
                            ArtistId = 3,
                            Length = 2.59m,
                            Title = "Scream"
                        },
                        new
                        {
                            Id = 4,
                            Album = "Tonight",
                            ArtistId = 4,
                            Length = 2.35m,
                            Title = "Kiss A Girl"
                        });
                });

            modelBuilder.Entity("TunaPianoStudentAssessment.Models.Song", b =>
                {
                    b.HasOne("TunaPianoStudentAssessment.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TunaPianoStudentAssessment.Models.Genre", null)
                        .WithMany("Songs")
                        .HasForeignKey("GenreId");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("TunaPianoStudentAssessment.Models.Genre", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
