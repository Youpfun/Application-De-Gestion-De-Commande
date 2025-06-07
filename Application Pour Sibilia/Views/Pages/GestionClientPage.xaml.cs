using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.ViewModels.Pages;
using System;
using System.Collections.ObjectModel;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class GestionClientPage : INavigableView<GestionClientViewModel>
    {
        public GestionClientViewModel ViewModel { get; }
        public Magasin LeMagasin { get; set; }
        public GestionClientPage(GestionClientViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            ChargeData();
            InitializeComponent();

        }
        public void ChargeData()
        {
            try
            {
                LeMagasin = new Magasin("Magasin 1");
                this.DataContext = LeMagasin;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données veuillez consulter votre admin");
               
                Application.Current.Shutdown();
            }
        }

    }
}
