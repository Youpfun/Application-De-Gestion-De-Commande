﻿using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly SessionService _sessionService;

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
        private LigneCommande ligneCommandeSelectionnee;


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
        
        public int NumeroEmployeConnecte
        {
            get { return _sessionService.NumEmploye; }
        }

        /// <summary>
        /// Initialise le ViewModel de création de commande.
        /// </summary>
        public CreationCommandeViewModel()
        {
            // Récupération du service de session pour accéder au numéro d'employé
            _sessionService = App.Services.GetRequiredService<SessionService>();
            
            lignesCommande = new ObservableCollection<LigneCommande>();
            lignesCommande.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalTTC));

            ChargerCategories();
            ChargerSousCategories();
            ChargerPlats();
        }

        /// <summary>
        /// Charge la liste des catégories disponibles.
        /// </summary>
        private void ChargerCategories()
        {
            var categories = categorieRepo.FindAll();
            LesCategories = new ObservableCollection<Categorie>(categories);
        }

        /// <summary>
        /// Charge la liste des sous-catégories disponibles.
        /// </summary>
        private void ChargerSousCategories()
        {
            toutesLesSousCategories = sousCategorieRepo.FindAll();
            LesSousCategories = new ObservableCollection<SousCategorie>(toutesLesSousCategories);
        }

        /// <summary>
        /// Charge la liste des plats disponibles.
        /// </summary>
        private void ChargerPlats()
        {
            tousLesPlats = platRepo.FindAll().ToList();
            FiltrerPlats();
        }

        /// <summary>
        /// Filtre la liste des plats selon les critères sélectionnés.
        /// </summary>
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
        
        /// <summary>
        /// Réinitialise tous les filtres de recherche.
        /// </summary>
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

        /// <summary>
        /// Met à jour la liste des sous-catégories selon la catégorie sélectionnée.
        /// </summary>
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

        /// <summary>
        /// Ajoute un plat à la commande en cours.
        /// </summary>
        [RelayCommand]
        private void AjouterPlat()
        {
            if (PlatSelectionne != null && QuantiteSelectionnee > 0)
            {
                // Blocage de la quantité maximum à 100
                if (QuantiteSelectionnee > 100)
                {
                    QuantiteSelectionnee = 100;
                }

                var existant = LignesCommande.FirstOrDefault(l => l.Plat.NumPlat == PlatSelectionne.NumPlat);
                if (existant != null)
                {
                    int nouvelleQuantite = existant.Quantite + QuantiteSelectionnee;

                    if (nouvelleQuantite > 100)
                        existant.Quantite = 100;
                    else
                        existant.Quantite = nouvelleQuantite;
                }
                else
                {
                    LignesCommande.Add(new LigneCommande { Plat = PlatSelectionne, Quantite = QuantiteSelectionnee });
                }

                OnPropertyChanged(nameof(TotalTTC));
            }
        }

        /// <summary>
        /// Enregistre la commande et ses lignes dans la base de données.
        /// </summary>
        public void EnregistrerCommande()
        {
            // Vérifications de base avant insertion
            if (ClientSelectionne == null)
            {
                throw new InvalidOperationException("Aucun client sélectionné !");
            }
            if (DateRetraitPrevue == null)
            {
                throw new InvalidOperationException("Aucune date de retrait prévue !");
            }
            if (LignesCommande.Count == 0)
            {
                throw new InvalidOperationException("Aucun plat dans la commande !");
            }

            // 1️⃣ On crée d’abord la commande principale
            Commande commande = new Commande
            {
                IdClient = ClientSelectionne.IdClient,
                IdEmploye = NumeroEmployeConnecte,
                DateCommande = DateTime.Now,
                DateRetraitPrevue = DateRetraitPrevue.Value,
                Payee = false,
                Retiree = false,
                PrixTotal = TotalTTC
            };

            int numCommande = commande.Create();

            // 2️⃣ Puis on enregistre chaque ligne de platCommande
            foreach (var ligne in LignesCommande)
            {
                PlatCommande pc = new PlatCommande(numCommande, ligne.Plat.NumPlat, ligne.Quantite, ligne.Plat.PrixUnitaire);
                pc.Create();
            }
        }

        /// <summary>
        /// Supprime la ligne de commande sélectionnée.
        /// </summary>
        [RelayCommand]
        private void SupprimerLigneCommande()
        {
            if (LigneCommandeSelectionnee != null)
            {
                LignesCommande.Remove(LigneCommandeSelectionnee);
                OnPropertyChanged(nameof(TotalTTC));  // Pour recalculer le total
            }
            else
            {
                System.Windows.MessageBox.Show("Aucun plat sélectionné à supprimer.");
            }
        }
    }
}
