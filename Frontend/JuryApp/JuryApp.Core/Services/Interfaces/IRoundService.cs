using System.Threading.Tasks;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;

namespace JuryApp.Core.Services.Interfaces
{
    public interface IRoundService
    {
        Task<Rounds> GetAllRoundsByEnabledQuiz();
        Task<bool> EditRound(int id, Round editedRound);
    }
}