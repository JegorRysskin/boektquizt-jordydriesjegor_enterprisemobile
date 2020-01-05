using BoektQuiz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BoektQuiz.Services
{
    public class BackendService : IBackendService
    {
        private static readonly string baseUrl = "http://10.0.2.2:8080/";

        public async Task<List<Round>> GetAllRounds(string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                var response = await client.GetAsync(baseUrl + "round").ConfigureAwait(false); //10.0.2.2 is a magic IP address which points to the emulating localhost (127.0.0.1)

                if (response.IsSuccessStatusCode)
                {
                    var rounds = JsonConvert.DeserializeObject<List<Round>>(await response.Content.ReadAsStringAsync());
                    return rounds;
                }

                return null;
            }
        }

        public async Task<Round> GetRoundById(int id, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                var response = await client.GetAsync(baseUrl + "round/" + id).ConfigureAwait(false); //10.0.2.2 is a magic IP address which points to the emulating localhost (127.0.0.1)

                if (response.IsSuccessStatusCode)
                {
                    var round = JsonConvert.DeserializeObject<Round>(await response.Content.ReadAsStringAsync());
                    return round;
                }

                return null;
            }
        }

        public async Task<HttpStatusCode> PatchRound(Round round, string token)
        {
            var json = JsonConvert.SerializeObject(round);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(baseUrl + "round", data); //10.0.2.2 is a magic IP address which points to the emulating localhost (127.0.0.1)

                return response.StatusCode;
            }
        }

        public async Task<String> Login(string username, string password)
        {
            var loginModel = new LoginModel() { Username = username, Password = password };

            var json = JsonConvert.SerializeObject(loginModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(baseUrl + "signin", data); //10.0.2.2 is a magic IP address which points to the emulating localhost (127.0.0.1)

                if (response.IsSuccessStatusCode)
                {
                    var token = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync());
                    return token.Token;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return "401";
                    }
                    else
                    {
                        return "500";
                    }
                }
            }
        }

        public async Task<HttpStatusCode> Register(string username, string password)
        {
            var registerModel = new RegisterModel() { Username = username, Password = password };

            var json = JsonConvert.SerializeObject(registerModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(baseUrl + "signup", data); //10.0.2.2 is a magic IP address which points to the emulating localhost (127.0.0.1)

                return response.StatusCode;
            }
        }
    }
}
