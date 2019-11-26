using System;
using System.Collections;
using System.Collections.Generic;
using JuryApp.ViewModels;

using Windows.UI.Xaml.Controls;
using JuryApp.Core.Models;

namespace JuryApp.Views
{
    public sealed partial class TeamsPage : Page
    {
        private TeamsViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TeamsViewModel; }
        }

        public TeamsPage()
        {
            InitializeComponent();
        }

        private void Teams_OnItemClick_ItemClick(object sender, ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
