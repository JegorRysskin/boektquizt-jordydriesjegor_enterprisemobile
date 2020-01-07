using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Helpers;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class RoundViewModel
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;
        private MessengerCache MessengerCache => ViewModelLocator.Current.MessengerCache;
        private readonly RoundService _roundService;

        public Round SelectedRound { get; set; }
        public RoundViewModel()
        {
            _roundService = new RoundService();
            SelectedRound = MessengerCache.CachedSelectedRound;
            Messenger.Default.Register<Round>(this, (round) => { SelectedRound = round; });
        }

        public RelayCommand<int> AddNewAnswerCommand => new RelayCommand<int>(AddNewAnswer);
        public RelayCommand SaveRoundCommand => new RelayCommand(SaveRound);

        private async void SaveRound()
        {
            var result = await _roundService.EditRound(SelectedRound.RoundId, SelectedRound);

            if (result)
            {
                NavigationService.GoBack();
            }
        }


        private void AddNewAnswer(int selectedQuestionIndex)
        {
            if (selectedQuestionIndex == -1) return;

            SelectedRound.RoundQuestions[selectedQuestionIndex].QuestionCorrectAnswers.Add( new CorrectAnswer{CorrectAnswerText = ""} );
        }
    }
}
