using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.Services;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class GestionDesPlatsPage : INavigableView<GestionDesPlatsViewModel>
    {
        public GestionDesPlatsViewModel ViewModel { get; }
        private readonly SessionService _sessionService;

        public GestionDesPlatsPage(GestionDesPlatsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            // Récupérer le service de session
            _sessionService = App.Services.GetRequiredService<SessionService>();
            
            // Configurer l'accès au bouton selon le rôle
            ConfigurerAccesSelonRole();
        }

        private void ConfigurerAccesSelonRole()
        {
            // Si l'utilisateur est un vendeur (rôle 1), masquer le bouton de création de plat
            if (_sessionService.EstVendeur)
            {
                buttonNouveau.Visibility = Visibility.Collapsed;
            }
        }

        private void buttonNouveauPlat_Click(object sender, RoutedEventArgs e)
        {
            Plat unPlat = new Plat();
            Windows.PlatWindow wPlat = new Windows.PlatWindow(unPlat, typeAction.Créer);
            bool? result = wPlat.ShowDialog();
            if (result == true)
            {
                try
                {
                    unPlat.NumPlat = unPlat.Create();
                    ViewModel.AjouterPlat(unPlat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le Plat n'a pas pu être créé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonReinitialiser_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ReinitialiserFiltresCommand.Execute(null);
        }
    }
}