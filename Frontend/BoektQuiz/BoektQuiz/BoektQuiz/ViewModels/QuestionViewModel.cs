using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
using System;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class QuestionViewModel : BaseViewModel
    {
        private Question _question;
        public Question Question { 
            get => _question; 
            set {
                if (_question == value) return;
                _question = value;
                OnPropertyChanged();
            }
        }

        private Answer _answer;

        public Answer Answer { 
            get => _answer;
            set {
                if (_answer == value) return;
                _answer = value;
                OnPropertyChanged();
            }
        }

        private Team _team;

        public Team Team { 
            get => _team; 
            set
            {
                if (_team == value) return;
                _team = value;
                OnPropertyChanged();
            } 
        }

        public int Index { get; set; }

        public Round _round;
        public Round Round
        {
            get => _round;
            set
            {
                if (_round == value) return;
                _round = value;
                OnPropertyChanged();
            }
        }

        private INavigationService _navigationService;

        public QuestionViewModel()
        {
        }

        public QuestionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            if (Round != null && Team != null)
            {
                MessagingCenter.Instance.Unsubscribe<QuestionViewModel, Round>(this, "Round");
                MessagingCenter.Instance.Unsubscribe<QuestionViewModel, Team>(this, "Team");
                Question = Round.Questions[Index];
                Answer = Team.Answers[Index];
            }
            else
            {
                MessagingCenter.Instance.Subscribe<RoundStartViewModel, Round>(this, "Round", (sender, round) =>
                {
                    this.Index = 0;
                    Round = round;
                    Question = Round.Questions[Index];
                });
                MessagingCenter.Instance.Subscribe<RoundStartViewModel, Team>(this, "Team", (sender, team) =>
                {
                    Team = team;
                    Answer = Team.Answers[Index];
                });
            }
        }
        

        private Command _sendAnswerCommand;

        public Command SendAnswerCommand =>
            _sendAnswerCommand ?? (_sendAnswerCommand = new Command(OnSendAnswer, CanSendAnswer));

        private async void OnSendAnswer()
        {
            Index++;
            if (Index < Round.Questions.Count)
            {
                Question = Round.Questions[Index];
                Answer = Team.Answers[Index];
            } 
            else
            {
                await _navigationService.NavigateToAsync(RoutingConstants.RoundEndRoute);
                MessagingCenter.Instance.Send(this, "IsACEnabled", false);
                MessagingCenter.Instance.Send(this, "Round", Round);
                MessagingCenter.Instance.Send(this, "Team", Team);
            }
        }

        public bool CanSendAnswer()
        {
            if (Question != null)
            {
                if (Answer != null)
                {
                    if (Answer.AnswerString != null)
                    {
                        return Answer.AnswerString.Length > 0;
                    }
                }
            }

            return false;
        }
    }
}
