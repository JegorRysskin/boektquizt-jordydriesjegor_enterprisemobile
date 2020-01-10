using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private String _status;
        public String Status { get => _status; set { if (_status == value) return; _status = value; OnPropertyChanged(); } }

        private Color _statusColor;
        public Color StatusColor { get => _statusColor; set { if (_statusColor == value) return; _statusColor = value; OnPropertyChanged(); } }

        public Command UpdateStatusCommand { get; set; }

        public HomeViewModel()
        {
            UpdateStatus();
            UpdateStatusCommand = new Command(() => UpdateStatus());
        }

        private void UpdateStatus()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (Application.Current != null)
                {
                    if (Application.Current.Properties.ContainsKey("token"))
                    {
                        Status = "Selecteer een ronde uit het hamburgermenu!";
                        StatusColor = Color.Accent;
                    }
                    else
                    {
                        Status = "U dient zich eerst in te loggen voordat u de rondes kan ophalen.\nU dient zich eerst te registreren voordat u kan inloggen.";
                        StatusColor = Color.FromHex("ED028C");
                    }
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
