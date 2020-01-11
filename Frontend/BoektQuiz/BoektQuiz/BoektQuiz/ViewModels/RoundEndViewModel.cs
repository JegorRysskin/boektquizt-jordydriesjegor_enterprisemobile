using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.Util;
using Plugin.Connectivity.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class RoundEndViewModel : BaseViewModel
    {
        public Round _round;
        public Round Round {
            get => _round; 
            set
            {
                if (_round == value) return;
                _round = value;
                OnPropertyChanged();
            } 
        }

        private Team _team;
        public Team Team
        {
            get => _team;
            set { 
                if (_team == value) return; 
                _team = value; 
                OnPropertyChanged(); 
            }
        }


        private INavigationService _navigationService;

        private IRoundRepository _roundRepository;

        private ITeamRepository _teamRepository;

        private IBackendService _backendService;

        private Command _endRoundCommand;
        private string _token;

        public Command EndRoundCommand =>
            _endRoundCommand ?? (_endRoundCommand = new Command(OnEndRound, CanEndRound));

        public Command ReloadRoundCommand { get; set; }

        private String _status;
        public String Status { get => _status; set { if (_status == value) return; _status = value; OnPropertyChanged(); } }

        private Color _statusColor;
        public Color StatusColor { get => _statusColor; set { if (_statusColor == value) return; _statusColor = value; OnPropertyChanged(); } }

        public RoundEndViewModel(INavigationService navigationService, IRoundRepository roundRepository, ITeamRepository teamRepository, IBackendService backendService)
        {
            MessagingCenter.Instance.Subscribe<QuestionViewModel, Round>(this, "Round", (sender, round) => { Round = round; UpdateStatus(); });
            MessagingCenter.Instance.Subscribe<QuestionViewModel, Team>(this, "Team", (sender, team) => { Team = team; });
            if (Application.Current != null)
            {
                if (Application.Current.Properties.ContainsKey("token"))
                {
                    _token = Application.Current.Properties["token"].ToString();
                }
            }
            _navigationService = navigationService;
            _roundRepository = roundRepository;
            _teamRepository = teamRepository;
            _backendService = backendService;
            Connectivity.Instance.ConnectivityChanged += HandleConnectivityChanged;
            ReloadRoundCommand = new Command(async () => await ExecuteReloadRoundCommand());
        }

        private async void OnEndRound()
        {
            if (Round != null)
            {
                MessagingCenter.Instance.Unsubscribe<QuestionViewModel, Round>(this, "Round");
                MessagingCenter.Instance.Unsubscribe<QuestionViewModel, Team>(this, "Team");
                await _navigationService.ReturnToRoot();
                await _roundRepository.UpdateRoundAsync(Round);
                await _teamRepository.UpdateTeamAsync(Team);

                foreach (Answer answer in Team.Answers)
                {
                    await _backendService.PatchTeamAnswer(answer, Team, _token);
                }
            }
        }

        public bool CanEndRound()
        {
            return Connectivity.Instance.IsConnected && !Round.Enabled;
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (Connectivity.Instance.IsConnected)
            {
                Round = _backendService.GetRoundById(Round.Id, _token).Result;
            }

            UpdateStatus();
            EndRoundCommand.ChangeCanExecute();
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
                    EndRoundCommand.ChangeCanExecute();
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

            if (Connectivity.Instance.IsConnected)
            {
                if (!Round.Enabled)
                {
                    Status = "Verbinding gemaakt. U kan nu de antwoorden versturen.";
                    StatusColor = Color.Accent;
                }
                else
                {
                    Status = "Deze ronde is nog niet afgesloten door de jury. Swipe naar beneden om de status van de ronde opnieuw op te halen.";
                }
            } 
            else
            {
                Status = "Nu mag u de WiFi terug aanzetten om de antwoorden te versturen.";
            }
        }
    }
}
