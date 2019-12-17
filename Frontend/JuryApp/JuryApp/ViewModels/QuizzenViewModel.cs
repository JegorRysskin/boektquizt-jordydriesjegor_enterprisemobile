using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Services;
using JuryApp.Views;

namespace JuryApp.ViewModels
{
    public class QuizzenViewModel : ViewModelBase, INotifyCollectionChanged
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private readonly QuizService _quizService;

        private ObservableCollection<Quiz> _quizzes;
        public ObservableCollection<Quiz> Quizzes
        {
            get => _quizzes;
            set
            {
                _quizzes = value;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public RelayCommand CreateQuizCommand => new RelayCommand(NavigateToCreateQuizPage);
        public RelayCommand<int> EditQuizCommand => new RelayCommand<int>(NavigateToEditQuizPage);


        public QuizzenViewModel()
        {
            _quizService = new QuizService();
            FetchListOfQuizzes(false);
            //TODO: Navigationservice.Navigated not ideal as it invokes too often, but does the trick
            NavigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfQuizzes(true);
        }

        private void NavigateToCreateQuizPage()
        {
            NavigationService.Navigate(typeof(CreateQuizViewModel).FullName);
        }

        private void NavigateToEditQuizPage(int selectedIndex)
        {
            if (selectedIndex != -1)
            {
                Messenger.Default.Send(_quizzes[selectedIndex]);
                NavigationService.Navigate(typeof(EditQuizViewModel).FullName);
            }

        }

        private async void FetchListOfQuizzes(bool forceRefresh)
        {
            Quizzes = await _quizService.GetAllQuizzes(forceRefresh);
        }


    }
}
