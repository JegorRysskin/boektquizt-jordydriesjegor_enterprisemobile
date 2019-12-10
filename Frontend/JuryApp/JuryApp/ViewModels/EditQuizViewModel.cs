using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models;

namespace JuryApp.ViewModels
{
    public class EditQuizViewModel : ViewModelBase
    {
        public Quiz SelectedQuiz { get; set; }
        public EditQuizViewModel()
        {
            
        }
    }
}
