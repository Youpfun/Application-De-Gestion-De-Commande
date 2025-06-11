using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Net;
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
            DataContext = LeMagasin;
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
                        CollectionViewSource.GetDefaultView(reClient.ItemsSource).Refresh();
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
            if (reClient.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un Client", "Attention",MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Client clientSelectionne = (Client)reClient.SelectedItem;
                //MessageBox.Show("", "" + reClient.SelectedIndex);
                Client copie = new Client(clientSelectionne.IdClient, clientSelectionne.NomClient,
                clientSelectionne.PrenomClient, clientSelectionne.TelClient, clientSelectionne.AdresseRueClient
                , clientSelectionne.AdresseCPClient, clientSelectionne.AdresseVilleClient);
                WindowClient wClient = new WindowClient(copie, typeAction.Modifier);
                bool? result = wClient.ShowDialog();
                Console.WriteLine(result);
                //reClient.SelectedIndex = -1;

                if (result == true)
                {
                    try
                    {
                        Console.WriteLine(clientSelectionne.IdClient);
                        clientSelectionne.NomClient = copie.NomClient;
                        clientSelectionne.PrenomClient = copie.PrenomClient;
                        clientSelectionne.TelClient = copie.TelClient;
                        clientSelectionne.AdresseRueClient = copie.AdresseRueClient;
                        clientSelectionne.AdresseCPClient = copie.AdresseCPClient;
                        clientSelectionne.AdresseVilleClient = copie.AdresseVilleClient;
                        clientSelectionne.Update();
                       
                    }
                    catch (Exception ex)
                    {
                        

                        MessageBox.Show("Le Client n'a pas pu être modifié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                CollectionViewSource.GetDefaultView(reClient.ItemsSource)?.Refresh();

            }

        }

        private void buttonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (reClient.SelectedItem != null)
            {
                Client clientChoisit = (Client)reClient.SelectedItem;
                MessageBoxResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le Client '{clientChoisit.NomClient +" "+ clientChoisit.PrenomClient}' ?",
                                                        "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Ici vous devrez implémenter la méthode Delete() dans la classe Plat
                        clientChoisit.Delete();
                        LeMagasin.LesClients.Remove(clientChoisit);
                        MessageBox.Show("Le Client a été supprimé avec succès.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Le Client n'a pas pu être supprimé.\nErreur: {ex.Message}", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un Client à supprimer.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
