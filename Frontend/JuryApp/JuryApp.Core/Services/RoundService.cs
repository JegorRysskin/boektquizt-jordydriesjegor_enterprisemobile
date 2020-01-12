﻿using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace JuryApp.Core.Services
{
    public class RoundService : IRoundService
    {
        private readonly HttpDataService _httpDataService;

        public RoundService()
        {
            _httpDataService = new HttpDataService();
        }

        public async Task<Rounds> GetAllRoundsByEnabledQuiz(bool forceRefresh)
        {
            var result = await _httpDataService.GetAsync<Rounds>("round", await LoginService.Login(), forceRefresh);

            return result;
        }

        public async Task<bool> EditRound(int id, Round editedRound)
        {
            var result = await _httpDataService.PatchAsJsonAsync($"round/{id}", editedRound, await LoginService.Login());

            return result;
        }
    }
}