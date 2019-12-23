using System;
using System.Collections.Generic;
using System.Linq;
using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using BoektQuiz.Views;
using Xamarin.Forms;

namespace BoektQuiz
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        AppShellViewModel viewModel;

        public AppShell()
        {
            InitializeComponent();

            Shell.SetBackgroundColor(this, Color.FromHex("ED028C"));
            Shell.SetForegroundColor(this, Color.White);
            Shell.SetTabBarIsVisible(this, false);

            viewModel = new AppShellViewModel(this);
            BindingContext = viewModel;
        }
    }
}
