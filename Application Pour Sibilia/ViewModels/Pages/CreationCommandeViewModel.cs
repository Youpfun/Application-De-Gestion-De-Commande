using Application_Pour_Sibilia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class CreationCommandeViewModel : ObservableObject
    {
        private Plat platRepo = new Plat();
        private Categorie categorieRepo = new Categorie();
        private SousCategorie sousCategorieRepo = new SousCategorie();

        private List<Plat> tousLesPlats;
        private List<SousCategorie> toutesLesSousCategories;  // important pour le cascade

        [ObservableProperty]
        private ObservableCollection<Plat> lesPlats;

        [ObservableProperty]
        private ObservableCollection<Categorie> lesCategories;

        [ObservableProperty]
        private ObservableCollection<SousCategorie> lesSousCategories;

        [ObservableProperty]
        private Categorie selectedCategorie;

        [ObservableProperty]
        private SousCategorie selectedSousCategorie;

        [ObservableProperty]
        private string motCleRecherche;

        [ObservableProperty]
        private decimal? motCleRecherchePrix;

        public CreationCommandeViewModel()
        {
            ChargerCategories();
            ChargerSousCategories();
            ChargerPlats();
        }

        private void ChargerCategories()
        {
            var categories = categorieRepo.FindAll();
            LesCategories = new ObservableCollection<Categorie>(categories);
        }

        private void ChargerSousCategories()
        {
            toutesLesSousCategories = sousCategorieRepo.FindAll();
            LesSousCategories = new ObservableCollection<SousCategorie>(toutesLesSousCategories);
        }

        private void ChargerPlats()
        {
            tousLesPlats = platRepo.FindAll().ToList();
            FiltrerPlats();
        }

        [RelayCommand]
        private void FiltrerPlats()
        {
            IEnumerable<Plat> platsFiltres = tousLesPlats;

            if (!string.IsNullOrWhiteSpace(MotCleRecherche))
                platsFiltres = platsFiltres.Where(p => p.NomPlat.Contains(MotCleRecherche, StringComparison.OrdinalIgnoreCase));

            if (SelectedCategorie != null)
                platsFiltres = platsFiltres.Where(p => p.NomCategorie == SelectedCategorie.NomCategorie);

            if (SelectedSousCategorie != null)
                platsFiltres = platsFiltres.Where(p => p.NomSousCategorie == SelectedSousCategorie.NomSousCategorie);

            if (MotCleRecherchePrix.HasValue)
            {
                platsFiltres = platsFiltres.Where(p => p.PrixUnitaire <= MotCleRecherchePrix.Value);
            }


            LesPlats = new ObservableCollection<Plat>(platsFiltres);
        }
        [RelayCommand]
        private void ReinitialiserFiltres()
        {
            MotCleRecherche = string.Empty;
            MotCleRecherchePrix = null;
            SelectedCategorie = null;
            SelectedSousCategorie = null;

            LesSousCategories = new ObservableCollection<SousCategorie>(toutesLesSousCategories);

            FiltrerPlats();

        }

        // ⚠ Le cascade dynamique : appelé quand la catégorie change
        partial void OnSelectedCategorieChanged(Categorie value)
        {
            if (value != null)
            {
                var sousCatFiltres = toutesLesSousCategories
                    .Where(sc => sc.IdCategorie == value.IdCategorie)
                    .ToList();

                LesSousCategories = new ObservableCollection<SousCategorie>(sousCatFiltres);
            }
            else
            {
                // si aucune catégorie sélectionnée : on affiche toutes les sous-catégories
                LesSousCategories = new ObservableCollection<SousCategorie>(toutesLesSousCategories);
            }

            // réinitialiser la sous-catégorie sélectionnée
            SelectedSousCategorie = null;
        }

    }
}
