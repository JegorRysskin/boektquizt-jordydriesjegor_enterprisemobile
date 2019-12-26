using System;
using System.Collections.Generic;
using System.Text;
using BoektQuiz.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace BoektQuiz.Context
{
    public class BoektQuizContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Round> Rounds { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Team> Teams { get; set; }

        private string _connectionString;

        public string ConnectionString { get => _connectionString; set { if (_connectionString == value) return; _connectionString = value; } }

        public BoektQuizContext()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;";
        }

        public BoektQuizContext(string connectionString)
        {
            _connectionString = connectionString;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(ConnectionString);
                optionsBuilder.EnableSensitiveDataLogging(true);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>().HasKey(a => new { a.Id, a.TeamId });

            modelBuilder.Entity<Round>().HasMany(r => r.Questions);

            modelBuilder.Entity<Question>().HasOne(q => q.Answer);

            modelBuilder.Entity<Team>().HasData(new Team() { Id = -1, TeamName = "Dummy Team" });

            modelBuilder.Entity<Answer>().HasData(new List<Answer>()
                {
                    new Answer() { Id = -1, AnswerString = "Dummy Antwoord 1", TeamId = -1, QuestionId = -1 },
                    new Answer() { Id = -2, AnswerString = "Dummy Antwoord 2", TeamId = -1, QuestionId = -2 },
                    new Answer() { Id = -3, AnswerString = "Dummy Antwoord 3", TeamId = -1, QuestionId = -3 },
                    new Answer() { Id = -4, AnswerString = "Dummy Antwoord 4", TeamId = -1, QuestionId = -4 },
                    new Answer() { Id = -5, AnswerString = "Dummy Antwoord 5", TeamId = -1, QuestionId = -5 },
                    new Answer() { Id = -6, AnswerString = "Dummy Antwoord 6", TeamId = -1, QuestionId = -6 },
                    new Answer() { Id = -7, AnswerString = "Dummy Antwoord 7", TeamId = -1, QuestionId = -7 },
                    new Answer() { Id = -8, AnswerString = "Dummy Antwoord 8", TeamId = -1, QuestionId = -8 },
                    new Answer() { Id = -9, AnswerString = "Dummy Antwoord 9", TeamId = -1, QuestionId = -9 },
                    new Answer() { Id = -10, AnswerString = "Dummy Antwoord 10", TeamId = -1, QuestionId = -10 }
                });

            modelBuilder.Entity<Round>().HasData(new Round() { Id = -1, Text = "Ronde 0" });

            modelBuilder.Entity<Question>().HasData(new List<Question>()
                {
                    new Question() { Id = -1, RoundId = -1, Text = "Vraag 1"  },
                    new Question() { Id = -2, RoundId = -1, Text = "Vraag 2"  },
                    new Question() { Id = -3, RoundId = -1, Text = "Vraag 3"  },
                    new Question() { Id = -4, RoundId = -1, Text = "Vraag 4" },
                    new Question() { Id = -5, RoundId = -1, Text = "Vraag 5"  },
                    new Question() { Id = -6, RoundId = -1, Text = "Vraag 6"  },
                    new Question() { Id = -7, RoundId = -1, Text = "Vraag 7"  },
                    new Question() { Id = -8, RoundId = -1, Text = "Vraag 8"  },
                    new Question() { Id = -9, RoundId = -1, Text = "Vraag 9"  },
                    new Question() { Id = -10, RoundId = -1, Text = "Vraag 10" },
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
