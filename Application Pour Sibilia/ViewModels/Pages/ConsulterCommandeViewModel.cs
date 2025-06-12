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
        private ObservableCollection<GestionCommande> lesCommandesDuJour;
        [ObservableProperty]
        private ObservableCollection<GestionCommande> lesCommandesRecupere;
        [ObservableProperty]
        private string motClefCommande;
        public ConsulterCommandeViewModel()
        {
            ChargerCommandes();
            ChargerCommandeDuJour();
            ChargerCommandeRecupere();
        }

        public void ChargerCommandes()
        {
            GestionCommande repo = new GestionCommande();
            var commandes = repo.FindAll();
            LesGestionCommandes = new ObservableCollection<GestionCommande>(commandes);
        }
        public void ChargerCommandeDuJour() 
        {
            GestionCommande duJour = new GestionCommande();
            var commandes = duJour.FindAllCommandeAujourdhui();
            LesCommandesDuJour = new ObservableCollection<GestionCommande>(commandes);

        }
        public void ChargerCommandeRecupere()
        {
            GestionCommande recup = new GestionCommande();
            var commandes = recup.FindAllCommandeRecupere();
            LesCommandesRecupere = new ObservableCollection<GestionCommande>(commandes);

        }
        


    }
}

