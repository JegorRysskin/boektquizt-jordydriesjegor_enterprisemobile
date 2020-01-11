using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class RoundOverviewViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IRoundRepository _roundRepository;

        private IList<Round> _rounds;
        public IList<Round> Rounds { get => _rounds; set { if (_rounds == value) return; _rounds = value; OnPropertyChanged(); } }

        private Command _itemSelectCommand;
        public Command ItemSelectCommand =>
            _itemSelectCommand ?? (_itemSelectCommand = new Command(OnItemSelect, CanItemSelect));

        public Command LoadItemsCommand { get; set; }

        private bool CanItemSelect(object arg)
        {
            if (arg is Round round)
            {
                if (round.Questions != null)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnItemSelect(object obj)
        {
            if (obj is Round round)
            {
                _navigationService.NavigateToAsync(RoutingConstants.QuestionOverviewRoute);
                MessagingCenter.Send(this, "Round", round);
            }
        }

        public RoundOverviewViewModel(INavigationService navigationService, IRoundRepository roundRepository)
        {
            _navigationService = navigationService;
            _roundRepository = roundRepository;

            Rounds = roundRepository.GetAllRoundsAsync().Result;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Rounds.Clear();
                Rounds = await _roundRepository.GetAllRoundsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
