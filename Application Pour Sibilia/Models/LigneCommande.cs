using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Application_Pour_Sibilia.Models
{
    public partial class LigneCommande : ObservableObject
    {
        public Plat Plat { get; set; }

        [ObservableProperty]
        private int quantite;

        public decimal PrixTTC => Quantite * Plat.PrixUnitaire;

        // Très important : on notifie que PrixTTC change lorsque Quantite change
        partial void OnQuantiteChanged(int value)
        {
            OnPropertyChanged(nameof(PrixTTC));
        }
    }
}
