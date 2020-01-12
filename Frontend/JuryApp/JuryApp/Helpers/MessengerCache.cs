using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;

namespace JuryApp.Helpers
{
    public class MessengerCache : IMessengerCache
    {
        public Quiz CachedSelectedQuiz { get; set; }
        public Team CachedSelectedTeam { get; set; }
        public Quizzes CachedAllQuizzes { get; set; }
        public Round CachedSelectedRound { get; set; }

        public MessengerCache()
        {
            Messenger.Default.Register<Quiz>(this, (quiz) => { CachedSelectedQuiz = quiz; });
            Messenger.Default.Register<Team>(this, (team) => { CachedSelectedTeam = team; });
            Messenger.Default.Register<Quizzes>(this, (qz) => { CachedAllQuizzes = qz; });
            Messenger.Default.Register<Round>(this, (round) => { CachedSelectedRound = round; });

        }
    }
}
