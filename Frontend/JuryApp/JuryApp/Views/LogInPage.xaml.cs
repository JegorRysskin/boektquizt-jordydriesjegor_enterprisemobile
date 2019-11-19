using System;

using JuryApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace JuryApp.Views
{
    public sealed partial class LogInPage : Page
    {
        private LogInViewModel ViewModel
        {
            get { return ViewModelLocator.Current.LogInViewModel; }
        }

        public LogInPage()
        {
            InitializeComponent();
        }
    }
}
