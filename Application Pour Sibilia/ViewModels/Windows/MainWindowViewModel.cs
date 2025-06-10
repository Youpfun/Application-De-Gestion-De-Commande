using Application_Pour_Sibilia.Views.Windows;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Application_Pour_Sibilia.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Application Pour Sibilia";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Menu principal",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
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
