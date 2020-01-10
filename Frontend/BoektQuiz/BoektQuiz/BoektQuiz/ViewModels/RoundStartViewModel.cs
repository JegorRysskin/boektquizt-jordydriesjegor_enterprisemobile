using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
using Plugin.Connectivity.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class RoundStartViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        public Round Round { get; set; }

        public Team Team { get; set; }

        private string _token;

        private IBackendService _backendService;

        private Command _startRoundCommand;

        private String _status;
        public String Status { get => _status; set { if (_status == value) return; _status = value; OnPropertyChanged(); } }

        private Color _statusColor;
        public Color StatusColor { get => _statusColor; set { if (_statusColor == value) return; _statusColor = value; OnPropertyChanged(); } }

        public Command StartRoundCommand =>
            _startRoundCommand ?? (_startRoundCommand = new Command(OnStartRound, CanStartRound));

        public Command ReloadRoundCommand { get; set; }

        public RoundStartViewModel(INavigationService navigationService, IBackendService backendService, int id)
        {
            _backendService = backendService;

            if (Application.Current != null)
            {
                if (Application.Current.Properties.ContainsKey("token"))
                {
                    _token = Application.Current.Properties["token"].ToString();
                } 
            }

            if (_token != String.Empty)
            {
                Team = _backendService.GetTeamByToken(_token).Result;
                Round = _backendService.GetRoundById(id, _token).Result;
            }
            _navigationService = navigationService;
            Connectivity.Instance.ConnectivityChanged += HandleConnectivityChanged;
            ReloadRoundCommand = new Command(async () => await ExecuteReloadRoundCommand());
            UpdateStatus();
        }

        private async void OnStartRound()
        {
            //Start Round
            await _navigationService.NavigateToAsync(RoutingConstants.QuestionRoute);
            MessagingCenter.Instance.Send(this, "Round", Round);
            MessagingCenter.Instance.Send(this, "Team", Team);
        }

        public bool CanStartRound()
        {
            return !Connectivity.Instance.IsConnected && Round.Enabled;
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (Connectivity.Instance.IsConnected)
            {
                Round = _backendService.GetRoundById(Round.Id, _token).Result;
            }

            UpdateStatus();
            StartRoundCommand.ChangeCanExecute();
        }

        async Task ExecuteReloadRoundCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (Connectivity.Instance.IsConnected)
                {
                    Round = _backendService.GetRoundById(Round.Id, _token).Result;
                    UpdateStatus();
                    StartRoundCommand.ChangeCanExecute();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void UpdateStatus()
        {
            StatusColor = Color.FromHex("ED028C");

            if (Round.Enabled)
            {
                Status = "Om de ronde te starten, dient u eerst de WiFi uit te zetten.";
                
                if (!Connectivity.Instance.IsConnected)
                {
                    Status = "U kan de ronde starten.";
                    StatusColor = Color.Accent;
                }
            } 
            else
            {
                Status = "Deze ronde is nog niet opengezet door de jury.";
            }
        }
    }
}
