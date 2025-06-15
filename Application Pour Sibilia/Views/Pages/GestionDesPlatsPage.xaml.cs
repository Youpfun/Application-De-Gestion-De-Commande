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

        /// <summary>
        /// Initialise la page de gestion des plats et configure l'accès selon le rôle utilisateur.
        /// </summary>
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

        /// <summary>
        /// Configure l'accès aux fonctionnalités selon le rôle de l'utilisateur connecté.
        /// </summary>
        private void ConfigurerAccesSelonRole()
        {
            // Si l'utilisateur est un vendeur (rôle 1), masquer le bouton de création de plat
            if (_sessionService.EstVendeur)
            {
                buttonNouveau.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton pour créer un nouveau plat.
        /// Ouvre la fenêtre de création et ajoute le plat si la création est validée.
        /// </summary>
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

        /// <summary>
        /// Gère le clic sur le bouton de réinitialisation des filtres.
        /// Exécute la commande de réinitialisation des filtres du ViewModel.
        /// </summary>
        private void buttonReinitialiser_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ReinitialiserFiltresCommand.Execute(null);
        }
    }
}