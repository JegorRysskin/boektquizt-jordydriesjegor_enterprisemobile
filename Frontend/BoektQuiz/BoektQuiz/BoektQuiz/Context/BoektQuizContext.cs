using System.Collections.Generic;
using BoektQuiz.Models;
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
            modelBuilder.Entity<Answer>().HasKey(a => new { a.Id, a.QuestionId });

            modelBuilder.Entity<Question>().HasKey(q => new { q.Id, q.RoundId });

            modelBuilder.Entity<Round>().HasMany(r => r.Questions);

            modelBuilder.Entity<Team>().HasMany(t => t.Answers);

            modelBuilder.Entity<Answer>().HasData(new List<Answer>()
                {
                    new Answer() { Id = -1, AnswerString = "Dummy Antwoord 1", QuestionId = -1 },
                    new Answer() { Id = -2, AnswerString = "Dummy Antwoord 2", QuestionId = -2 },
                    new Answer() { Id = -3, AnswerString = "Dummy Antwoord 3", QuestionId = -3 },
                    new Answer() { Id = -4, AnswerString = "Dummy Antwoord 4", QuestionId = -4 },
                    new Answer() { Id = -5, AnswerString = "Dummy Antwoord 5", QuestionId = -5 },
                    new Answer() { Id = -6, AnswerString = "Dummy Antwoord 6", QuestionId = -6 },
                    new Answer() { Id = -7, AnswerString = "Dummy Antwoord 7", QuestionId = -7 },
                    new Answer() { Id = -8, AnswerString = "Dummy Antwoord 8", QuestionId = -8 },
                    new Answer() { Id = -9, AnswerString = "Dummy Antwoord 9", QuestionId = -9 },
                    new Answer() { Id = -10, AnswerString = "Dummy Antwoord 10", QuestionId = -10 }
                });

            modelBuilder.Entity<Team>().HasData(new Team() { Id = -1, Name = "Dummy Team", Enabled = true, Scores = 0 });

            modelBuilder.Entity<Round>().HasData(new Round() { Id = -1, Name = "Ronde 0" });

            modelBuilder.Entity<Question>().HasData(new List<Question>()
                {
                    new Question() { Id = -1, RoundId = -1, QuestionString = "Vraag 1"  },
                    new Question() { Id = -2, RoundId = -1, QuestionString = "Vraag 2"  },
                    new Question() { Id = -3, RoundId = -1, QuestionString = "Vraag 3"  },
                    new Question() { Id = -4, RoundId = -1, QuestionString = "Vraag 4"  },
                    new Question() { Id = -5, RoundId = -1, QuestionString = "Vraag 5"  },
                    new Question() { Id = -6, RoundId = -1, QuestionString = "Vraag 6"  },
                    new Question() { Id = -7, RoundId = -1, QuestionString = "Vraag 7"  },
                    new Question() { Id = -8, RoundId = -1, QuestionString = "Vraag 8"  },
                    new Question() { Id = -9, RoundId = -1, QuestionString = "Vraag 9"  },
                    new Question() { Id = -10, RoundId = -1, QuestionString = "Vraag 10" },
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
