using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.Views.Windows;
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

        public GestionDesPlatsPage(GestionDesPlatsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
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