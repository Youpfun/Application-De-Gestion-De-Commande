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
        private Client _clientSelectionne;


        private List<Plat> tousLesPlats;
        private List<SousCategorie> toutesLesSousCategories;
        public ObservableCollection<string> LesPeriodes { get; set; } = new ObservableCollection<string>
        {
            "Hiver",
            "Printemps",
            "Été",
            "Automne",
            "Toute L Année",
            "Période De Fêtes"
        };


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

        [ObservableProperty]
        private string motCleRecherchePeriode;

        [ObservableProperty]
        private int quantiteSelectionnee = 1;

        [ObservableProperty]
        private Plat platSelectionne;

        private DateTime? dateRetraitPrevue;

        [ObservableProperty]
        private ObservableCollection<LigneCommande> lignesCommande = new ObservableCollection<LigneCommande>();

        public DateTime? DateRetraitPrevue
        {
            get 
            { 
                return dateRetraitPrevue; 
            }
            set
            {
                dateRetraitPrevue = value;
                OnPropertyChanged(nameof(DateRetraitPrevue));
            }
        }
        public decimal TotalTTC
        {
            get
            {
                return LignesCommande.Sum(lc => lc.PrixTTC);
            }
        }

        public Client ClientSelectionne
        {
            get { return _clientSelectionne; }
            set
            {
                _clientSelectionne = value;
                OnPropertyChanged(nameof(ClientSelectionne));
            }
        }

        public CreationCommandeViewModel()
        {
            lignesCommande = new ObservableCollection<LigneCommande>();

            lignesCommande.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalTTC));

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

            // Vérifie si une catégorie a été sélectionnée
            if (SelectedCategorie != null)
            {
                // Récupère la liste de tous les IDs de sous-catégories qui appartiennent à la catégorie sélectionnée
                var sousCategoriesDeLaCategorie = toutesLesSousCategories
                    .Where(sc => sc.IdCategorie == SelectedCategorie.IdCategorie) // filtre uniquement les sous-catégories de cette catégorie
                    .Select(sc => sc.IdSousCategorie) // extrait uniquement les IDs des sous-catégories
                    .ToList(); // transforme le résultat en liste pour pouvoir l'utiliser plus tard dans le filtre

                // Filtre les plats : on ne garde que les plats dont la sous-catégorie appartient à la liste récupérée
                platsFiltres = platsFiltres.Where(p => sousCategoriesDeLaCategorie.Contains(p.NumSousCategorie));
            }

            // Vérifie si une sous-catégorie a été sélectionnée
            if (SelectedSousCategorie != null)
            {
                // Filtre les plats : on ne garde que ceux qui correspondent à la sous-catégorie sélectionnée
                platsFiltres = platsFiltres.Where(p => p.NumSousCategorie == SelectedSousCategorie.IdSousCategorie);
            }


            if (MotCleRecherchePrix.HasValue)
            {
                platsFiltres = platsFiltres.Where(p => p.PrixUnitaire <= MotCleRecherchePrix.Value);
            }

            if (!string.IsNullOrWhiteSpace(MotCleRecherchePeriode))
            {
                var periodeNormRecherche = MotCleRecherchePeriode.Trim().ToLower();
                platsFiltres = platsFiltres.Where(p =>
                    !string.IsNullOrWhiteSpace(p.NomPeriode) &&
                    p.NomPeriode.Trim().ToLower() == periodeNormRecherche
                );
            }



            LesPlats = new ObservableCollection<Plat>(platsFiltres);
        }
        [RelayCommand]
        private void ReinitialiserFiltres()
        {
            MotCleRecherche = string.Empty;
            MotCleRecherchePrix = null;
            MotCleRecherchePeriode = string.Empty;
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

        [RelayCommand]
        private void AjouterPlat()
        {
            if (PlatSelectionne != null && QuantiteSelectionnee > 0)
            {
                var existant = LignesCommande.FirstOrDefault(l => l.Plat.NumPlat == PlatSelectionne.NumPlat);
                if (existant != null)
                {
                    existant.Quantite += QuantiteSelectionnee;
                }
                else
                {
                    LignesCommande.Add(new LigneCommande { Plat = PlatSelectionne, Quantite = QuantiteSelectionnee });
                }

                OnPropertyChanged(nameof(TotalTTC));
            }
        }

    }
}
