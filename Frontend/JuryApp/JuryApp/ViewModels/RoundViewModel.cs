using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Helpers;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class RoundViewModel
    {
        private readonly INavigationServiceEx _navigationService;
        private readonly IMessengerCache _messengerCache;
        private readonly IRoundService _roundService;

        public Round SelectedRound { get; set; }
        public RoundViewModel(IRoundService roundService, INavigationServiceEx navigationService, IMessengerCache messengerCache)
        {
            _roundService = roundService;
            _navigationService = navigationService;
            _messengerCache = messengerCache;
            SelectedRound = _messengerCache.CachedSelectedRound;
            Messenger.Default.Register<Round>(this, (round) => { SelectedRound = round; });
        }

        public RelayCommand<int> AddNewAnswerCommand => new RelayCommand<int>(AddNewAnswer);
        public RelayCommand SaveRoundCommand => new RelayCommand(SaveRound);

        private async void SaveRound()
        {
            var result = await _roundService.EditRound(SelectedRound.RoundId, SelectedRound);

            if (result)
            {
                _navigationService.GoBack();
            }
        }


        private void AddNewAnswer(int selectedQuestionIndex)
        {
            if (selectedQuestionIndex == -1) return;

            SelectedRound.RoundQuestions[selectedQuestionIndex].QuestionCorrectAnswers.Add( new CorrectAnswer{CorrectAnswerText = ""} );
        }
    }
}
