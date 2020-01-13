using JuryApp.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JuryApp.Core.Services
{
    // This class provides a wrapper around common functionality for HTTP actions.
    // Learn more at https://docs.microsoft.com/windows/uwp/networking/httpclient
    public class HttpDataService
    {
        private HttpClient client;

        public HttpDataService(string defaultBaseUrl = "http://localhost:8080")
        {
            client = new HttpClient();

            if (!string.IsNullOrEmpty(defaultBaseUrl))
            {
                client.BaseAddress = new Uri($"{defaultBaseUrl}/");
            }

        }

        public async Task<T> GetAsync<T>(string uri, string accessToken = null)
        {
            AddAuthorizationHeader(accessToken);
            var json = await client.GetStringAsync(uri);
            var result = await Task.Run(() => JsonConvert.DeserializeObject<T>(json));

            return result;
        }

        public async Task<bool> PostAsJsonAsync<T>(string uri, T item, string accessToken = null)
        {
            if (item == null)
            {
                return false;
            }

            var serializedItem = JsonConvert.SerializeObject(item);
            AddAuthorizationHeader(accessToken);
            var response = await client.PostAsync(uri, new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetLoginToken(LoginUser user, string uri = "signin")
        {
            var serializedItem = JsonConvert.SerializeObject(user);
            var response = await client.PostAsync(uri, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseContent)["token"].ToString();
        }

        public async Task<bool> PatchAsJsonAsync<T>(string uri, T item, string accessToken = null)
        {
            if (item == null)
            {
                return false;
            }

            var serializedItem = JsonConvert.SerializeObject(item);
            AddAuthorizationHeader(accessToken);

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(serializedItem, Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string uri, string accessToken = null)
        {
            AddAuthorizationHeader(accessToken);
            var response = await client.DeleteAsync(uri);

            return response.IsSuccessStatusCode;
        }

        // Add this to all public methods
        private void AddAuthorizationHeader(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = null;
                return;
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
