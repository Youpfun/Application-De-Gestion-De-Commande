using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using static System.Formats.Asn1.AsnWriter;

namespace Application_Pour_Sibilia.Views.Pages
{
    /// <summary>
    /// Logique d'interaction pour CommandeDuJour.xaml
    /// </summary>
    public partial class CommandeDuJour : INavigableView<ConsulterCommandeViewModel>

    {
        public ConsulterCommandeViewModel ViewModel { get; }
        public Magasin LeMagasin { get; set; }
        private ConsulterCommandeViewModel vm;
        public CommandeDuJour(ConsulterCommandeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            DataContext = new ConsulterCommandeViewModel();

            InitializeComponent();
            commandeRecupere.Items.Filter = RechercheMotCefNomClientRecup;
            //rechCommande.Items.Filter = RechercheMotClefClient;
        }

        private void buttonRecuperer_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie que le ViewModel est bien instancié
            if (commandeARecup.SelectedItem == null) 
            { 
                MessageBox.Show("Veuillez sélectionner une commande", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                GestionCommande clientSelectionne = (GestionCommande)commandeARecup.SelectedItem;
                try
                {
                    clientSelectionne.Retiree = true;
                    clientSelectionne.EstPayee = true; // On considère que la commande est payée lors de la récupération
                    clientSelectionne.Update();
                    var viewModel = DataContext as ConsulterCommandeViewModel;
                    viewModel?.LesCommandesDuJour.Remove(clientSelectionne);
                    viewModel?.LesCommandesRecupere.Add(clientSelectionne);

                }
                catch (Exception ex)
                {


                    MessageBox.Show("La commande n'a pas pu être récupéré.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                CollectionViewSource.GetDefaultView(commandeRecupere.ItemsSource)?.Refresh();
            }
        }

        private void textRechercheClientQuiARecup_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(commandeRecupere.ItemsSource).Refresh();
        }
        public bool RechercheMotCefNomClientRecup(object obj)
        {
            if (String.IsNullOrEmpty(textRechercheClientQuiARecup.Text))
                return true;
            GestionCommande comRecup = obj as GestionCommande;
            return (comRecup.NomClient.StartsWith(textRechercheClientQuiARecup.Text, StringComparison.OrdinalIgnoreCase)
            );
        }

        private void buttonDetailsCommande_Click(object sender, RoutedEventArgs e)
        {
            if (commandeARecup.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une commande", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //PlatCommande detailsCommande = (PlatCommande)commandeARecup.SelectedItem;
                //try
                //{
                //    detailsCommande.Retiree = true;
                //    detailsCommande.EstPayee = true; // On considère que la commande est payée lors de la récupération
                //    detailsCommande.Update();
                //    var viewModel = DataContext as ConsulterCommandeViewModel;
                //    viewModel?.LesCommandesDuJour.Remove(clientSelectionne);
                //    viewModel?.LesCommandesRecupere.Add(clientSelectionne);

                //}
                //catch (Exception ex)
                //{


                //    MessageBox.Show("La commande n'a pas pu être récupéré.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
                //CollectionViewSource.GetDefaultView(commandeRecupere.ItemsSource)?.Refresh();

            }
        }
    }
}
