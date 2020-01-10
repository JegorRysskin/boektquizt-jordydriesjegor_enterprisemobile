using System.Collections.Generic;
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
                        .ToListAsync();
        }

        public async Task UpdateRoundAsync(Round round)
        {
            if (!_context.Rounds.Contains(round))
            {
                await _context.Rounds.AddAsync(round);

                await _context.SaveChangesAsync();
            } 
        }
    }
}
