using System;

using JuryApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace JuryApp.Views
{
    public sealed partial class QuizzenPage : Page
    {
        private QuizzenViewModel ViewModel
        {
            get { return ViewModelLocator.Current.QuizzenViewModel; }
        }

        public QuizzenPage()
        {
            InitializeComponent();
        }

    }
}
