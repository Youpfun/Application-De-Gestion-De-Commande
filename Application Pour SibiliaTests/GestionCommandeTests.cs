using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application_Pour_Sibilia.Models;
using System;

namespace Application_Pour_SibiliaTests
{
    [TestClass]
    public class GestionCommandeTests
    {
        [TestMethod]
        public void Constructeur_SansParametres_InitialiseObjets()
        {
            var commande = new GestionCommande();
            Assert.IsNotNull(commande);
        }

        [TestMethod]
        public void Constructeur_ParametresBase_AssigneValeurs()
        {
            var date = DateTime.Today;
            var commande = new GestionCommande(1, "0600000000", date, 99.99);

            Assert.AreEqual(1, commande.NumCommande);
            Assert.AreEqual("0600000000", commande.TelClient);
            Assert.AreEqual(date, commande.DateRetraitPrevue);
            Assert.AreEqual(99.99, commande.PrixTotal);
        }

        [TestMethod]
        public void Constructeur_Complet_AssigneToutesValeurs()
        {
            var date = DateTime.Today;
            var commande = new GestionCommande(2, "Jean Dupont", "0600000001", date, "Vendeur Test", 120.5, true, true);

            Assert.AreEqual(2, commande.NumCommande);
            Assert.AreEqual("Jean Dupont", commande.NomClient);
            Assert.AreEqual("0600000001", commande.TelClient);
            Assert.AreEqual(date, commande.DateRetraitPrevue);
            Assert.AreEqual("Vendeur Test", commande.NomVendeur);
            Assert.AreEqual(120.5, commande.PrixTotal);
            Assert.IsTrue(commande.EstPayee);
            Assert.IsTrue(commande.Retiree);
        }

        [TestMethod]
        public void Propriete_SetGet_Fonctionne()
        {
            var commande = new GestionCommande();
            var date = DateTime.Now;

            commande.NumCommande = 10;
            commande.NomClient = "Alice";
            commande.TelClient = "0612345678";
            commande.DateRetraitPrevue = date;
            commande.NomVendeur = "Bob";
            commande.PrixTotal = 50.5;
            commande.EstPayee = true;
            commande.Retiree = false;

            Assert.AreEqual(10, commande.NumCommande);
            Assert.AreEqual("Alice", commande.NomClient);
            Assert.AreEqual("0612345678", commande.TelClient);
            Assert.AreEqual(date, commande.DateRetraitPrevue);
            Assert.AreEqual("Bob", commande.NomVendeur);
            Assert.AreEqual(50.5, commande.PrixTotal);
            Assert.IsTrue(commande.EstPayee);
            Assert.IsFalse(commande.Retiree);
        }

        [TestMethod]
        public void Equals_DeuxCommandesIdentiques_RetourneTrue()
        {
            var date = DateTime.Today;
            var c1 = new GestionCommande(1, "Jean Dupont", "0600000001", date, "Vendeur", 100, true, false);
            var c2 = new GestionCommande(1, "Jean Dupont", "0600000001", date, "Vendeur", 100, true, false);

            Assert.IsTrue(c1.Equals(c2));
        }

        [TestMethod]
        public void Equals_CommandesDifferentes_RetourneFalse()
        {
            var date = DateTime.Today;
            var c1 = new GestionCommande(1, "Jean Dupont", "0600000001", date, "Vendeur", 100, true, false);
            var c2 = new GestionCommande(2, "Alice", "0600000002", date, "Vendeur", 200, false, true);

            Assert.IsFalse(c1.Equals(c2));
        }
    }
}
