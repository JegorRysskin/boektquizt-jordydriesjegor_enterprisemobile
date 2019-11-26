using System;

using BoektQuiz.Models;
using BoektQuiz.Views;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Vraag Vraag { get; set; }

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
        public ItemDetailViewModel(INavigation navigation, Vraag vraag = null)
        {
            Title = vraag?.Text;
            Vraag = vraag;
            Navigation = navigation;
        }

        private Command _sendAnswerCommand;

        public Command SendAnswerCommand =>
            _sendAnswerCommand ?? (_sendAnswerCommand = new Command(OnSendAnswer, CanSendAnswer));

        private void OnSendAnswer()
        {
            int next = Vraag.Id + 1;
            if (next < 10)
            {
                Navigation.PopAsync();
                Navigation.PushAsync(new ItemDetailPage(next));
            }
        }

        public bool CanSendAnswer()
        {
            return IsEntryFilledIn;
        }
    }
}
