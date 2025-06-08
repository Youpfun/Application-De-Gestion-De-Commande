using Application_Pour_Sibilia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class GestionDesPlatsViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<Plat> plats = new()
        {
            new Plat { Nom = "Tranche de jambon", Categorie = "Entrée", SousCategorie = "Jambon", Disponibilite = "Toute saison" },
            new Plat { Nom = "Escargot", Categorie = "Plats", SousCategorie = "la boeme", Disponibilite = "hiver" },
            new Plat { Nom = "tech paff", Categorie = "dessert", SousCategorie = "st maritin", Disponibilite = "ete" }
        };

        // Commandes pour les actions
        [RelayCommand]
        private void ModifierPlat(Plat plat)
        {
            // Action pour modifier un plat
            System.Diagnostics.Debug.WriteLine($"Modifier : {plat.Nom}");
        }

        [RelayCommand]
        private void SupprimerPlat(Plat plat)
        {
            Plats.Remove(plat);
        }
    }
}
