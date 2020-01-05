using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Helpers;

namespace JuryApp.ViewModels
{
    public class RoundViewModel
    {
        private MessengerCache MessengerCache => ViewModelLocator.Current.MessengerCache;

        public Round SelectedRound { get; set; }
        public RoundViewModel()
        {
            SelectedRound = MessengerCache.CachedSelectedRound;
            Messenger.Default.Register<Round>(this, (round) => { SelectedRound = round; });
        }
    }
}
