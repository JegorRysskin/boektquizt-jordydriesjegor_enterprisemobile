using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
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
            if (Round != null)
            {
                MessagingCenter.Instance.Unsubscribe<QuestionViewModel, Round>(this, "Round");
                Question = Round.Questions[Index];
            } 
            else
            {
                MessagingCenter.Instance.Subscribe<RoundStartViewModel, Round>(this, "Round", (sender, round) => {
                    this.Index = 0;
                    Round = round; 
                    Question = Round.Questions[Index]; 
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
            } 
            else
            {
                await _navigationService.NavigateToAsync(RoutingConstants.RoundEndRoute);
                MessagingCenter.Instance.Send(this, "IsACEnabled", false);
                MessagingCenter.Instance.Send(this, "Round", Round);
            }
        }

        public bool CanSendAnswer()
        {
            if (Question != null)
            {
                return Question.Answer.AnswerString.Length > 0;
            }

            return false;
        }
    }
}
