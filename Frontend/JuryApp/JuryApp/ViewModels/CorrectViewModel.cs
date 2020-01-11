using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class CorrectViewModel
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;
        private readonly TeamService _teamService;
        public Teams EnabledTeams { get; set; } = new Teams();

        public CorrectViewModel()
        {
            _teamService = new TeamService();
            FetchListOfEnabledTeams(false);

            NavigationService.Navigated += NavigationService_Navigated;

        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfEnabledTeams(true);
        }

        private async void FetchListOfEnabledTeams(bool forceRefresh)
        {
            var teams = await _teamService.GetAllTeams(forceRefresh);

            EnabledTeams.Clear();
            foreach (var team in teams)
            {
                if (team.TeamEnabled)
                    EnabledTeams.Add(team);
            }
        }
    }
}
