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
    public class CreateQuizViewModel : ViewModelBase
    {
        private INavigationServiceEx _navigationService;
        public readonly List<int> ListOfTenForComboBox = Enumerable.Range(1, 10).ToList();

        public Quiz NewQuiz { get; set; } = new Quiz();

        private readonly IQuizService _quizService;

        public RelayCommand CreateNewQuizCommand => new RelayCommand(CreateNewQuiz);
        public RelayCommand<int> CreateRoundsCommand => new RelayCommand<int>(CreateRounds);

        public CreateQuizViewModel(IQuizService quizService, INavigationServiceEx navigationServiceEx)
        {
            _quizService = quizService;
            _navigationService = navigationServiceEx;
        }

        private async void CreateNewQuiz()
        {
            var result = await _quizService.AddQuiz(NewQuiz);

            if (result)
            {
                _navigationService.Navigate(typeof(QuizzenViewModel).FullName);
            }
        }

        private void CreateRounds(int amountOfRoundsToCreate)
        {
            var tenEmptyQuestions = new Questions();
            for (var i = 1; i <= 10; i++)
            {
                tenEmptyQuestions.Add(new Question { QuestionText = $"Vraag {i}", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer { CorrectAnswerText = "" } } });
            }

            NewQuiz.QuizRounds = new Rounds();
            for (var i = 1; i <= amountOfRoundsToCreate; i++)
            {
                NewQuiz.QuizRounds.Add(new Round { RoundEnabled = false, RoundName = $"Ronde {i}", RoundQuestions = tenEmptyQuestions });
            }
        }
    }
}
