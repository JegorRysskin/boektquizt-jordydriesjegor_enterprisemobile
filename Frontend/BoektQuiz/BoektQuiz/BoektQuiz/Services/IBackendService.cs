using BoektQuiz.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BoektQuiz.Services
{
    public interface IBackendService
    {
        Task<String> Login(String username, String password);
        Task<HttpStatusCode> Register(String username, String password);
        Task<List<Round>> GetAllRounds(String token);
        Task<Round> GetRoundById(int id, String token);
        Task<HttpStatusCode> PatchRound(Round round, String token);
        Task<Team> GetTeamByName(string teamName);
    }
}
