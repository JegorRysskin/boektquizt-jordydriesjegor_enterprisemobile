using BoektQuiz.Context;
using BoektQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BoektQuiz.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly BoektQuizContext _context;

        public TeamRepository(BoektQuizContext context)
        {
            _context = context;
        }

        public async Task UpdateTeamAsync(Team team)
        {
            if (!_context.Teams.Contains(team))
            {
                await _context.Teams.AddAsync(team);
            } 
            else
            {
                await _context.Answers.AddRangeAsync(team.Answers.Except(_context.Answers));
            }

            await _context.SaveChangesAsync();
        }
    }
}
