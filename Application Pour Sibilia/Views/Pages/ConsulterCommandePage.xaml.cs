using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
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

namespace Application_Pour_Sibilia.Views.Pages
{
    /// <summary>
    /// Logique d'interaction pour ConsulterCommandePage.xaml
    /// </summary>
    public partial class ConsulterCommandePage : INavigableView<ConsulterCommandeViewModel>

    {
        public ConsulterCommandeViewModel ViewModel { get; }
        public Magasin LeMagasin { get; set; }
        public ConsulterCommandePage(ConsulterCommandeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            DataContext = new ConsulterCommandeViewModel();
            InitializeComponent();
            rechCommande.Items.Filter = RechercheMotClefClient;
        }
        public bool RechercheMotClefClient(object obj)
        {
            if (String.IsNullOrEmpty(textGestionCommande.Text))
                return true;
            Models.GestionCommande uneGestionCommande = (GestionCommande)obj;
            return uneGestionCommande.NomClient.StartsWith(textGestionCommande.Text, StringComparison.OrdinalIgnoreCase);
        }
        private void textGestionCommande_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(rechCommande.ItemsSource).Refresh();
        }
    }
}
