using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Application_Pour_Sibilia.Models
{
    public class PlatCommande
    {
        public int NumCommande { get; set; }
        public int NumPlat { get; set; }
        public int Quantite { get; set; }
        public decimal Prix { get; set; }

        public PlatCommande() { }

        public PlatCommande(int numCommande, int numPlat, int quantite, decimal prix)
        {
            this.NumCommande = numCommande;
            this.NumPlat = numPlat;
            this.Quantite = quantite;
            this.Prix = prix;
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

                return DataAccess.Instance.ExecuteSet(cmdInsert); // ExecuteSet sans retour
            }
        }

    }
}
