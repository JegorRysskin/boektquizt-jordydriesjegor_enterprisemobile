using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BoektQuiz
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Shell.SetBackgroundColor(this, Color.FromHex("ED028C"));
            Shell.SetForegroundColor(this, Color.White);
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}
