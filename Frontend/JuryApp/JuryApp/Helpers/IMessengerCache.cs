using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;

namespace JuryApp.Helpers
{
    public interface IMessengerCache
    {
        Quizzes CachedAllQuizzes { get; set; }
        Quiz CachedSelectedQuiz { get; set; }
        Round CachedSelectedRound { get; set; }
        Team CachedSelectedTeam { get; set; }
    }
}