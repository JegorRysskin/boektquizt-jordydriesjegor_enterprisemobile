using BoektQuiz.Models;
using JWT;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BoektQuiz.Services
{
    public class BackendService : IBackendService
    {
        private static readonly string baseUrl = "http://10.0.2.2:8080/"; //10.0.2.2 is a magic IP address which points to the emulating localhost (127.0.0.1)

        public async Task<List<Round>> GetAllRounds(string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                var response = await client.GetAsync(baseUrl + "round").ConfigureAwait(false);

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
                var response = await client.GetAsync(baseUrl + "round/" + id).ConfigureAwait(false);

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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, baseUrl + "round")
                {
                    Content = data
                };

                var response = await client.SendAsync(request);

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
                var response = await client.PostAsync(baseUrl + "signin", data);

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

        public async Task<Team> GetTeamByToken(string token)
        {
            var serializer = new JsonNetSerializer();
            var provider = new UtcDateTimeProvider();
            var validator = new JwtValidator(serializer, provider);
            var urlEncoder = new JwtBase64UrlEncoder();
            var decoder = new JwtDecoder(serializer, validator, urlEncoder);

            var result = decoder.Decode(token);

            var jwtToken = JsonConvert.DeserializeObject<JWTToken>(result);

            string teamName = jwtToken.Sub;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                var response = await client.GetAsync(baseUrl + "team/name/" + teamName).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var team = JsonConvert.DeserializeObject<Team>(await response.Content.ReadAsStringAsync());
                    return team;
                }
            }

            return null;
        }

        public async Task<HttpStatusCode> PatchTeamAnswer(Answer answer, Team team, string token)
        {
            var json = JsonConvert.SerializeObject(answer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, baseUrl + "team/answer/" + team.Id)
                {
                    Content = data
                };

                var response = await client.SendAsync(request);

                return response.StatusCode;
            }
        }
    }
}
