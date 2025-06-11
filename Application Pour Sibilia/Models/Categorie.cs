using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Application_Pour_Sibilia.Models
{
    public class Categorie
    {
        public int IdCategorie { get; set; }
        public string NomCategorie { get; set; }


        // Méthode de récupération de toutes les catégories
        public List<Categorie> FindAll()
        {
            List<Categorie> categories = new List<Categorie>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT numcategorie, nomcategorie FROM categorie;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    categories.Add(new Categorie
                    {
                        IdCategorie = (int)dr["numcategorie"],
                        NomCategorie = (string)dr["nomcategorie"]
                    });
                }
            }
            return categories;
        }

    }
}
