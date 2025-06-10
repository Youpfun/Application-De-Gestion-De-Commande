using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class GestionClientPage : INavigableView<GestionClientViewModel>
    {
        public GestionClientViewModel ViewModel { get; }
        public Magasin LeMagasin { get; set; }

        private GestionClientViewModel vm;

        public GestionClientPage(GestionClientViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            DataContext = new GestionClientViewModel();

          
            InitializeComponent();
            reClient.Items.Filter = RechercheMotClefClient;
            LeMagasin = new Magasin();
        }
        public bool RechercheMotClefClient(object obj)
        {
            if (String.IsNullOrEmpty(textMotClefClient.Text))
                return true;
            Client unClient = obj as Client;
            return (unClient.NomClient.StartsWith(textMotClefClient.Text, StringComparison.OrdinalIgnoreCase)
            || unClient.TelClient.StartsWith(textMotClefClient.Text, StringComparison.OrdinalIgnoreCase) 
            || unClient.AdresseCPClient.StartsWith(textMotClefClient.Text, StringComparison.OrdinalIgnoreCase) 
            || unClient.AdresseVilleClient.StartsWith(textMotClefClient.Text, StringComparison.OrdinalIgnoreCase)
            || unClient.AdresseRueClient.StartsWith(textMotClefClient.Text, StringComparison.OrdinalIgnoreCase));
        }
        private void textMotClefClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(reClient.ItemsSource).Refresh();
        }

        private void buttonNouveau_Click(object sender, RoutedEventArgs e)
        {
            Client unClient = new Client();
            Windows.WindowClient wClient = new Windows.WindowClient(unClient, typeAction.Créer);
            bool? result = wClient.ShowDialog();
            if (result == true)
            {
                {
                    try
                    {
                        unClient.IdClient = unClient.Create();
                        LeMagasin.LesClients.Add(unClient);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le Client n'a pas pu être crée.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void buttonModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
