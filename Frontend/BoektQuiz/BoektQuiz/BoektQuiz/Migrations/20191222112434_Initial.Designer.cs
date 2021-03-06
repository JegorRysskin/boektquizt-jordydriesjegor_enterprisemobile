﻿// <auto-generated />
using BoektQuiz.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoektQuiz.Migrations
{
    [DbContext(typeof(BoektQuizContext))]
    [Migration("20191222112434_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("BoektQuiz.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Answer")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoundId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Answer = "Dummy Antwoord 1",
                            RoundId = 1,
                            Text = "Vraag 1"
                        },
                        new
                        {
                            Id = 2,
                            Answer = "Dummy Antwoord 2",
                            RoundId = 1,
                            Text = "Vraag 2"
                        },
                        new
                        {
                            Id = 3,
                            Answer = "Dummy Antwoord 3",
                            RoundId = 1,
                            Text = "Vraag 3"
                        },
                        new
                        {
                            Id = 4,
                            Answer = "Dummy Antwoord 4",
                            RoundId = 1,
                            Text = "Vraag 4"
                        },
                        new
                        {
                            Id = 5,
                            Answer = "Dummy Antwoord 5",
                            RoundId = 1,
                            Text = "Vraag 5"
                        },
                        new
                        {
                            Id = 6,
                            Answer = "Dummy Antwoord 6",
                            RoundId = 1,
                            Text = "Vraag 6"
                        },
                        new
                        {
                            Id = 7,
                            Answer = "Dummy Antwoord 7",
                            RoundId = 1,
                            Text = "Vraag 7"
                        },
                        new
                        {
                            Id = 8,
                            Answer = "Dummy Antwoord 8",
                            RoundId = 1,
                            Text = "Vraag 8"
                        },
                        new
                        {
                            Id = 9,
                            Answer = "Dummy Antwoord 9",
                            RoundId = 1,
                            Text = "Vraag 9"
                        },
                        new
                        {
                            Id = 10,
                            Answer = "Dummy Antwoord 10",
                            RoundId = 1,
                            Text = "Vraag 10"
                        });
                });

            modelBuilder.Entity("BoektQuiz.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rounds");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Text = "Ronde 0"
                        });
                });

            modelBuilder.Entity("BoektQuiz.Models.Question", b =>
                {
                    b.HasOne("BoektQuiz.Models.Round", null)
                        .WithMany("Questions")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
