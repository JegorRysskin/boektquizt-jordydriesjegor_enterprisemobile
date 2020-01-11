using BoektQuiz.Models;
using System.Threading.Tasks;

namespace BoektQuiz.Repositories
{
    public interface IAnswerRepository
    {
        Task<Answer> GetAnswerFromQuestion(int questionId);
    }
}
