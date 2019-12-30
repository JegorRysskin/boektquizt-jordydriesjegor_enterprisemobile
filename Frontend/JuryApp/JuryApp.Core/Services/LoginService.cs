using JuryApp.Core.Models;
using System.Threading.Tasks;

namespace JuryApp.Core.Services
{
    public abstract class LoginService
    {
        public static async Task<string> Login()
        {
            var httpDataService = new HttpDataService();
            var loginUser = new LoginUser { password = "admin", username = "admin" };
            var result = await httpDataService.GetLoginToken(loginUser);
            return result;
        }
    }
}