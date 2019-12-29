﻿using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace JuryApp.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly HttpDataService _httpDataService;

        public TeamService()
        {
            _httpDataService = new HttpDataService();
        }

        public async Task<Teams> GetAllTeams(bool forceRefresh)
        {
            var result = await _httpDataService.GetAsync<Teams>("team", await LoginService.Login(), forceRefresh);
            return result;
        }
        public async Task<bool> DeleteTeam(int id)
        {
            var result = await _httpDataService.DeleteAsync($"users/{id}", await LoginService.Login());
            return result;
        }
    }
}