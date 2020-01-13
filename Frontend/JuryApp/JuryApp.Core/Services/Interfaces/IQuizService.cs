using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using System.Threading.Tasks;

namespace JuryApp.Core.Services.Interfaces
{
    public interface IQuizService
    {
        Task<Quiz> GetQuizById(int id);
        Task<Quizzes> GetAllQuizzes(bool forceRefresh);
        Task<bool> AddQuiz(Quiz newQuiz);
        Task<bool> DeleteQuiz(int id);
        Task<bool> EditQuiz(int id, Quiz editedQuiz);
    }

}
