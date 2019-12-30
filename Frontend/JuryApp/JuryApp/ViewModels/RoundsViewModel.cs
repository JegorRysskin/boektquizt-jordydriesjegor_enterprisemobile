using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;

namespace JuryApp.ViewModels
{
    public class RoundsViewModel
    {
        public Quiz SelectedQuiz { get; set; }
        public RoundsViewModel()
        {
            Messenger.Default.Register<Quiz>(this, (quiz) => { SelectedQuiz = quiz; });
        }
    }
}
