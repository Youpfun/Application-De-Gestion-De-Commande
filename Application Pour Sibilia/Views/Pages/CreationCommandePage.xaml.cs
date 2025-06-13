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

        // Constructeur par défaut pour la page de création de commande
        public CreationCommandePage()
        {
            InitializeComponent();
            ViewModel = new CreationCommandeViewModel();
            DataContext = ViewModel;
            ViewModel.LignesCommande.CollectionChanged += LignesCommande_CollectionChanged;
        }

        // Méthode pour initialiser la page avec un client sélectionné
        public CreationCommandePage(Client clientSelectionne)
        {
            InitializeComponent();
            ViewModel = new CreationCommandeViewModel();
            DataContext = ViewModel;

            this.clientSelectionne = clientSelectionne;

            ViewModel.ClientSelectionne = clientSelectionne;
            ViewModel.LignesCommande.CollectionChanged += LignesCommande_CollectionChanged;
        }


        private void LignesCommande_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ScrollCommande?.ScrollToEnd();
        }

        private void buttonSelectionClient_Click(object sender, RoutedEventArgs e)
        {
            GestionClientPage gestionClientPage = new GestionClientPage(new GestionClientViewModel());
            NavigationService?.Navigate(gestionClientPage);
        }

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
                // Création de la commande
                Commande nouvelleCommande = new Commande
                (
                    0, // idCommande généré automatiquement par la BDD
                    ViewModel.ClientSelectionne.IdClient,
                    ViewModel.NumeroEmployeConnecte, // Utilisation du numéro d'employé connecté
                    DateTime.Now,
                    ViewModel.DateRetraitPrevue.Value,
                    false, // Payée
                    false, // Retirée
                    ViewModel.TotalTTC // Prix total
                );

                // Sauvegarde en base
                nouvelleCommande.IdCommande = nouvelleCommande.Create();

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