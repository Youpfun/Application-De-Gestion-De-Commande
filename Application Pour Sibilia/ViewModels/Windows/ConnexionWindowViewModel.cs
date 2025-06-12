using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.Views.Windows;
using Application_Pour_Sibilia.Services;
using Npgsql;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;
using System.Windows.Controls;
using Wpf.Ui;
using System.Data; // Ajout de l'espace de noms requis pour utiliser DataTable

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
                    var cmd = new NpgsqlCommand("SELECT login, prenomemploye, nomemploye FROM employe WHERE login = @login AND password = @password;");
                    cmd.Parameters.AddWithValue("login", username);
                    cmd.Parameters.AddWithValue("password", password);
                    
                    DataTable result = DataAccess.Instance.ExecuteSelect(cmd);
                    
                    if (result.Rows.Count > 0)
                    {
                        sessionService.Login = result.Rows[0]["login"].ToString();
                        sessionService.Prenom = result.Rows[0]["prenomemploye"].ToString();
                        sessionService.Nom = result.Rows[0]["nomemploye"].ToString();
                        return sessionService.Login;
                    }
                    else message = "Informations incorrectes";
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
