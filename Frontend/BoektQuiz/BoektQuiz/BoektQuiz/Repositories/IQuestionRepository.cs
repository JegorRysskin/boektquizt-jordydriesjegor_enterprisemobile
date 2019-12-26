using BoektQuiz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoektQuiz.Repositories
{
    public interface IQuestionRepository
    {
        Task<IList<Question>> GetQuestionsFromRound(int roundId);
    }
}
