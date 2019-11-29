using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models;
using JuryApp.Core.Services;

namespace JuryApp.ViewModels
{
    public class QuizzenViewModel : ViewModelBase
    {
        private QuizRepository _quizRepository;
        public ObservableCollection<Quiz> Quizzes { get; set; }

        public QuizzenViewModel()
        {
            _quizRepository = new QuizRepository();
            WaitForList();
        }

        private async void WaitForList()
        {
            Quizzes = await _quizRepository.GetAllQuizzes();
        }
    }
}
