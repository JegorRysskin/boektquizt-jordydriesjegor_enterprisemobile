using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public String Username { get; set; }
        public String Password { get; set; }
        private String _status;
        public String Status { get => _status; set { if (_status == value) return; _status = value; OnPropertyChanged(); } }
        private Color _statusColor;
        public Color StatusColor { get => _statusColor; set { if (_statusColor == value) return; _statusColor = value; OnPropertyChanged(); } }

        private Command _registerTeamCommand;

        public Command RegisterTeamCommand => _registerTeamCommand ?? (_registerTeamCommand = new Command(OnRegisterTeam, CanRegisterTeam));

        private IBackendService _backendService;

        public RegisterViewModel(IBackendService backendService)
        {
            _backendService = backendService;
            Connectivity.Instance.ConnectivityChanged += Instance_ConnectivityChanged;

            if (!Connectivity.Instance.IsConnected)
            {
                Status = "U moet verbonden zijn met het internet om te kunnen registreren.";
                StatusColor = Color.FromHex("ED028C");
            }
        }

        private void Instance_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (!Connectivity.Instance.IsConnected)
            {
                Status = "U moet verbonden zijn met het internet om te kunnen registreren.";
                StatusColor = Color.FromHex("ED028C");
            }
            else
            {
                Status = String.Empty;
                StatusColor = Color.Transparent;
            }

            RegisterTeamCommand.ChangeCanExecute();
        }

        private async void OnRegisterTeam()
        {
            var status = await _backendService.Register(Username, Password);

            if (status.Equals(HttpStatusCode.Created) || status.Equals(HttpStatusCode.OK))
            {
                Status = "Team registreren gelukt";
                StatusColor = Color.Accent;
            }
            else
            {
                Status = "Er is iets misgelopen bij het registreren";
                StatusColor = Color.FromHex("ED028C");
            }
        }

        private bool CanRegisterTeam()
        {
            if (Connectivity.Instance.IsConnected)
            {
                if (Username != null && Password != null)
                {
                    return (Username.Length > 3 && Password.Length > 6);
                }
            }

            return false;
        }
    }
}
