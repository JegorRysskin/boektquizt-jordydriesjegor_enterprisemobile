using System.Threading.Tasks;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;

namespace JuryApp.Core.Services
{
    public class RoundService : IRoundService
    {
        private readonly HttpDataService _httpDataService;

        public RoundService()
        {
            _httpDataService = new HttpDataService();
        }

        public async Task<Rounds> GetAllRoundsByEnabledQuiz()
        {
            var result = await _httpDataService.GetAsync<Rounds>("round", await LoginService.Login());

            return result;
        }

        public async Task<bool> EditRound(int id, Round editedRound)
        {
            var result = await _httpDataService.PatchAsJsonAsync($"round/{id}", editedRound, await LoginService.Login());

            return result;
        }
    }
}