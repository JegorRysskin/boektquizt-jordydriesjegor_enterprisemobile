using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using BoektQuiz.Context;
using BoektQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace BoektQuiz.Repositories
{
    public class RoundRepository : IRoundRepository
    {
        private readonly BoektQuizContext _context;

        public RoundRepository(BoektQuizContext context)
        {
            _context = context;
        }

        public async Task<IList<Round>> GetAllRoundsAsync()
        {
            return await _context.Rounds
                        .Include(r => r.Questions)
                        .ThenInclude(q => q.Answer)
                        .ToListAsync();
        }

        public async Task UpdateRoundAsync(Round round)
        {
            Round existingRound = GetAllRoundsAsync().Result.FirstOrDefault(r => r.Id == round.Id);
            if (existingRound != null)
            {
                List<Answer> existingAnswers = existingRound.Questions.ConvertAll<Answer>(q => q.Answer);
                List<Answer> answers = round.Questions.ConvertAll<Answer>(q => q.Answer);

                existingAnswers.ForEach(a1 => a1.AnswerString = answers.Find(a2 => a1.Id == a2.Id).AnswerString);

                _context.Answers.UpdateRange(existingAnswers);
            } 
            else
            {
                await _context.Rounds.AddAsync(round);
            }

            await _context.SaveChangesAsync();
        }
    }
}
