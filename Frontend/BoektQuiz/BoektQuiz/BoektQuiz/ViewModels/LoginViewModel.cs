using BoektQuiz.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public String Username { get; set; }
        public String Password { get; set; }
        private String _status;
        public String Status { get => _status; set { if (_status == value) return; _status = value; OnPropertyChanged(); } }
        private Color _statusColor;
        public Color StatusColor { get => _statusColor; set { if (_statusColor == value) return; _statusColor = value; OnPropertyChanged(); } }

        private Command _loginCommand;

        public Command LoginCommand => _loginCommand ??
                                              (_loginCommand = new Command(OnLogin, CanLogin));

        private async void OnLogin()
        {
            var loginModel = new LoginModel() { Username = Username, Password = Password };

            var json = JsonConvert.SerializeObject(loginModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("http://10.0.2.2:8080/signin", data); //10.0.2.2 is a magic IP address which points to the emulating localhost (127.0.0.1)

                if (response.IsSuccessStatusCode)
                {
                    var token = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync());
                    Application.Current.Properties["token"] = token.Token;
                    Status = "Inloggen gelukt";
                    StatusColor = Color.Accent;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Status = "Onjuiste combinatie gebruikersnaam en wachtwoord";
                    } else
                    {
                        Status = "Er is iets misgelopen bij het inloggen";
                    }

                    StatusColor = Color.FromHex("ED028C");
                }
            }
        }

        private bool CanLogin()
        {
            if (Username != null && Password != null)
            {
                return (Username.Length > 3 && Password.Length > 6);
            }

            return false;
        }
    }
}
