using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;

namespace JuryApp.ViewModels
{
    public class EditQuizViewModel : ViewModelBase
    {
        private Quiz _selectedQuiz;

        public Quiz SelectedQuiz
        {
            get => _selectedQuiz;
            set
            {
                if (_selectedQuiz == value) return;
                _selectedQuiz = value;
                RaisePropertyChanged("SelectedQuiz");
            }
        }

        public EditQuizViewModel()
        {
            Messenger.Default.Register<Quiz>(this, (quiz) => { _selectedQuiz = quiz; });
        }
    }
}
