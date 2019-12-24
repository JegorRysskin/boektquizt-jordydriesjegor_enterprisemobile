using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Views;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        private MockDataStore dataStore = new MockDataStore();

        public List<Round> Rounds { get; set; }

        public AppShell Shell { get; set; }

        public AppShellViewModel(AppShell appShell)
        {
            List<Round> rounds = dataStore.GetItemsAsync().Result.ToList();

            foreach (Round round in rounds)
            {
                ShellSection shell_section = new ShellSection
                {
                    Title = round.Text,
                    Icon = "tab_round_" + round.Id + ".png"
                };

                shell_section.Items.Add(new ShellContent() { Content = new RoundStartPage(round.Id) });

                appShell.Items.Add(shell_section);
            }
        }
    }
}
