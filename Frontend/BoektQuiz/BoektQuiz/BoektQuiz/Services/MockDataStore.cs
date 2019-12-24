using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoektQuiz.Models;

namespace BoektQuiz.Services
{
    public class MockDataStore : IDataStore<Round>
    {
        private readonly List<Question> questions1;
        private readonly List<Question> questions2;
        private readonly List<Question> questions3;
        private readonly List<Question> questions4;
        private readonly List<Question> questions5;
        private readonly List<Question> questions6;
        private readonly List<Question> questions7;
        private readonly List<Question> questions8;
        private readonly List<Round> rounds;

        public MockDataStore()
        {
            questions1 = GenerateQuestionsList(0);

            questions2 = GenerateQuestionsList(1);

            questions3 = GenerateQuestionsList(2);

            questions4 = GenerateQuestionsList(3);

            questions5 = GenerateQuestionsList(4);

            questions6 = GenerateQuestionsList(5);

            questions7 = GenerateQuestionsList(6);

            questions8 = GenerateQuestionsList(7);

            rounds = new List<Round>()
            {
                new Round { Id = 1, Text = "Ronde 1", Questions = questions1 },
                new Round { Id = 2, Text = "Ronde 2", Questions = questions2 },
                new Round { Id = 3, Text = "Ronde 3", Questions = questions3 },
                new Round { Id = 4, Text = "Ronde 4", Questions = questions4 },
                new Round { Id = 5, Text = "Ronde 5", Questions = questions5 },
                new Round { Id = 6, Text = "Ronde 6", Questions = questions6 },
                new Round { Id = 7, Text = "Ronde 7", Questions = questions7 },
                new Round { Id = 8, Text = "Ronde 8", Questions = questions8 },
            };
        }

        public async Task<bool> AddItemAsync(Round round)
        {
            rounds.Add(round);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(Round round)
        {
            var oldRound = rounds.Where((Round arg) => arg.Id == round.Id).FirstOrDefault();
            rounds.Remove(oldRound);
            rounds.Add(round);

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldRound = rounds.Where((Round arg) => arg.Id == id).FirstOrDefault();
            rounds.Remove(oldRound);

            return await Task.FromResult(true);
        }
        public async Task<Round> GetItemAsync(int id)
        {
            return await Task.FromResult(rounds.FirstOrDefault(r => r.Id == id));
        }
        public async Task<IEnumerable<Round>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(rounds);
        }

        private List<Question> GenerateQuestionsList(int index)
        {
            return new List<Question>()
            {
                new Question { Id = 1 + (index * 10), Text = "Vraag 1", Answer = new Answer() { Id = 1 + (index * 10), AnswerString = "", QuestionId = 1 + (index * 10), TeamId = 1 }  },
                new Question { Id = 2 + (index * 10), Text = "Vraag 2", Answer = new Answer() { Id = 2 + (index * 10), AnswerString = "", QuestionId = 2 + (index * 10), TeamId = 1 }  },
                new Question { Id = 3 + (index * 10), Text = "Vraag 3", Answer = new Answer() { Id = 3 + (index * 10), AnswerString = "", QuestionId = 3 + (index * 10), TeamId = 1 }  },
                new Question { Id = 4 + (index * 10), Text = "Vraag 4", Answer = new Answer() { Id = 4 + (index * 10), AnswerString = "", QuestionId = 4 + (index * 10), TeamId = 1 }  },
                new Question { Id = 5 + (index * 10), Text = "Vraag 5", Answer = new Answer() { Id = 5 + (index * 10), AnswerString = "", QuestionId = 5 + (index * 10), TeamId = 1 }  },
                new Question { Id = 6 + (index * 10), Text = "Vraag 6", Answer = new Answer() { Id = 6 + (index * 10), AnswerString = "", QuestionId = 6 + (index * 10), TeamId = 1 }  },
                new Question { Id = 7 + (index * 10), Text = "Vraag 7", Answer = new Answer() { Id = 7 + (index * 10), AnswerString = "", QuestionId = 7 + (index * 10), TeamId = 1 }  },
                new Question { Id = 8 + (index * 10), Text = "Vraag 8", Answer = new Answer() { Id = 8 + (index * 10), AnswerString = "", QuestionId = 8 + (index * 10), TeamId = 1 }  },
                new Question { Id = 9 + (index * 10), Text = "Vraag 9", Answer = new Answer() { Id = 9 + (index * 10), AnswerString = "", QuestionId = 9 + (index * 10), TeamId = 1 } },
                new Question { Id = 10 + (index * 10), Text = "Vraag 10", Answer = new Answer() { Id = 10 + (index * 10), AnswerString = "", QuestionId = 10 + (index * 10), TeamId = 1 } },
            };
        }
    }
}