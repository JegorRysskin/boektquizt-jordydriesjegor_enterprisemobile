using System;
using System.Collections.Generic;
using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.Views;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public List<Round> Rounds { get; set; }
        private IBackendService _backendService;
        private string _token;

        public AppShellViewModel(IBackendService backendService)
        {
            _backendService = backendService;
        }

        public void LoadRounds()
        {
            if (Application.Current != null) //If the application is indeed running, not a test
            {
                if (Application.Current.Properties.ContainsKey("token"))
                {
                    _token = Application.Current.Properties["token"].ToString();
                }
            }

            if (Connectivity.Instance.IsConnected)
            {
                if (_token != String.Empty)
                {
                    Rounds = _backendService.GetAllRounds(_token).Result;
                }
            }

            if (Application.Current != null)
            {
                if (Rounds != null)
                {
                    foreach (Round round in Rounds)
                    {
                        ShellSection shell_section = new ShellSection
                        {
                            Title = round.Name,
                            Icon = "tab_round_" + round.Id + ".png"
                        };

                        shell_section.Items.Add(new ShellContent() { Content = new RoundStartPage(round.Id) });

                        if (Application.Current.MainPage != null)
                        {
                            if (Application.Current.MainPage is AppShell shell)
                            {
                                shell.Items.Add(shell_section);
                            }
                        }
                    }
                }
            }
        }
    }
}
