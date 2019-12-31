using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Helpers;

namespace JuryApp.ViewModels
{
    public class RoundsViewModel
    {
        private MessengerCache MessengerCache => ViewModelLocator.Current.MessengerCache;

        public Quiz SelectedQuiz { get; set; }
        public RoundsViewModel()
        {
            SelectedQuiz = MessengerCache.CachedSelectedQuiz;
            Messenger.Default.Register<Quiz>(this, (quiz) => { SelectedQuiz = quiz; });
        }
    }
}
