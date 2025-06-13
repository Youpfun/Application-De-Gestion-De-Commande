using Application_Pour_Sibilia.Services;
using Application_Pour_Sibilia.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui;  // Ajout de cet import
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Appearance;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        private readonly SessionService _sessionService;

        [ObservableProperty]
        private string _appVersion = String.Empty;

        [ObservableProperty]
        private ApplicationTheme _currentTheme = ApplicationTheme.Unknown;

        public SettingsViewModel(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public Task OnNavigatedToAsync()
        {
            if (!_isInitialized)
                InitializeViewModel();

            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync() => Task.CompletedTask;

        private void InitializeViewModel()
        {
            CurrentTheme = ApplicationThemeManager.GetAppTheme();
            AppVersion = $"Application Pour Sibilia - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                ?? String.Empty;
        }

        [RelayCommand]
        private void OnChangeTheme(string parameter)
        {
            switch (parameter)
            {
                case "theme_light":
                    if (CurrentTheme == ApplicationTheme.Light)
                        break;

                    ApplicationThemeManager.Apply(ApplicationTheme.Light);
                    CurrentTheme = ApplicationTheme.Light;

                    break;

                default:
                    if (CurrentTheme == ApplicationTheme.Dark)
                        break;

                    ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                    CurrentTheme = ApplicationTheme.Dark;

                    break;
            }
        }

        [RelayCommand]
        private void Deconnecter()
        {
            // Demande de confirmation à l'utilisateur
            MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir vous déconnecter ?", 
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Obtenir une référence à la fenêtre principale actuelle
                    Window mainWindow = Application.Current.MainWindow;
                    
                    // Réinitialiser les données de session
                    _sessionService.Login = null;
                    _sessionService.Nom = null;
                    _sessionService.Prenom = null;
                    _sessionService.NumEmploye = 0;
                    _sessionService.RoleEmploye = 0;

                    // Créer une nouvelle fenêtre de connexion
                    ConnexionWindow connexionWindow = new ConnexionWindow(_sessionService);
                    
                    // Afficher la fenêtre de connexion
                    bool? loginResult = connexionWindow.ShowDialog();
                    
                    // Si la connexion réussit
                    if (loginResult == true)
                    {
                        // Au lieu d'essayer de créer une nouvelle instance de MainWindow,
                        // naviguez simplement vers la page d'accueil
                        if (mainWindow is INavigationWindow navWindow)
                        {
                            navWindow.Navigate(typeof(Views.Pages.ConsulterCommandePage));
                        }
                    }
                    else
                    {
                        // Si l'utilisateur annule la connexion, fermer l'application
                        Application.Current.Shutdown();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la déconnexion : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
