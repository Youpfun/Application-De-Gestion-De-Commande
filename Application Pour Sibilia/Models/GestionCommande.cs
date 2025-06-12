using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Pour_Sibilia.Models
{
    public class GestionCommande
    {

        private int numCommande;
        private string nomClient;
        private string telClient;
        private DateTime dateRetraitPrevue;
        private string nomVendeur;
        private double prixTotal;
        private bool estPayee;
        public GestionCommande()
        { }

        public GestionCommande(int numCommande, string telClient, DateTime dateRetraitPrevue, double prixTotal)
        {
            this.NumCommande = numCommande;
            this.TelClient = telClient;
            this.DateRetraitPrevue = dateRetraitPrevue;
            this.PrixTotal = prixTotal;
        }

        public GestionCommande(int numCommande, string nomClient, string telClient, DateTime dateRetraitPrevue, string nomVendeur, double prixTotal, bool estPayee)
        {
            this.NumCommande = numCommande;
            this.NomClient = nomClient;
            this.TelClient = telClient;
            this.DateRetraitPrevue = dateRetraitPrevue;
            this.NomVendeur = nomVendeur;
            this.PrixTotal = prixTotal;
            this.EstPayee = estPayee;
        }

        public int NumCommande
        {
            get
            {
                return this.numCommande;
            }

            set
            {
                this.numCommande = value;
            }
        }

        public string NomClient
        {
            get
            {
                return this.nomClient;
            }

            set
            {
                this.nomClient = value;
            }
        }

        public string TelClient
        {
            get
            {
                return this.telClient;
            }

            set
            {
                this.telClient = value;
            }
        }

        public DateTime DateRetraitPrevue
        {
            get
            {
                return this.dateRetraitPrevue;
            }

            set
            {
                this.dateRetraitPrevue = value;
            }
        }

        public string NomVendeur
        {
            get
            {
                return this.nomVendeur;
            }

            set
            {
                this.nomVendeur = value;
            }
        }

        public double PrixTotal
        {
            get
            {
                return this.prixTotal;
            }

            set
            {
                this.prixTotal = value;
            }
        }

        public bool EstPayee
        {
            get
            {
                return this.estPayee;
            }

            set
            {
                this.estPayee = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is GestionCommande commande &&
                   this.NumCommande == commande.NumCommande &&
                   this.NomClient == commande.NomClient &&
                   this.TelClient == commande.TelClient &&
                   this.DateRetraitPrevue == commande.DateRetraitPrevue &&
                   this.NomVendeur == commande.NomVendeur &&
                   this.PrixTotal == commande.PrixTotal &&
                   this.EstPayee == commande.EstPayee;
        }

        public List<GestionCommande> FindAll()
        {
            List<GestionCommande> lesGestionCommandes = new List<GestionCommande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select c.numcommande, CONCAT(cl.nomclient, ' ', cl.prenomclient) AS nomClient,cl.tel,c.DATERETRAITPREVUE, CONCAT(e.nomemploye, ' ', e.prenomemploye) AS Vendeur, c.prixtotal, c.payee from commande c join client cl on c.numclient = cl.numclient join employe e on c.numemploye=e.numemploye order by DATERETRAITPREVUE desc"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesGestionCommandes.Add(new GestionCommande((int)dr["numcommande"], (String)dr["nomClient"], (String)dr["tel"], (DateTime)dr["DATERETRAITPREVUE"],
                   (String)dr["Vendeur"], (double)(decimal)dr["prixtotal"], (bool)dr["payee"]));
            }
            return lesGestionCommandes;
        }
        public List<GestionCommande> FindAllCommandeAujourdhui()
        {
            List<GestionCommande> lesCommandesDuJour = new List<GestionCommande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select c.numcommande, CONCAT(cl.nomclient, ' ', cl.prenomclient) AS nomClient,cl.tel,c.DATERETRAITPREVUE, CONCAT(e.nomemploye, ' ', e.prenomemploye) AS Vendeur, c.prixtotal, c.payee from commande c join client cl on c.numclient = cl.numclient join employe e on c.numemploye=e.numemploye where DATERETRAITPREVUE = Current_date order by payee desc"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCommandesDuJour.Add(new GestionCommande((int)dr["numcommande"], (String)dr["nomClient"], (String)dr["tel"], (DateTime)dr["DATERETRAITPREVUE"],
                   (String)dr["Vendeur"], (double)(decimal)dr["prixtotal"], (bool)dr["payee"]));
            }
            return lesCommandesDuJour;
        }

    }
}

    