using System;
using System.Collections.ObjectModel;
using JuryApp.ViewModels;

using Windows.UI.Xaml.Controls;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Helpers;
using JuryApp.Services;
using Microsoft.UI.Xaml.Controls;
using NavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;

namespace JuryApp.Views
{
    public sealed partial class QuizzenPage : Page
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;
        private QuizzenViewModel ViewModel => ViewModelLocator.Current.QuizzenViewModel;
        public Quiz SelectedQuiz { get; set; }

        public QuizzenPage()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(EditQuizViewModel).FullName);
        }

        private void Quizzes_OnItemClick(object sender, ItemClickEventArgs e)
        {
            NavigationService.Navigate(typeof(EditQuizViewModel).FullName);
        }
    }
}
