using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    /// <summary>
    /// Logique d'interaction pour ToutesLesCommandes.xaml
    /// </summary>
    public partial class ToutesLesCommandes : INavigableView<ConsulterCommandeViewModel>

    {
        public ConsulterCommandeViewModel ViewModel { get; }
        public Magasin LeMagasin { get; set; }

        /// <summary>
        /// Initialise la page affichant toutes les commandes.
        /// </summary>
        public ToutesLesCommandes(ConsulterCommandeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            DataContext = new ConsulterCommandeViewModel();
            InitializeComponent();
            rechCommande.Items.Filter = RechercheMotClefClient;
        }

        /// <summary>
        /// Filtre les commandes selon le nom du client.
        /// </summary>
        public bool RechercheMotClefClient(object obj)
        {
            if (String.IsNullOrEmpty(textGestionToutesCommandes.Text))
                return true;
            Models.GestionCommande uneGestionCommande = (GestionCommande)obj;
            return uneGestionCommande.NomClient.StartsWith(textGestionToutesCommandes.Text, StringComparison.OrdinalIgnoreCase);
        } 
        
        /// <summary>
        /// Rafraîchit la liste lors de la saisie dans la zone de recherche.
        /// </summary>
        private void textGestionToutesCommandes_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(rechCommande.ItemsSource).Refresh();
        }

        /// <summary>
        /// Gère le clic sur le bouton pour afficher les détails d'une commande.
        /// </summary>
        private void buttonDetailsCommande_Click(object sender, RoutedEventArgs e)
        {
            if (rechCommande.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une commande", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    GestionCommande detailsCommande = (GestionCommande)rechCommande.SelectedItem;
                    int numCommande = detailsCommande.NumCommande;
                    var fenetreDetails = new WindowDetailsCommande(numCommande);
                    fenetreDetails.ShowDialog();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("La commande n'a pas pu être récupéré.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                CollectionViewSource.GetDefaultView(rechCommande.ItemsSource)?.Refresh();

            }

        }
    }
}
