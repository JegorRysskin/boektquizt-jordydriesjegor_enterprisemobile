using System;
using System.Collections.ObjectModel;
using JuryApp.ViewModels;

using Windows.UI.Xaml.Controls;
using JuryApp.Core.Models;
using JuryApp.Core.Services;

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
