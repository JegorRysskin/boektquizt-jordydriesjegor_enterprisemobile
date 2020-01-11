using BoektQuiz.Services;
using BoektQuiz.Util;
using System;
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

        private IBackendService _backendService;

        public LoginViewModel(IBackendService backendService)
        {
            _backendService = backendService;
            Connectivity.Instance.ConnectivityChanged += Instance_ConnectivityChanged;

            if (!Connectivity.Instance.IsConnected)
            {
                Status = "U moet verbonden zijn met het internet om te kunnen inloggen.";
                StatusColor = Color.FromHex("ED028C");
            }
        }

        private void Instance_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (!Connectivity.Instance.IsConnected)
            {
                Status = "U moet verbonden zijn met het internet om te kunnen inloggen.";
                StatusColor = Color.FromHex("ED028C");
            } 
            else
            {
                Status = String.Empty;
                StatusColor = Color.Transparent;
            }

            LoginCommand.ChangeCanExecute();
        }

        private async void OnLogin()
        {
            var status = await _backendService.Login(Username, Password);

            if (status == "401")
            {
                Status = "Onjuiste combinatie van gebruikersnaam en paswoord.";
                StatusColor = Color.FromHex("ED028C");
            }
            else if (status == "500") 
            {
                Status = "Er is iets misgelopen bij het inloggen.";
                StatusColor = Color.FromHex("ED028C");
            } 
            else
            {
                Status = "Inloggen gelukt";
                StatusColor = Color.Accent;
                Application.Current.Properties["token"] = status;
                if (Application.Current.MainPage is AppShell shell)
                {
                    if (shell.BindingContext is AppShellViewModel aSVM)
                    {
                        aSVM.LoadRounds();
                    }
                }
            }
        }

        private bool CanLogin()
        {
            if (Connectivity.Instance.IsConnected)
            {
                if (Username != null && Password != null)
                {
                    return (Username.Length > 3 && Password.Length > 6);
                }
            }

            return false;
        }
    }
}
