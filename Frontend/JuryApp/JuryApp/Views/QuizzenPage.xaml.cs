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
        private QuizzenViewModel ViewModel => ViewModelLocator.Current.QuizzenViewModel;
        public QuizzenPage()
        {
            InitializeComponent();
        }
    }
}
