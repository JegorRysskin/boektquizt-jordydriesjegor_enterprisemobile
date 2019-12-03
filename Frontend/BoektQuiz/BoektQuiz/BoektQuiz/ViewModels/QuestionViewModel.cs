using System;

using BoektQuiz.Models;
using BoektQuiz.Views;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class QuestionViewModel : BaseViewModel
    {
        public Question Question { get; set; }

        private bool _isEntryFilledIn;

        public bool IsEntryFilledIn
        {
            get => _isEntryFilledIn;
            set
            {
                if (_isEntryFilledIn == value) return;
                _isEntryFilledIn = value;
                OnPropertyChanged();
            }
        }

        public INavigation Navigation;
        public QuestionViewModel(INavigation navigation, Question question = null)
        {
            Title = question?.Text;
            Question = question;
            Navigation = navigation;
        }

        private Command _sendAnswerCommand;

        public Command SendAnswerCommand =>
            _sendAnswerCommand ?? (_sendAnswerCommand = new Command(OnSendAnswer, CanSendAnswer));

        private void OnSendAnswer()
        {
            Navigation.PopAsync();
        }

        public bool CanSendAnswer()
        {
            return IsEntryFilledIn;
        }
    }
}
