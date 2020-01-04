using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;

namespace JuryApp.Helpers
{
    public class MessengerCache
    {
        public Quiz CachedSelectedQuiz { get; set; }
        public Team CachedSelectedTeam { get; set; }
        public bool CachedAlreadyOneEnabledQuiz { get; set; }
        public Round CachedSelectedRound { get; set; }

        public MessengerCache()
        {
            Messenger.Default.Register<Quiz>(this, (quiz) => { CachedSelectedQuiz = quiz; });
            Messenger.Default.Register<Team>(this, (team) => { CachedSelectedTeam = team; });
            Messenger.Default.Register<bool>(this, (b) => { CachedAlreadyOneEnabledQuiz = b; });
            Messenger.Default.Register<Round>(this, (round) => { CachedSelectedRound = round; });

        }
    }
}
