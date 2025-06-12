using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
using System.Net;
using System.Windows.Controls;

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
    }
}