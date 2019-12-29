using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;

namespace JuryApp.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Teams> GetAllTeams(bool forceRefresh);
        Task<bool> DeleteTeam(int id);
    }
}
