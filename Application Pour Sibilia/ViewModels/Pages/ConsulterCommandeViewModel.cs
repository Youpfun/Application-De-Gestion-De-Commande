using System.Collections.ObjectModel;
using System.Windows.Media;
using Application_Pour_Sibilia.Models;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class ConsulterCommandeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<GestionCommande> lesGestionCommandes;
        [ObservableProperty]
        private string motClefCommande;
        public ConsulterCommandeViewModel()
        {
            ChargerCommandes();
        }

        public void ChargerCommandes()
        {
            GestionCommande repo = new GestionCommande();
            var commandes = repo.FindAll();
            LesGestionCommandes = new ObservableCollection<GestionCommande>(commandes);
        }
    }
}

