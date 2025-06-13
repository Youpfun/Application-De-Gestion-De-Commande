using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace Application_Pour_Sibilia.Models
{
    public class PlatCommande
    {
        public int NumCommande { get; set; }

        private string nomPlat;
        public int NumPlat { get; set; }
        public int Quantite { get; set; }
        public decimal Prix { get; set; }
        public PlatCommande() { }
        public PlatCommande(int numCommande, string nomPlat, int quantite, decimal prix)
        {
            this.NumCommande = numCommande;
            this.NomPlat = nomPlat;
            this.Quantite = quantite;
            this.Prix = prix;

        }

        public PlatCommande(int numCommande, int numPlat, int quantite, decimal prix)
        {
            this.NumCommande = numCommande;
            this.NumPlat = numPlat;
            this.Quantite = quantite;
            this.Prix = prix;
            
        }
        

        public PlatCommande(int numCommande, int numPlat, int quantite, decimal prix, string nomPlat) : this(numCommande, numPlat, quantite, prix)
        {
            this.NomPlat = nomPlat;
        }

        public string NomPlat
        {
            get
            {
                return this.nomPlat;
            }

            set
            {
                this.nomPlat = value;
            }
        }

        

        public int Create()
        {
            using (var cmdInsert = new NpgsqlCommand(
                "INSERT INTO platcommande (numcommande, numplat, quantite, prix) " +
                "VALUES (@numcommande, @numplat, @quantite, @prix)"))
            {
                cmdInsert.Parameters.AddWithValue("numcommande", this.NumCommande);
                cmdInsert.Parameters.AddWithValue("numplat", this.NumPlat);
                cmdInsert.Parameters.AddWithValue("quantite", this.Quantite);
                cmdInsert.Parameters.AddWithValue("prix", this.Prix);

                return DataAccess.Instance.ExecuteSet(cmdInsert);
            }
        }
        
        // Méthode pour récupérer tous les plats associés à une commande
        public static List<PlatCommande> GetByNumCommande(int numCommande)
        {
            List<PlatCommande> result = new List<PlatCommande>();
            
            using (var cmdSelect = new NpgsqlCommand(
                "SELECT numcommande, numplat, quantite, prix " +
                "FROM platcommande " +
                "WHERE numcommande = @numcommande"))
            {
                cmdSelect.Parameters.AddWithValue("numcommande", numCommande);
                
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow row in dt.Rows)
                {
                    result.Add(new PlatCommande(
                        Convert.ToInt32(row["numcommande"]),
                        Convert.ToInt32(row["numplat"]),
                        Convert.ToInt32(row["quantite"]),
                        Convert.ToDecimal(row["prix"])
                    ));
                }
            }
            
            return result;
        }
        
        // Méthode pour supprimer un plat d'une commande
        public int Delete()
        {
            using (var cmdDelete = new NpgsqlCommand(
                "DELETE FROM platcommande " +
                "WHERE numcommande = @numcommande AND numplat = @numplat"))
            {
                cmdDelete.Parameters.AddWithValue("numcommande", this.NumCommande);
                cmdDelete.Parameters.AddWithValue("numplat", this.NumPlat);
                
                return DataAccess.Instance.ExecuteSet(cmdDelete);
            }
        }
        
        // Méthode pour mettre à jour la quantité et le prix d'un plat dans une commande
        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand(
                "UPDATE platcommande " +
                "SET quantite = @quantite, prix = @prix " +
                "WHERE numcommande = @numcommande AND numplat = @numplat"))
            {
                cmdUpdate.Parameters.AddWithValue("quantite", this.Quantite);
                cmdUpdate.Parameters.AddWithValue("prix", this.Prix);
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                cmdUpdate.Parameters.AddWithValue("numplat", this.NumPlat);
                
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
        public List<PlatCommande> FindAll()
        {
            List<PlatCommande> lesDetailsPlats = new List<PlatCommande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select numcommande, nomplat, quantite,prix from platcommande p join plat pl on p.numplat=pl.numplat"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesDetailsPlats.Add(new PlatCommande((int)dr["numcommande"], (String)dr["nomplat"], (int)dr["quantite"],Convert.ToDecimal(dr["prix"])));
            }
            return lesDetailsPlats;
        }
    }
}
