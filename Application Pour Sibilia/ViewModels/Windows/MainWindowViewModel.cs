using Application_Pour_Sibilia.Services;
using Application_Pour_Sibilia.Views.Windows;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Application_Pour_Sibilia.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly SessionService _sessionService;

        [ObservableProperty]
        private string _applicationTitle = "Application Pour Sibilia";

        [ObservableProperty]
        private string _welcomeMessage;

        public MainWindowViewModel(SessionService sessionService)
        {
            _sessionService = sessionService;
            // Écoute des changements de prénom et de nom
            _sessionService.PropertyChanged += (sender, e) => 
            {
                if (e.PropertyName == nameof(SessionService.Prenom) || 
                    e.PropertyName == nameof(SessionService.Nom))
                    UpdateWelcomeMessage();
            };
            
            // Initialisation du message de bienvenue
            UpdateWelcomeMessage();
        }

        private void UpdateWelcomeMessage()
        {
            string userInfo = $"{_sessionService.Prenom} {_sessionService.Nom}";
            WelcomeMessage = $"Connecté en tant que : {userInfo}";
        }

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            
            new NavigationViewItem()
            {
                Content = "Gestion des plats",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DocumentTable24 },
                TargetPageType = typeof(Views.Pages.GestionDesPlatsPage)
            },
            new NavigationViewItem()
            {
                Content = "Client",
                Icon = new SymbolIcon { Symbol = SymbolRegular.PeopleCommunity24 },
                TargetPageType = typeof(Views.Pages.GestionClientPage)
            },
            new NavigationViewItem()
            {
                Content = "Commande",
                Icon = new SymbolIcon { Symbol = SymbolRegular.BoxSearch24 },
                TargetPageType = typeof(Views.Pages.ConsulterCommandePage)
            },
            new NavigationViewItem()
            {
                Content = "Créer Commande",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Food24 },
                TargetPageType = typeof(Views.Pages.CreationCommandePage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Paramètres",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
