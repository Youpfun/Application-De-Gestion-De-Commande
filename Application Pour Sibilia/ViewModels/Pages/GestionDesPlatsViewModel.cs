using Application_Pour_Sibilia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class GestionDesPlatsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Plat> lesPlats;

        [ObservableProperty]
        private ObservableCollection<PlatCommande> lesDetailsPlats;

        [ObservableProperty]
        private ObservableCollection<Plat> lesPlatsFiltrés;

        [ObservableProperty]
        private string motClefPlat;

        [ObservableProperty]
        private ObservableCollection<Categorie> lesCategories;

        [ObservableProperty]
        private ObservableCollection<SousCategorie> lesSousCategories;

        [ObservableProperty]
        private ObservableCollection<SousCategorie> lesSousCategoriesFiltrées;

        [ObservableProperty]
        private Categorie categorieSelectionnee;

        [ObservableProperty]
        private SousCategorie sousCategorieSelectionnee;

        [ObservableProperty]
        private string periodeSelectionnee;

        public ObservableCollection<string> LesPeriodes { get; set; } = new ObservableCollection<string>
        {
            "Hiver",
            "Printemps",
            "Été",
            "Automne",
            "Toute L Année",
            "Période De Fêtes"
        };

        /// <summary>
        /// Initialise le ViewModel de gestion des plats.
        /// </summary>
        public GestionDesPlatsViewModel()
        {
            ChargerDonnees();
            ChargerClients();
        }

        /// <summary>
        /// Charge les données de base (plats, catégories, sous-catégories).
        /// </summary>
        private void ChargerDonnees()
        {
            // Charger les plats
            Plat repo = new Plat();
            var plats = repo.FindAll();
            LesPlats = new ObservableCollection<Plat>(plats);
            LesPlatsFiltrés = new ObservableCollection<Plat>(plats);

            // Charger les catégories
            Categorie repoCategorie = new Categorie();
            var categories = repoCategorie.FindAll();
            LesCategories = new ObservableCollection<Categorie>(categories);

            // Charger les sous-catégories
            SousCategorie repoSousCategorie = new SousCategorie();
            var sousCategories = repoSousCategorie.FindAll();
            LesSousCategories = new ObservableCollection<SousCategorie>(sousCategories);
            LesSousCategoriesFiltrées = new ObservableCollection<SousCategorie>(sousCategories);
        }

        /// <summary>
        /// Met à jour la liste des sous-catégories filtrées selon la catégorie sélectionnée.
        /// </summary>
        partial void OnCategorieSelectionneeChanged(Categorie value)
        {
            // Filtrer les sous-catégories selon la catégorie sélectionnée
            if (value != null)
            {
                var sousCategoriesFiltrées = LesSousCategories.Where(sc => sc.IdCategorie == value.IdCategorie).ToList();
                LesSousCategoriesFiltrées = new ObservableCollection<SousCategorie>(sousCategoriesFiltrées);
            }
            else
            {
                LesSousCategoriesFiltrées = new ObservableCollection<SousCategorie>(LesSousCategories);
            }

            // Réinitialiser la sous-catégorie sélectionnée
            SousCategorieSelectionnee = null;

            // Appliquer les filtres
            AppliquerFiltres();
        }

        partial void OnSousCategorieSelectionneeChanged(SousCategorie value)
        {
            AppliquerFiltres();
        }

        partial void OnPeriodeSelectionneeChanged(string value)
        {
            AppliquerFiltres();
        }

        partial void OnMotClefPlatChanged(string value)
        {
            AppliquerFiltres();
        }

        /// <summary>
        /// Applique les filtres sur la liste des plats.
        /// </summary>
        private void AppliquerFiltres()
        {
            var platsFiltrés = LesPlats.AsEnumerable();

            // Filtre par mot-clé
            if (!string.IsNullOrEmpty(MotClefPlat))
            {
                platsFiltrés = platsFiltrés.Where(p =>
                    p.NomPlat.Contains(MotClefPlat, System.StringComparison.OrdinalIgnoreCase) ||
                    p.PrixUnitaire.ToString().Contains(MotClefPlat) ||
                    p.DelaiPreparation.ToString().Contains(MotClefPlat) ||
                    p.NbPersonnes.ToString().Contains(MotClefPlat));
            }

            // Filtre par catégorie
            if (CategorieSelectionnee != null)
            {
                platsFiltrés = platsFiltrés.Where(p => p.NomCategorie == CategorieSelectionnee.NomCategorie);
            }

            // Filtre par sous-catégorie
            if (SousCategorieSelectionnee != null)
            {
                platsFiltrés = platsFiltrés.Where(p => p.NomSousCategorie == SousCategorieSelectionnee.NomSousCategorie);
            }

            // Filtre par période
            if (!string.IsNullOrEmpty(PeriodeSelectionnee))
            {
                platsFiltrés = platsFiltrés.Where(p => p.NomPeriode == PeriodeSelectionnee);
            }

            LesPlatsFiltrés = new ObservableCollection<Plat>(platsFiltrés);
        }

        /// <summary>
        /// Réinitialise tous les filtres.
        /// </summary>
        [RelayCommand]
        private void ReinitialiserFiltres()
        {
            CategorieSelectionnee = null;
            SousCategorieSelectionnee = null;
            PeriodeSelectionnee = null;
            MotClefPlat = string.Empty;
            LesSousCategoriesFiltrées = new ObservableCollection<SousCategorie>(LesSousCategories);
            LesPlatsFiltrés = new ObservableCollection<Plat>(LesPlats);
        }

        /// <summary>
        /// Ajoute un nouveau plat à la liste.
        /// </summary>
        public void AjouterPlat(Plat nouveauPlat)
        {
            LesPlats.Add(nouveauPlat);
            AppliquerFiltres();
        }

        /// <summary>
        /// Charge les détails des plats pour l'affichage.
        /// </summary>
        private void ChargerClients()
        {
            PlatCommande repo = new PlatCommande();
            var details = repo.FindAll();
            LesDetailsPlats = new ObservableCollection<PlatCommande>(details);
        }
    }
}