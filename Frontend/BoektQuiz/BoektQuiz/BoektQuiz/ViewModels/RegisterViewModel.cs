using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public String TeamName { get; set; }
        public String Password { get; set; }

        private Command _registerTeamCommand;

        public Command RegisterTeamCommand => _registerTeamCommand ?? (_registerTeamCommand = new Command(OnRegisterTeam, CanRegisterTeam));

        private void OnRegisterTeam()
        {
            Console.WriteLine("Function OnRegisterTeam() Called.");
        }

        private bool CanRegisterTeam()
        {
            if (TeamName != null && Password != null)
            {
                return (TeamName.Length > 3 && Password.Length > 6);
            }

            return false;
        }
    }
}
