using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;

namespace JuryApp.Core.Services.Interfaces
{
    public interface IQuizService
    {
        Task<Quizzes> GetAllQuizzes(bool forceRefresh);
        Task<Quiz> GetQuizById(int id);
        Task<bool> AddQuiz(Quiz newQuiz);
        Task<bool> DeleteQuiz(int id);
    }

}
