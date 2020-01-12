using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Services;
using System;
using JuryApp.Helpers;
using JuryApp.Core.Services.Interfaces;

namespace JuryApp.ViewModels
{
    public class EditTeamViewModel : ViewModelBase
    {
        private readonly INavigationServiceEx _navigationService;
        private readonly IMessengerCache _messengerCache;
        private readonly ITeamService _teamService;

        public Team SelectedTeam { get; set; }

        public EditTeamViewModel(ITeamService teamService, INavigationServiceEx navigationService, IMessengerCache messengerCache)
        {
            _teamService = teamService;
            _navigationService = navigationService;
            _messengerCache = messengerCache;

            SelectedTeam = _messengerCache.CachedSelectedTeam;
            Messenger.Default.Register<Team>(this, (team) => { SelectedTeam = team; });
        }

        public RelayCommand DeleteTeamCommand => new RelayCommand(DeleteTeam);
        public RelayCommand EditTeamCommand => new RelayCommand(EditTeam);

        private async void EditTeam()
        {
            var result = await _teamService.EditTeam(SelectedTeam.TeamId, SelectedTeam);

            if (result)
            {
                _navigationService.GoBack();
            }
        }

        private async void DeleteTeam()
        {
            var result = await _teamService.DeleteTeam(SelectedTeam.TeamId);

            if (result)
            {
                _navigationService.GoBack();
            }
        }
    }
}
