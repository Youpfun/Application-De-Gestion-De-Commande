using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.Views.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class GestionDesPlatsPage : INavigableView<GestionDesPlatsViewModel>
    {
        public GestionDesPlatsViewModel ViewModel { get; }
        public Magasin LeMagasin { get; set; }
        private GestionDesPlatsViewModel vm;

        public GestionDesPlatsPage(GestionDesPlatsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            DataContext = new GestionDesPlatsViewModel();
            InitializeComponent();
            rePlat.Items.Filter = RechercheMotClefPlat;
            LeMagasin = new Magasin();
        }

        public bool RechercheMotClefPlat(object obj)
        {
            if (String.IsNullOrEmpty(textMotClefPlat.Text))
                return true;
            Plat unPlat = obj as Plat;
            return (unPlat.NomPlat.StartsWith(textMotClefPlat.Text, StringComparison.OrdinalIgnoreCase)
            || unPlat.PrixUnitaire.ToString().StartsWith(textMotClefPlat.Text, StringComparison.OrdinalIgnoreCase)
            || unPlat.DelaiPreparation.ToString().StartsWith(textMotClefPlat.Text, StringComparison.OrdinalIgnoreCase)
            || unPlat.NbPersonnes.ToString().StartsWith(textMotClefPlat.Text, StringComparison.OrdinalIgnoreCase));
        }

        private void textMotClefPlat_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(rePlat.ItemsSource).Refresh();
        }

        private void buttonNouveau_Click(object sender, RoutedEventArgs e)
        {
            //Plat unPlat = new Plat();
            //Windows.WindowPlat wPlat = new Windows.WindowPlat(unPlat, typeAction.Créer);
            //bool? result = wPlat.ShowDialog();
            //if (result == true)
            //{
            //    try
            //    {
            //        unPlat.NumPlat = unPlat.Create();
            //        LeMagasin.LesPlats.Add(unPlat);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Le Plat n'a pas pu être créé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
        }

        private void buttonModifier_Click(object sender, RoutedEventArgs e)
        {
            //if (rePlat.SelectedItem != null)
            //{
            //    Plat platSelectionne = (Plat)rePlat.SelectedItem;
            //    Plat platCopie = new Plat(platSelectionne.NumSousCategorie, platSelectionne.NumPeriode,
            //                             platSelectionne.NomPlat, platSelectionne.PrixUnitaire,
            //                             platSelectionne.DelaiPreparation, platSelectionne.NbPersonnes,
            //                             platSelectionne.NumPlat);
            //
            //    Windows.WindowPlat wPlat = new Windows.WindowPlat(platCopie, typeAction.Modifier);
            //    bool? result = wPlat.ShowDialog();
            //    if (result == true)
            //    {
            //        try
            //        {
            //            platCopie.Update();
            //            // Mettre à jour l'objet dans la collection
            //            platSelectionne.NumSousCategorie = platCopie.NumSousCategorie;
            //            platSelectionne.NumPeriode = platCopie.NumPeriode;
            //            platSelectionne.NomPlat = platCopie.NomPlat;
            //            platSelectionne.PrixUnitaire = platCopie.PrixUnitaire;
            //            platSelectionne.DelaiPreparation = platCopie.DelaiPreparation;
            //            platSelectionne.NbPersonnes = platCopie.NbPersonnes;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Le Plat n'a pas pu être modifié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Veuillez sélectionner un plat à modifier.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }
    }
}