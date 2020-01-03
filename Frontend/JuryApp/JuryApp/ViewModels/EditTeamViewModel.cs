using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Services;
using System;
using JuryApp.Helpers;

namespace JuryApp.ViewModels
{
    public class EditTeamViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;
        private MessengerCache MessengerCache => ViewModelLocator.Current.MessengerCache;

        public Team SelectedTeam { get; set; }
        private readonly TeamService _teamService;

        public EditTeamViewModel()
        {
            _teamService = new TeamService();

            SelectedTeam = MessengerCache.CachedSelectedTeam;
            Messenger.Default.Register<Team>(this, (team) => { SelectedTeam = team; });
        }

        public RelayCommand DeleteTeamCommand => new RelayCommand(DeleteTeam);
        public RelayCommand EditTeamCommand => new RelayCommand(EditTeam);

        private async void EditTeam()
        {
            var result = await _teamService.EditTeam(SelectedTeam.TeamId, SelectedTeam);

            if (result)
            {
                NavigationService.GoBack();
            }
        }

        private async void DeleteTeam()
        {
            var result = await _teamService.DeleteTeam(SelectedTeam.TeamId);

            if (result)
            {
                NavigationService.GoBack();
            }
        }
    }
}
