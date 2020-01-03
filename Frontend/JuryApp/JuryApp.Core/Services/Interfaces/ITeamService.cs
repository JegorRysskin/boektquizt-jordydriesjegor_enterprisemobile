using JuryApp.Core.Models.Collections;
using System.Threading.Tasks;
using JuryApp.Core.Models;

namespace JuryApp.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Teams> GetAllTeams(bool forceRefresh);
        Task<bool> DeleteTeam(int id);
        Task<bool> EditTeam(int id, Team editedTeam);
    }
}
