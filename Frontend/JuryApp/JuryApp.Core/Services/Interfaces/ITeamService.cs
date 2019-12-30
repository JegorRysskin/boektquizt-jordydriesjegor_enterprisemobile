using JuryApp.Core.Models.Collections;
using System.Threading.Tasks;

namespace JuryApp.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Teams> GetAllTeams(bool forceRefresh);
        Task<bool> DeleteTeam(int id);
    }
}
