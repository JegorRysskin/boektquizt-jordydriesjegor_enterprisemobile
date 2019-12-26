using System;
using System.Collections.Generic;
using System.Linq;
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

        public AppShellViewModel(AppShell appShell, IDataStore<Round> dataStore)
        {
            Rounds = dataStore.GetItemsAsync().Result.ToList();

            foreach (Round round in Rounds)
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
