using System.Threading.Tasks;
using JuryApp.Core.Models;

namespace JuryApp.Core.Services
{
    public abstract class LoginService
    {
        public static string AccessToken { get; set; }
        public static async void Login()
        {
            var httpDataService = new HttpDataService();
            var loginUser = new LoginUser {password = "admin", username = "admin"};
            var result = await httpDataService.GetLoginToken(loginUser);
            AccessToken = result;
        }
    }
}