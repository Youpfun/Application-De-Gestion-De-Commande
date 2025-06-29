﻿using Application_Pour_Sibilia.Models;
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

        /// <summary>
        /// Initialise la page de consultation des commandes.
        /// </summary>
        public ConsulterCommandePage(ConsulterCommandeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
            FrameConsulterCommande.Navigate(new CommandeDuJour(ViewModel));
        }

        /// <summary>
        /// Affiche la liste de toutes les commandes.
        /// </summary>
        private void button_Toutes_Click(object sender, RoutedEventArgs e)
        {
            FrameConsulterCommande.Navigate(new ToutesLesCommandes(ViewModel));
        }

        /// <summary>
        /// Affiche la liste des commandes du jour.
        /// </summary>
        private void button_Ajourdhui_Click(object sender, RoutedEventArgs e)
        {
            FrameConsulterCommande.Navigate(new CommandeDuJour(ViewModel));
        }
    }
}
