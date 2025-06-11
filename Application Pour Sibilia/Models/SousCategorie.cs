using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace Application_Pour_Sibilia.Models
{
    public class SousCategorie
    {
        public int IdSousCategorie { get; set; }
        public string NomSousCategorie { get; set; }
        public int IdCategorie { get; set; }

        // Méthode de récupération de toutes les sous-catégories
        public List<SousCategorie> FindAll()
        {
            List<SousCategorie> sousCategories = new List<SousCategorie>();

            // Création de la commande SQL
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT numsouscategorie, nomsouscategorie, numcategorie FROM souscategorie;"))
            {
                // Exécution de la commande via la classe DataAccess centralisée
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);

                // Parcours des lignes de résultat
                foreach (DataRow dr in dt.Rows)
                {
                    sousCategories.Add(new SousCategorie
                    {
                        IdSousCategorie = (int)dr["numsouscategorie"],
                        NomSousCategorie = (string)dr["nomsouscategorie"],
                        IdCategorie = (int)dr["numcategorie"]
                    });
                }
            }

            return sousCategories;
        }

    }
}
