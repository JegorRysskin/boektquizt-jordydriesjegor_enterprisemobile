using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoektQuiz.Context;
using BoektQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace BoektQuiz.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly BoektQuizContext _context;

        public QuestionRepository(BoektQuizContext context)
        {
            _context = context;
        }

        public async Task<IList<Question>> GetQuestionsFromRound(int roundId)
        {
            var roundList = await _context.Rounds
                                  .Include(r => r.Questions)
                                  .ThenInclude(q => q.Answer)
                                  .ThenInclude(a => a.Team)
                                  .ToListAsync();
            var round = roundList.Find(r => r.Id == roundId);
            
            if (round != null)
            {
                return round.Questions;
            }

            return null;
        }
    }
}
