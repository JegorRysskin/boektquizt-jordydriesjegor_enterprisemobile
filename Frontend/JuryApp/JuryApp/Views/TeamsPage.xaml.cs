using System;
using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml;
using JuryApp.ViewModels;
using Windows.UI.Xaml.Controls;
using JuryApp.Core.Models;
using JuryApp.Core.Services;

namespace JuryApp.Views
{
    public sealed partial class TeamsPage : Page
    {
        private TeamsViewModel ViewModel => ViewModelLocator.Current.TeamsViewModel;

        public TeamsPage()
        {
            InitializeComponent();
        }

    }
}
