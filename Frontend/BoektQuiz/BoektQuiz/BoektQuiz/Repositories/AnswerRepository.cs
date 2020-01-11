using BoektQuiz.Context;
using BoektQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BoektQuiz.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly BoektQuizContext _context;

        public AnswerRepository(BoektQuizContext context)
        {
            _context = context;
        }

        public async Task<Answer> GetAnswerFromQuestion(int questionId)
        {
            return _context.Answers.Where(a => a.QuestionId == questionId).FirstOrDefault();
        }
    }
}
