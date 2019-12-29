using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class TeamsViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private readonly TeamService _teamService;

        public Teams Teams { get; set; } = new Teams();

        public RelayCommand<int> EditTeamCommand => new RelayCommand<int>(NavigateToEditTeamPage);

        public TeamsViewModel()
        {
            _teamService = new TeamService();
            FetchListOfTeams(false);

            NavigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfTeams(true);
        }

        private void NavigateToEditTeamPage(int selectedIndex)
        {
            if (selectedIndex != -1)
            {
                Messenger.Default.Send(Teams[selectedIndex]);
                NavigationService.Navigate(typeof(EditTeamViewModel).FullName);
            }

        }

        private async void FetchListOfTeams(bool forceRefresh)
        {
            var teams = await _teamService.GetAllTeams(forceRefresh);

            Teams.Clear();
            foreach (var team in teams)
            {
                Teams.Add(team);
            }
        }
    }
}
