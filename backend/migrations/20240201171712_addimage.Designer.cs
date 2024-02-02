﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240201171712_addimage")]
    partial class addimage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Attractie", b =>
                {
                    b.Property<int>("AttractieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AfbeeldingUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Capaciteit")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<int>("Duur")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AttractieId");

                    b.ToTable("Attracties");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Gebruikersnaam")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VirtualQueue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AttractieId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AttractionId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EntryTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AttractieId");

                    b.HasIndex("UserId");

                    b.ToTable("VirtualQueue");
                });

            modelBuilder.Entity("VirtualQueue", b =>
                {
                    b.HasOne("Attractie", "Attractie")
                        .WithMany("VirtualQueue")
                        .HasForeignKey("AttractieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("VirtualQueue")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attractie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Attractie", b =>
                {
                    b.Navigation("VirtualQueue");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("VirtualQueue");
                });
#pragma warning restore 612, 618
        }
    }
}