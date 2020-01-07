using BoektQuiz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoektQuiz.Repositories
{
    public interface IQuestionRepository
    {
        Task<IList<Question>> GetQuestionsFromRound(int roundId);
    }
}
