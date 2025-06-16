using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application_Pour_Sibilia.ViewModels.Pages;
using Application_Pour_Sibilia.Models;
using System.Collections.ObjectModel;

namespace Application_Pour_SibiliaTests.ViewModels.Pages
{
    [TestClass]
    public class GestionDesPlatsViewModelTests
    {
        [TestMethod]
        public void AjouterPlat_AjoutePlatEtFiltre()
        {
            var viewModel = new GestionDesPlatsViewModel();
            var plat = new Plat { NumPlat = 99, NomPlat = "TestPlat" };

            int countAvant = viewModel.LesPlats.Count;
            viewModel.AjouterPlat(plat);

            Assert.AreEqual(countAvant + 1, viewModel.LesPlats.Count);
            Assert.IsTrue(viewModel.LesPlats.Contains(plat));
            Assert.IsTrue(viewModel.LesPlatsFiltrés.Contains(plat));
        }

        [TestMethod]
        public void ReinitialiserFiltres_ReinitialiseTousLesFiltres()
        {
            var viewModel = new GestionDesPlatsViewModel();
            viewModel.CategorieSelectionnee = new Categorie { IdCategorie = 1, NomCategorie = "Cat" };
            viewModel.SousCategorieSelectionnee = new SousCategorie { IdSousCategorie = 1, NomSousCategorie = "SousCat" };
            viewModel.PeriodeSelectionnee = "Hiver";
            viewModel.MotClefPlat = "test";

            viewModel.ReinitialiserFiltresCommand.Execute(null);

            Assert.IsNull(viewModel.CategorieSelectionnee);
            Assert.IsNull(viewModel.SousCategorieSelectionnee);
            Assert.IsNull(viewModel.PeriodeSelectionnee);
            Assert.AreEqual(string.Empty, viewModel.MotClefPlat);
            CollectionAssert.AreEqual(viewModel.LesSousCategories, viewModel.LesSousCategoriesFiltrées);
            CollectionAssert.AreEqual(viewModel.LesPlats, viewModel.LesPlatsFiltrés);
        }
    }
}