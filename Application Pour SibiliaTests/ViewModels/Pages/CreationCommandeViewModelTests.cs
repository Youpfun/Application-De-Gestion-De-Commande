using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.Models;
using Application_Pour_Sibilia.Services;
using System.Collections.ObjectModel;
using System;
using Microsoft.Extensions.DependencyInjection; // Ajout de la directive using

namespace Application_Pour_SibiliaTests.ViewModels.Pages
{
    [TestClass]
    public class CreationCommandeViewModelTests
    {
        private CreationCommandeViewModel viewModel;

        private IServiceProvider services; // Ajout d'une variable pour gérer les services

        [TestInitialize]
        public void Setup()
        {
            // Mock du service de session
            services = new ServiceCollection() // Correction ici
                .AddSingleton(new SessionService { NumEmploye = 42 }) // Utilisation correcte de l'extension AddSingleton
                .BuildServiceProvider();

            viewModel = new CreationCommandeViewModel();
        }

        [TestMethod]
        public void AjouterPlat_AjouteUneLigneCommande()
        {
            var plat = new Plat { NumPlat = 1, PrixUnitaire = 10m };
            viewModel.PlatSelectionne = plat;
            viewModel.QuantiteSelectionnee = 2;

            viewModel.AjouterPlatCommand.Execute(null);

            Assert.AreEqual(1, viewModel.LignesCommande.Count);
            Assert.AreEqual(plat, viewModel.LignesCommande[0].Plat);
            Assert.AreEqual(2, viewModel.LignesCommande[0].Quantite);
        }

        [TestMethod]
        public void ReinitialiserFiltres_ReinitialiseTousLesFiltres()
        {
            viewModel.MotCleRecherche = "test";
            viewModel.MotCleRecherchePrix = 5;
            viewModel.MotCleRecherchePeriode = "Hiver";
            viewModel.SelectedCategorie = new Categorie { IdCategorie = 1 };
            viewModel.SelectedSousCategorie = new SousCategorie { IdSousCategorie = 1 };

            viewModel.ReinitialiserFiltresCommand.Execute(null);

            Assert.AreEqual(string.Empty, viewModel.MotCleRecherche);
            Assert.IsNull(viewModel.MotCleRecherchePrix);
            Assert.AreEqual(string.Empty, viewModel.MotCleRecherchePeriode);
            Assert.IsNull(viewModel.SelectedCategorie);
            Assert.IsNull(viewModel.SelectedSousCategorie);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EnregistrerCommande_SansClient_ProvoqueException()
        {
            viewModel.DateRetraitPrevue = DateTime.Now;
            viewModel.LignesCommande.Add(new LigneCommande { Plat = new Plat(), Quantite = 1 });
            viewModel.EnregistrerCommande();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EnregistrerCommande_SansDateRetrait_ProvoqueException()
        {
            viewModel.ClientSelectionne = new Client { IdClient = 1 };
            viewModel.LignesCommande.Add(new LigneCommande { Plat = new Plat(), Quantite = 1 });
            viewModel.EnregistrerCommande();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EnregistrerCommande_SansLigneCommande_ProvoqueException()
        {
            viewModel.ClientSelectionne = new Client { IdClient = 1 };
            viewModel.DateRetraitPrevue = DateTime.Now;
            viewModel.EnregistrerCommande();
        }
    }
}