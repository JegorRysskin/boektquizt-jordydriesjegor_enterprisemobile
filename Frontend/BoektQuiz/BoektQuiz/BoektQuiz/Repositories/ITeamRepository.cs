using BoektQuiz.Models;
using System.Threading.Tasks;

namespace BoektQuiz.Repositories
{
    public interface ITeamRepository
    {
        Task UpdateTeamAsync(Team team);
    }
}
