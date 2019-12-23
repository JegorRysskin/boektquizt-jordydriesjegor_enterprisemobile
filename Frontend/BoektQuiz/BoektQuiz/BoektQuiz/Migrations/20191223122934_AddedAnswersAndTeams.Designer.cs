﻿// <auto-generated />
using BoektQuiz.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoektQuiz.Migrations
{
    [DbContext(typeof(BoektQuizContext))]
    [Migration("20191223122934_AddedAnswersAndTeams")]
    partial class AddedAnswersAndTeams
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("BoektQuiz.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeamId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnswerString")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id", "TeamId");

                    b.HasIndex("QuestionId")
                        .IsUnique();

                    b.HasIndex("TeamId");

                    b.ToTable("Answers");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 1",
                            QuestionId = -1
                        },
                        new
                        {
                            Id = -2,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 2",
                            QuestionId = -2
                        },
                        new
                        {
                            Id = -3,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 3",
                            QuestionId = -3
                        },
                        new
                        {
                            Id = -4,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 4",
                            QuestionId = -4
                        },
                        new
                        {
                            Id = -5,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 5",
                            QuestionId = -5
                        },
                        new
                        {
                            Id = -6,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 6",
                            QuestionId = -6
                        },
                        new
                        {
                            Id = -7,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 7",
                            QuestionId = -7
                        },
                        new
                        {
                            Id = -8,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 8",
                            QuestionId = -8
                        },
                        new
                        {
                            Id = -9,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 9",
                            QuestionId = -9
                        },
                        new
                        {
                            Id = -10,
                            TeamId = -1,
                            AnswerString = "Dummy Antwoord 10",
                            QuestionId = -10
                        });
                });

            modelBuilder.Entity("BoektQuiz.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

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
                            Id = -1,
                            RoundId = -1,
                            Text = "Vraag 1"
                        },
                        new
                        {
                            Id = -2,
                            RoundId = -1,
                            Text = "Vraag 2"
                        },
                        new
                        {
                            Id = -3,
                            RoundId = -1,
                            Text = "Vraag 3"
                        },
                        new
                        {
                            Id = -4,
                            RoundId = -1,
                            Text = "Vraag 4"
                        },
                        new
                        {
                            Id = -5,
                            RoundId = -1,
                            Text = "Vraag 5"
                        },
                        new
                        {
                            Id = -6,
                            RoundId = -1,
                            Text = "Vraag 6"
                        },
                        new
                        {
                            Id = -7,
                            RoundId = -1,
                            Text = "Vraag 7"
                        },
                        new
                        {
                            Id = -8,
                            RoundId = -1,
                            Text = "Vraag 8"
                        },
                        new
                        {
                            Id = -9,
                            RoundId = -1,
                            Text = "Vraag 9"
                        },
                        new
                        {
                            Id = -10,
                            RoundId = -1,
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
                            Id = -1,
                            Text = "Ronde 0"
                        });
                });

            modelBuilder.Entity("BoektQuiz.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            TeamName = "Dummy Team"
                        });
                });

            modelBuilder.Entity("BoektQuiz.Models.Answer", b =>
                {
                    b.HasOne("BoektQuiz.Models.Question", null)
                        .WithOne("Answer")
                        .HasForeignKey("BoektQuiz.Models.Answer", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoektQuiz.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
