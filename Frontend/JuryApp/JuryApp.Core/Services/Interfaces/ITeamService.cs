using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JuryApp.Core.Models;

namespace JuryApp.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Task<ObservableCollection<Team>> GetAllTeams(bool forceRefresh);
    }
}
