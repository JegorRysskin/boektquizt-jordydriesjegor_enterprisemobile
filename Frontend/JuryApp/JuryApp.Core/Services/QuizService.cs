using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;

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
            var result = await _httpDataService.GetAsync<Quizzes>("quiz", LoginService.AccessToken, forceRefresh);
            return result;
        }

        public async Task<Quiz> GetQuizById(int id)
        {
            var result = await _httpDataService.GetAsync<Quiz>($"quiz/{id}");

            return result;
        }

        public async Task<bool> AddQuiz(Quiz newQuiz)
        {
            var result = await _httpDataService.PostAsJsonAsync("quiz", newQuiz);

            return result;
        }

        public async Task<bool> DeleteQuiz(int id)
        {
            var result = await _httpDataService.DeleteAsync($"quiz/{id}");

            return result;
        }
    }
}