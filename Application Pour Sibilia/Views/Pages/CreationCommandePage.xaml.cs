using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
using Microsoft.VisualBasic;
using System.Net;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class CreationCommandePage : Page
    {
        private Client clientSelectionne;

        public CreationCommandeViewModel ViewModel { get; }

        /// <summary>
        /// Initialise la page de création de commande.
        /// </summary>
        public CreationCommandePage()
        {
            InitializeComponent();
            ViewModel = new CreationCommandeViewModel();
            DataContext = ViewModel;
            ViewModel.LignesCommande.CollectionChanged += LignesCommande_CollectionChanged;
        }

        /// <summary>
        /// Initialise la page de création de commande avec un client sélectionné.
        /// </summary>
        public CreationCommandePage(Client clientSelectionne)
        {
            InitializeComponent();
            ViewModel = new CreationCommandeViewModel();
            DataContext = ViewModel;

            this.clientSelectionne = clientSelectionne;

            ViewModel.ClientSelectionne = clientSelectionne;
            ViewModel.LignesCommande.CollectionChanged += LignesCommande_CollectionChanged;
        }

        /// <summary>
        /// Fait défiler la liste des lignes de commande lors d'un changement.
        /// </summary>
        private void LignesCommande_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ScrollCommande?.ScrollToEnd();
        }

        /// <summary>
        /// Ouvre la page de gestion des clients pour sélectionner un client.
        /// </summary>
        private void buttonSelectionClient_Click(object sender, RoutedEventArgs e)
        {
            GestionClientPage gestionClientPage = new GestionClientPage(new GestionClientViewModel());
            NavigationService?.Navigate(gestionClientPage);
        }
        /// <summary>
        /// Valide et enregistre la commande.
        /// </summary>
        private void buttonValiderCommande_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie que les informations minimales sont bien renseignées
            if (ViewModel.ClientSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ViewModel.DateRetraitPrevue == null)
            {
                MessageBox.Show("Veuillez choisir une date de retrait.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Appel de la méthode du ViewModel qui enregistre la commande ET les platCommande
                ViewModel.EnregistrerCommande();

                MessageBox.Show("Commande enregistrée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Création d'un nouveau ViewModel pour la page de consultation
                ConsulterCommandeViewModel consulterCommandeViewModel = new ConsulterCommandeViewModel();
                ConsulterCommandePage consulterCommandePage = new ConsulterCommandePage(consulterCommandeViewModel);

                NavigationService?.Navigate(consulterCommandePage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}