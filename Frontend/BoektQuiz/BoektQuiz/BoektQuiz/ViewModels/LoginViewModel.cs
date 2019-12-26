using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class LoginViewModel
    {
        public String TeamName { get; set; }
        public String Password { get; set; }

        private Command _loginCommand;

        public Command LoginCommand => _loginCommand ??
                                              (_loginCommand = new Command(OnLogin, CanLogin));

        private void OnLogin()
        {
            Console.WriteLine("Function OnLogin() Called.");
        }

        private bool CanLogin()
        {
            if (TeamName != null && Password != null)
            {
                return (TeamName.Length > 3 && Password.Length > 6);
            }

            return false;
        }
    }
}
