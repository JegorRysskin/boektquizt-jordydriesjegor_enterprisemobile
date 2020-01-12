using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;
using System.Collections.Generic;
using System.Linq;

namespace JuryApp.ViewModels
{
    public class CorrectViewModel : ViewModelBase
    {
        private readonly INavigationServiceEx _navigationService;
        private readonly ITeamService _teamService;
        private readonly IRoundService _roundService;

        public Teams EnabledTeams { get; set; } = new Teams();
        public Team SelectedTeam { get; set; } = new Team();
        public Answers TeamAnswers { get; set; } = new Answers();
        public Rounds QuizRounds { get; set; } = new Rounds();

        public Answers TeamAnswersPerRound { get; set; } = new Answers();
        public string RoundsSelectionMode { get; set; } = "Single";

        public CorrectViewModel(ITeamService teamService, IRoundService roundService, INavigationServiceEx navigationService)
        {
            _teamService = teamService;
            _roundService = roundService;
            _navigationService = navigationService;

            FetchListOfEnabledTeams(false);
            _navigationService.Navigated += NavigationService_Navigated;

        }

        public RelayCommand<Team> GetAnswersSelectedTeamCommand => new RelayCommand<Team>(GetAnswersSelectedTeam);
        public RelayCommand<Round> GetSelectedRoundCommand => new RelayCommand<Round>(GetSelectedRound);
        public RelayCommand<IList<object>> SendScoreCommand => new RelayCommand<IList<object>>(SendScore);

        private void SendScore(IList<object> listItems)
        {
            PatchScoreToSelectedTeam(listItems.Count);
        }

        private async void PatchScoreToSelectedTeam(int score)
        {
            var result = await _teamService.PatchTeamScore(SelectedTeam.TeamId, score);
        }

        private void GetAnswersSelectedTeam(Team selectedTeam)
        {
            if (selectedTeam == null) return;
            SelectedTeam = selectedTeam;

            RoundsSelectionMode = "None";
            RaisePropertyChanged(() => RoundsSelectionMode);
            TeamAnswersPerRound.Clear();

            TeamAnswers.Clear();
            selectedTeam.TeamAnswers.ToList().ForEach(a =>
            {
                TeamAnswers.Add(a);
            });

            RoundsSelectionMode = "Single";
            RaisePropertyChanged(() => RoundsSelectionMode);

        }

        private void GetSelectedRound(Round selectedRound)
        {
            if (selectedRound == null) return;

            var questions = QuizRounds.First(r => r == selectedRound).RoundQuestions;

            TeamAnswersPerRound.Clear();
            foreach (var answer in TeamAnswers)
            {
                foreach (var question in questions)
                {
                    if (answer.AnswerQuestion.QuestionId == question.QuestionId)
                    {
                        TeamAnswersPerRound.Add(answer);
                    }
                }
            }
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfEnabledTeams(true);
            GetRoundsFromEnabledQuiz(true);
            TeamAnswersPerRound.Clear();
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
            GetRoundsFromEnabledQuiz(true);
        }

        private async void GetRoundsFromEnabledQuiz(bool forceRefresh)
        {
            var rounds = await _roundService.GetAllRoundsByEnabledQuiz(forceRefresh);

            QuizRounds.Clear();
            foreach (var round in rounds)
            {
                QuizRounds.Add(round);
            }

        }
    }
}
