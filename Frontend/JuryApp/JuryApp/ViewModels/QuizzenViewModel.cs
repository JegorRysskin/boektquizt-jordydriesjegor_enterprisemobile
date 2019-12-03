using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Views;

namespace JuryApp.ViewModels
{
    public class QuizzenViewModel : ViewModelBase, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private readonly QuizService _quizRepository;
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

        public QuizzenViewModel()
        {
            _quizRepository = new QuizService();
            WaitForList();
        }

        private async void WaitForList()
        {
            Quizzes = await _quizRepository.GetAllQuizzes();
        }

    }
}
