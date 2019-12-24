using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JuryApp.Core.Models;
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
        public async Task<ObservableCollection<Quiz>> GetAllQuizzes(bool forceRefresh)
        {
            var result = await _httpDataService.GetAsync<ObservableCollection<Quiz>>("quiz", forceRefresh: forceRefresh);
            
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