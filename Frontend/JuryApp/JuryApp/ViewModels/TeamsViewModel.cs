using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models;
using JuryApp.Core.Services;

namespace JuryApp.ViewModels
{
    public class TeamsViewModel : ViewModelBase, INotifyCollectionChanged
    {
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

        public TeamsViewModel()
        {
            _teamService = new TeamService();
            FetchListOfTeams();
        }

        private async void FetchListOfTeams()
        {
            Teams = await _teamService.GetAllTeams();
        }
    }
}
