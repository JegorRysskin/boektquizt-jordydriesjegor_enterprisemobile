using System;
using System.Collections;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models;
using JuryApp.Views;

namespace JuryApp.ViewModels
{
    public class TeamsViewModel : ViewModelBase
    {
        public IList Items { get; set; }

        public TeamsViewModel()
        {
            Items = new List<Team>
            {
                new Team{TeamId = 1, TeamName = "TeamA"},
                new Team{TeamId = 2, TeamName = "TeamB"},
                new Team{TeamId = 3, TeamName = "TeamC"},
                new Team{TeamId = 4, TeamName = "TeamD"},
                new Team{TeamId = 5, TeamName = "TeamE"},
                new Team{TeamId = 6, TeamName = "TeamF"}
            };
        }
    }
}
