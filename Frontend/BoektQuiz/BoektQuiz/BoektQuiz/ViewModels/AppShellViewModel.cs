using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Views;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public List<Round> Rounds { get; set; }

        private AppShell _shell;
        private IBackendService _backendService;

        public AppShellViewModel(AppShell shell, IBackendService backendService)
        {
            _shell = shell;
            _backendService = backendService;

            if (Application.Current.Properties.ContainsKey("token"))
            {
                LoadRounds();
            }
        }

        public void LoadRounds()
        {
            string token = Application.Current.Properties["token"].ToString();
            Rounds = _backendService.GetAllRounds(token).Result;

            foreach (Round round in Rounds)
            {
                ShellSection shell_section = new ShellSection
                {
                    Title = round.Text,
                    Icon = "tab_round_" + round.Id + ".png"
                };

                shell_section.Items.Add(new ShellContent() { Content = new RoundStartPage(round.Id) });

                _shell.Items.Add(shell_section);
            }
        }
    }
}
