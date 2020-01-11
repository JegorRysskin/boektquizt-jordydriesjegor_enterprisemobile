using BoektQuiz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoektQuiz.Repositories
{
    public interface IRoundRepository
    {
        Task<IList<Round>> GetAllRoundsAsync();
        Task UpdateRoundAsync(Round round);
    }
}
