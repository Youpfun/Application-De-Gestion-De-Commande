using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.Views.Windows;
using Application_Pour_Sibilia.Services;
using Npgsql;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;
using System.Windows.Controls;
using Wpf.Ui;

namespace Application_Pour_Sibilia.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public partial class ConnexionWindowViewModel : ObservableObject
        {
            private readonly SessionService sessionService;

            public ConnexionWindowViewModel(SessionService sessionService)
            {
                this.sessionService = sessionService;
            }

            public string Connection(string username, string password)
            {
                string message;

                try
                {
                    var cmd = new NpgsqlCommand("SELECT login FROM employe WHERE login = '" + username + "' AND password = '" + password + "';");
                    object result = DataAccess.Instance.ExecuteSelectUneValeur(cmd);
                    if (result != null)
                    {
                        sessionService.Login = result.ToString();
                        return result.ToString();
                    }
                    else message = "Informations incorrects";
                }
                catch (Exception ex)
                {
                    message = $"Erreur : {ex.Message}";
                }

                return message;
            }
        }
    }
}
