using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class TeamsViewModel : ViewModelBase, INotifyCollectionChanged
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private ObservableCollection<Team> _teams;
        private readonly TeamService _teamService;

        public ObservableCollection<Team> Teams
        {
            get => _teams;
            set
            {
                _teams = value;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public RelayCommand<int> EditTeamCommand => new RelayCommand<int>(NavigateToEditTeamPage);
        public TeamsViewModel()
        {
            _teamService = new TeamService();
            FetchListOfTeams(false);
            NavigationService.Navigated += NavigationService_Navigated;
        }
        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfTeams(true);
        }
        private void NavigateToEditTeamPage(int selectedIndex)
        {
            if (selectedIndex != -1)
            {
                Messenger.Default.Send(_teams[selectedIndex]);
                NavigationService.Navigate(typeof(EditTeamViewModel).FullName);
            }
        }
        private async void FetchListOfTeams(bool forceRefresh)
        {
            Teams = await _teamService.GetAllTeams(forceRefresh);
        }
    }
}
