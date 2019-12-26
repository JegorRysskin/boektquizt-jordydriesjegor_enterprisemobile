using BoektQuiz.Models;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using BoektQuiz.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BoektQuiz.Behavior
{
    public class ItemSelectBehavior : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView listView)
        {
            listView.ItemSelected += OnItemSelected;
            base.OnAttachedTo(listView);
        }

        protected override void OnDetachingFrom(ListView listView)
        {
            listView.ItemSelected -= OnItemSelected;
            base.OnDetachingFrom(listView);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).BindingContext is RoundOverviewViewModel rOVM)
            {
                if (e.SelectedItem is Round round)
                {
                    rOVM.ItemSelectCommand.Execute(round);
                }
            }
        }
    }
}
