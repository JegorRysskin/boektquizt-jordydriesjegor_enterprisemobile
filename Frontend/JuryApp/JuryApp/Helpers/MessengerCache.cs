﻿using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;

namespace JuryApp.Helpers
{
    public class MessengerCache
    {
        public Quiz CachedSelectedQuiz { get; set; }
        public Team CachedSelectedTeam { get; set; }

        public MessengerCache()
        {
            Messenger.Default.Register<Quiz>(this, (quiz) => { CachedSelectedQuiz = quiz; });
            Messenger.Default.Register<Team>(this, (team) => { CachedSelectedTeam = team; });
        }
    }
}
