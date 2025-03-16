﻿// <auto-generated />
using System;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(DiscographyContext))]
    [Migration("20250316111434_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BusinessObjects.Entity.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DebutYear")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("RecordId")
                        .HasColumnType("integer");

                    b.Property<int?>("TrackId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RecordId");

                    b.HasIndex("TrackId");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("BusinessObjects.Entity.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("BusinessObjects.Entity.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("RecordTrack", b =>
                {
                    b.Property<int>("RecordsId")
                        .HasColumnType("integer");

                    b.Property<int>("TracksId")
                        .HasColumnType("integer");

                    b.HasKey("RecordsId", "TracksId");

                    b.HasIndex("TracksId");

                    b.ToTable("RecordTrack");
                });

            modelBuilder.Entity("BusinessObjects.Entity.Artist", b =>
                {
                    b.HasOne("BusinessObjects.Entity.Record", null)
                        .WithMany("Artists")
                        .HasForeignKey("RecordId");

                    b.HasOne("BusinessObjects.Entity.Track", null)
                        .WithMany("Artists")
                        .HasForeignKey("TrackId");
                });

            modelBuilder.Entity("RecordTrack", b =>
                {
                    b.HasOne("BusinessObjects.Entity.Record", null)
                        .WithMany()
                        .HasForeignKey("RecordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Entity.Track", null)
                        .WithMany()
                        .HasForeignKey("TracksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObjects.Entity.Record", b =>
                {
                    b.Navigation("Artists");
                });

            modelBuilder.Entity("BusinessObjects.Entity.Track", b =>
                {
                    b.Navigation("Artists");
                });
#pragma warning restore 612, 618
        }
    }
}
