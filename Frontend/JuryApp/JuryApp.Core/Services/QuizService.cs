using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace JuryApp.Core.Services
{
    public class QuizService : IQuizService
    {
        private readonly HttpDataService _httpDataService;

        public QuizService()
        {
            _httpDataService = new HttpDataService();
        }

        public async Task<Quizzes> GetAllQuizzes(bool forceRefresh)
        {
            var result = await _httpDataService.GetAsync<Quizzes>("quiz", await LoginService.Login(), forceRefresh);
            return result;
        }

        public async Task<Quiz> GetQuizById(int id)
        {
            var result = await _httpDataService.GetAsync<Quiz>($"quiz/{id}");

            return result;
        }

        public async Task<bool> AddQuiz(Quiz newQuiz)
        {
            var result = await _httpDataService.PostAsJsonAsync("quiz", newQuiz, await LoginService.Login());

            return result;
        }

        public async Task<bool> DeleteQuiz(int id)
        {
            var result = await _httpDataService.DeleteAsync($"quiz/{id}", await LoginService.Login());

            return result;
        }
    }
}