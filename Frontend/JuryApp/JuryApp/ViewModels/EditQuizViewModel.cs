using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class EditQuizViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public Quiz SelectedQuiz { get; set; }
        private readonly QuizService _quizService;

        public EditQuizViewModel()
        {
            _quizService = new QuizService();

            NavigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            Messenger.Default.Register<Quiz>(this, (quiz) => { SelectedQuiz = quiz; });

        }

        public RelayCommand DeleteQuizCommand => new RelayCommand(DeleteQuiz);

        private async void DeleteQuiz()
        {
            var result = await _quizService.DeleteQuiz(SelectedQuiz.QuizId);

            if (result)
            {
                NavigationService.Navigate(typeof(QuizzenViewModel).FullName);
            }
        }
    }
}
