using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.Views.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class GestionClientViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Client> lesClients;
        [ObservableProperty]
        private string motClefClient;
        public GestionClientViewModel()
        {
            ChargerClients();
            
        }

        private void ChargerClients()
        {
            Client repo = new Client();
            var clients = repo.FindAll();
            LesClients = new ObservableCollection<Client>(clients);
        }
    }
}
