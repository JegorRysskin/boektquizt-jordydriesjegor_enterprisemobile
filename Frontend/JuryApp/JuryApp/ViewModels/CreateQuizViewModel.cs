using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JuryApp.Core.Models;
using JuryApp.Core.Services;

namespace JuryApp.ViewModels
{
    public class CreateQuizViewModel : ViewModelBase
    {
        public Quiz NewQuiz { get; set; }
        private QuizService _quizService;

        public RelayCommand CreateNewQuizCommand => new RelayCommand(CreateNewQuiz);

        public CreateQuizViewModel()
        {
            _quizService = new QuizService();
        }

        private async void CreateNewQuiz()
        {
            //TODO: do something with returned boolean, succes/fail
            //TODO: NewQuiz not mapping to page
            await _quizService.AddQuiz(new Quiz{ QuizEnabled = true, QuizName = "testQuiz"});
        }
    }
}
