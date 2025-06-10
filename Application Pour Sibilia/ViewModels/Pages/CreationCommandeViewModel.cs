using Application_Pour_Sibilia.Models;
using System.Collections.ObjectModel;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class CreationCommandeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Commande> lesCommandes;
        [ObservableProperty]
        private string motClefCommande;
        public CreationCommandeViewModel()
        {
            ChargerCommandes();
        }

        private void ChargerCommandes()
        {
            Commande repo = new Commande();
            var commandes = repo.FindAll();
            LesCommandes = new ObservableCollection<Commande>(commandes);
        }
    }
}
