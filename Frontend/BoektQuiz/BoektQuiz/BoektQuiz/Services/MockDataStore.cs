using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoektQuiz.Models;

namespace BoektQuiz.Services
{
    public class MockDataStore : IDataStore<Question>
    {
        readonly List<Question> questions;
        readonly List<Round> rounds;

        public MockDataStore()
        {
            questions = new List<Question>()
            {
                new Question { Id = 0, Text = "Vraag 1", Answer="" },
                new Question { Id = 1, Text = "Vraag 2", Answer="" },
                new Question { Id = 2, Text = "Vraag 3", Answer="" },
                new Question { Id = 3, Text = "Vraag 4", Answer="" },
                new Question { Id = 4, Text = "Vraag 5", Answer="" },
                new Question { Id = 5, Text = "Vraag 6", Answer="" },
                new Question { Id = 6, Text = "Vraag 7", Answer="" },
                new Question { Id = 7, Text = "Vraag 8", Answer="" },
                new Question { Id = 8, Text = "Vraag 9", Answer="" },
                new Question { Id = 9, Text = "Vraag 10", Answer="" },
            };
            rounds = new List<Round>()
            {
                new Round { Id = 0, Text = "Ronde 1" },
                new Round { Id = 1, Text = "Ronde 2" },
                new Round { Id = 2, Text = "Ronde 3" },
                new Round { Id = 3, Text = "Ronde 4" },
                new Round { Id = 4, Text = "Ronde 5" },
                new Round { Id = 5, Text = "Ronde 6" },
                new Round { Id = 6, Text = "Ronde 7" },
                new Round { Id = 7, Text = "Ronde 8" },
            };
        }

        public async Task<bool> AddQuestionAsync(Question question)
        {
            questions.Add(question);

            return await Task.FromResult(true);
        }
        public async Task<bool> AddRoundAsync(Round round)
        {
            rounds.Add(round);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            var oldQuestion = questions.Where((Question arg) => arg.Id == question.Id).FirstOrDefault();
            questions.Remove(oldQuestion);
            questions.Add(question);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateRoundAsync(Round round)
        {
            var oldRound = rounds.Where((Round arg) => arg.Id == round.Id).FirstOrDefault();
            rounds.Remove(oldRound);
            rounds.Add(round);

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var oldQuestion = questions.Where((Question arg) => arg.Id == id).FirstOrDefault();
            questions.Remove(oldQuestion);

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteRoundAsync(int id)
        {
            var oldRound = rounds.Where((Round arg) => arg.Id == id).FirstOrDefault();
            rounds.Remove(oldRound);

            return await Task.FromResult(true);
        }
        public async Task<Question> GetQuestionAsync(int id)
        {
            return await Task.FromResult(questions.FirstOrDefault(s => s.Id == id));
        }
        public async Task<Round> GetRoundAsync(int id)
        {
            return await Task.FromResult(rounds.FirstOrDefault(r => r.Id == id));
        }
        public async Task<IEnumerable<Question>> GetQuestionsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(questions);
        }
        public async Task<IEnumerable<Round>> GetRoundsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(rounds);
        }
    }
}