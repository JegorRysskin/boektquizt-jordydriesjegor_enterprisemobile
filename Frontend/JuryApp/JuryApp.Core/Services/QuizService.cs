using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JuryApp.Core.Models;

namespace JuryApp.Core.Services
{
    public class QuizService : IQuizService
    {
        private HttpDataService _httpDataService;
        public QuizService()
        {
            _httpDataService = new HttpDataService();
        }
        public async Task<ObservableCollection<Quiz>> GetAllQuizzes()
        {
            var result = await _httpDataService.GetAsync<ObservableCollection<Quiz>>("quiz");
            
            return result;
        }
    }
}