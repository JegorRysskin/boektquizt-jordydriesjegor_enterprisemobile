using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class EditTeamViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public Team SelectedTeam { get; set; }
        private readonly TeamService _teamService;

        public EditTeamViewModel()
        {
            _teamService = new TeamService();
            NavigationService.Navigated += NavigationService_Navigated;

        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            Messenger.Default.Register<Team>(this, (team) => { SelectedTeam = team; });
        }

        public RelayCommand DeleteTeamCommand => new RelayCommand(DeleteTeam);

        private async void DeleteTeam()
        {
            var result = await _teamService.DeleteTeam(SelectedTeam.TeamId);

            if (result)
            {
                NavigationService.Navigate(typeof(TeamsViewModel).FullName);
            }
        }
    }
}
