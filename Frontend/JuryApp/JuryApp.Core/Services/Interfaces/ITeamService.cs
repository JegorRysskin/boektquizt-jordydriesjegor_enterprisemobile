using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using System.Threading.Tasks;

namespace JuryApp.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Teams> GetAllTeams(bool forceRefresh);
        Task<bool> DeleteTeam(int id);
        Task<bool> PatchTeamScore(int id, int score);
        Task<bool> EditTeam(int id, Team editedTeam);
    }
}
