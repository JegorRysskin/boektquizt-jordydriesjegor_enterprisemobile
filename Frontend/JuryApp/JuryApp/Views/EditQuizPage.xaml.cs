using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.ViewModels;

namespace JuryApp.Views
{
    public sealed partial class EditQuizPage : Page
    {
        public Quiz SelectedQuiz { get; set; }
        private readonly QuizService _quizService;

        private EditQuizViewModel ViewModel
        {
            get { return ViewModelLocator.Current.EditQuizViewModel; }

        }
        public EditQuizPage()
        {
            InitializeComponent();
            //TODO: Get selectedindex from quizzenpage and request quiz by id and fill it into editquizpage
            //SelectedQuiz = _quizService.GetQuizById(id);
        }
    }
}
