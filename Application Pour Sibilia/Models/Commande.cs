using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Pour_Sibilia.Models
{
    public class Commande
    {
        private int idCommande;
        private int idClient;
        private int idEmploye;
        private DateTime dateCommande;
        private DateTime dateRetraitPrevue;
        private bool payee;
        private bool retiree;
        private double prixTotal;

        public int IdCommande
        {
            get
            {
                return this.idCommande;
            }

            set
            {
                this.idCommande = value;
            }
        }

        public int IdClient
        {
            get
            {
                return this.idClient;
            }

            set
            {
                this.idClient = value;
            }
        }

        public int IdEmploye
        {
            get
            {
                return this.idEmploye;
            }

            set
            {
                this.idEmploye = value;
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return this.dateCommande;
            }

            set
            {
                this.dateCommande = value;
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

        public bool Payee
        {
            get
            {
                return this.payee;
            }

            set
            {
                this.payee = value;
            }
        }

        public bool Retiree
        {
            get
            {
                return this.retiree;
            }

            set
            {
                this.retiree = value;
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

        public Commande(int idCommande)
        {
            this.IdCommande = idCommande;
        }
        public Commande()
        {
        }

        public Commande(int idCommande, int idClient, int idEmploye, DateTime dateCommande, DateTime dateRetraitPrevue, bool payee, bool retiree, double prixTotal) : this(idCommande)
        {
            this.IdClient = idClient;
            this.IdEmploye = idEmploye;
            this.DateCommande = dateCommande;
            this.DateRetraitPrevue = dateRetraitPrevue;
            this.Payee = payee;
            this.Retiree = retiree;
            this.PrixTotal = prixTotal;
        }

        public List<Commande> FindAll()
        {
            List<Commande> lesCommandes = new List<Commande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from commmande;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCommandes.Add(new Commande((int)dr["idcommande"], (int)dr["idclient"],
                   (int)dr["idemploye"], (DateTime)dr["datecommande"], (DateTime)dr["dateretraitprevue"], 
                   (bool)dr["payee"], (bool)dr["retiree"], (double)dr["prixtotal"]));
            }
            return lesCommandes;
        }

        public override bool Equals(object? obj)
        {
            return obj is Commande commande &&
                   this.IdCommande == commande.IdCommande &&
                   this.IdClient == commande.IdClient &&
                   this.IdEmploye == commande.IdEmploye &&
                   this.DateCommande == commande.DateCommande &&
                   this.DateRetraitPrevue == commande.DateRetraitPrevue &&
                   this.Payee == commande.Payee &&
                   this.Retiree == commande.Retiree &&
                   this.PrixTotal == commande.PrixTotal;
        }

        //public void Read()
        //{
        //    using (var cmdSelect = new NpgsqlCommand("select * from  commande  where numcommande =@IdCommande;"))
        //    {
        //        cmdSelect.Parameters.AddWithValue("IdCommande", this.IdCommande);

        //        DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
        //        this.IdClient = (int)dt.Rows[0]["idclient"];
        //        this.IdEmploye = (int)dt.Rows[0]["idemploye"];
        //        this.DateCommande = (DateTime)dt.Rows[0]["datecommande"];
        //        this.DateRetraitPrevue = (DateTime)dt.Rows[0]["dateretraitprevue"];
        //        this.Payee = (bool)dt.Rows[0]["payee"];
        //        this.Retiree = (bool)dt.Rows[0]["retiree"];
        //        this.PrixTotal = (double)dt.Rows[0]["prixtotal"];


        //    }

        //}
        //public int Create()
        //{
        //    int nb = 0;
        //    using (var cmdInsert = new NpgsqlCommand("insert into client (nomclient,prenomclient,tel,adresserue,adressecp,adresseville) values (@nomclient,@prenomclient,@tel,@adresserue,@adressecp,@adresseville) RETURNING numclient"))
        //    {
        //        cmdInsert.Parameters.AddWithValue("nomclient", this.NomClient);
        //        cmdInsert.Parameters.AddWithValue("prenomclient", this.PrenomClient);
        //        cmdInsert.Parameters.AddWithValue("tel", this.TelClient);
        //        cmdInsert.Parameters.AddWithValue("adresserue", this.AdresseRueClient);
        //        cmdInsert.Parameters.AddWithValue("adressecp", this.AdresseCPClient);
        //        cmdInsert.Parameters.AddWithValue("adresseville", this.AdresseVilleClient);
        //        nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
        //    }
        //    this.IdClient = nb;
        //    return nb;
        //}

        //public int Update()
        //{
        //    using (var cmdUpdate = new NpgsqlCommand("update client set nomclient =@nomclient ,  prenomclient = @prenomclient,  tel = @tel , adresserue =@adresserue ,  adressecp = @adressecp,  adresseville = @adresseville  where numclient =@IdClient;"))
        //    {
        //        cmdUpdate.Parameters.AddWithValue("nomclient", this.NomClient);
        //        cmdUpdate.Parameters.AddWithValue("prenomclient", this.PrenomClient);
        //        cmdUpdate.Parameters.AddWithValue("tel", this.TelClient);
        //        cmdUpdate.Parameters.AddWithValue("adresserue", this.AdresseRueClient);
        //        cmdUpdate.Parameters.AddWithValue("adressecp", this.AdresseCPClient);
        //        cmdUpdate.Parameters.AddWithValue("adresseville", this.AdresseVilleClient);
        //        cmdUpdate.Parameters.AddWithValue("numclient", this.IdClient);
        //        return DataAccess.Instance.ExecuteSet(cmdUpdate);
        //    }
        //}
    }
}
