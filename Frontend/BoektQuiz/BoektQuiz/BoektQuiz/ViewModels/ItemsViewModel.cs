using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using BoektQuiz.Models;
using BoektQuiz.Views;

namespace BoektQuiz.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Vraag> Vragen { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Vragen = new ObservableCollection<Vraag>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Vragen.Clear();
                var vragen = await DataStore.GetItemsAsync(true);
                foreach (var vraag in vragen)
                {
                    Vragen.Add(vraag);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}