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
        public Quiz SelectedQuiz { get; set; }
        public EditQuizViewModel()
        {
            Messenger.Default.Register<Quiz>(this, (quiz) => { SelectedQuiz = quiz; });
        }
    }
}
