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

        public List<Commande> FindAll()
        {
            List<Commande> lesCommandes = new List<Commande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from commande;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCommandes.Add(new Commande(
                        (int)dr["numcommande"], 
                        (int)dr["numclient"],
                        (int)dr["numemploye"], 
                        (DateTime)dr["datecommande"], 
                        (DateTime)dr["dateretraitprevue"], 
                        (bool)dr["payee"], 
                        (bool)dr["retiree"], 
                        Convert.ToDouble(dr["prixtotal"])));
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

        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("select * from commande where numcommande =@IdCommande;"))
            {
                cmdSelect.Parameters.AddWithValue("IdCommande", this.IdCommande);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                this.IdClient = (int)dt.Rows[0]["numclient"];
                this.IdEmploye = (int)dt.Rows[0]["numemploye"];
                this.DateCommande = (DateTime)dt.Rows[0]["datecommande"];
                this.DateRetraitPrevue = (DateTime)dt.Rows[0]["dateretraitprevue"];
                this.Payee = (bool)dt.Rows[0]["payee"];
                this.Retiree = (bool)dt.Rows[0]["retiree"];
                this.PrixTotal = (double)dt.Rows[0]["prixtotal"];


            }

        }
        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into commande (numcommande,numclient,numemploye,datecommande,dateretraitprevue,payee,retiree,prixtotal) values (@numcommande,@numclient,@numemploye,@datecommande,@dateretraitprevue,@payee,@retiree,@prixtotal) RETURNING numcommande"))
            {
                cmdInsert.Parameters.AddWithValue("numcommande", this.IdCommande);
                cmdInsert.Parameters.AddWithValue("numclient", this.IdClient);
                cmdInsert.Parameters.AddWithValue("numemploye", this.IdEmploye);
                cmdInsert.Parameters.AddWithValue("datecommande", this.DateCommande);
                cmdInsert.Parameters.AddWithValue("dateretraitprevue", this.DateRetraitPrevue);
                cmdInsert.Parameters.AddWithValue("payee", this.Payee);
                cmdInsert.Parameters.AddWithValue("retiree", this.Retiree);
                cmdInsert.Parameters.AddWithValue("prixtotal", this.PrixTotal);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.IdCommande = nb;
            return nb;
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update commande set numcommande = @numcommande ,  numclient = @numclient,  numemploye = @numemploye , datecommande = @datecommande ,  dateretraitprevue = @dateretraitprevue,  payee = @payee, retiree = @retiree, prixtotal = @prixtotal  where numcommande =@IdCommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.IdCommande);
                cmdUpdate.Parameters.AddWithValue("numclient", this.IdClient);
                cmdUpdate.Parameters.AddWithValue("numemploye", this.IdEmploye);
                cmdUpdate.Parameters.AddWithValue("datecommande", this.DateCommande);
                cmdUpdate.Parameters.AddWithValue("dateretraitprevue", this.DateRetraitPrevue);
                cmdUpdate.Parameters.AddWithValue("payee", this.Payee);
                cmdUpdate.Parameters.AddWithValue("retiree", this.Retiree);
                cmdUpdate.Parameters.AddWithValue("prixtotal", this.PrixTotal);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
    }
}
