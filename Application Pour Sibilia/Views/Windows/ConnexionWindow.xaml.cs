using Microsoft.Extensions.DependencyInjection;
using Application_Pour_Sibilia.Services;
using Application_Pour_Sibilia.ViewModels.Windows;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Wpf.Ui;
using Wpf.Ui.Controls;
using static Application_Pour_Sibilia.ViewModels.Windows.MainWindowViewModel;

namespace Application_Pour_Sibilia.Views.Windows
{
    /// <summary>
    /// Logique d'interaction pour ConnexionWindow.xaml
    /// </summary>
    public partial class ConnexionWindow : FluentWindow
    {
        public ConnexionWindowViewModel ViewModel { get; set; }
        private readonly SessionService sessionService;

        public ConnexionWindow(SessionService sessionService)
        {
            InitializeComponent();
            ViewModel = App.Services.GetRequiredService<ConnexionWindowViewModel>();
            DataContext = ViewModel;
            this.sessionService = sessionService;

            if (ConfigurationManager.AppSettings["NeedAuth"] == "false")
            {
                sessionService.Login = ConfigurationManager.AppSettings["Login"];
                this.Loaded += (s, e) =>
                {
                    this.DialogResult = true;
                    this.Close();
                };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void SeConnecter_Click(object sender, RoutedEventArgs e)
        {
            ErrorTxt.Text = ViewModel.Connection(usernameBox.Text, passwordBox.Password);
            if (sessionService.Login is not null)
                this.DialogResult = true;
        }

        // méthode pour gérer l'appui sur Entrée
        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Déclenche la connexion quand on appuie sur Entrée
                SeConnecter_Click(sender, new RoutedEventArgs());
            }
        }
    }
}