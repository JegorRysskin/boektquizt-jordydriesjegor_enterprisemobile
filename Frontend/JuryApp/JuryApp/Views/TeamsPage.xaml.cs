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
        private TeamsViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TeamsViewModel; }
        }

        public TeamsPage()
        {
            InitializeComponent();
        }

        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await DeleteContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ViewModel.Teams.RemoveAt(Teams.SelectedIndex);
                DeleteButton.Content = "Verwijder";
            }
        }

        private void Teams_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DeleteButton.IsEnabled == false)
                DeleteButton.IsEnabled = true;
            
            if(Teams.SelectedIndex != -1)
                DeleteButton.Content = $"Verwijder {Environment.NewLine}{ViewModel.Teams[Teams.SelectedIndex].TeamName}";
        }
    }
}
