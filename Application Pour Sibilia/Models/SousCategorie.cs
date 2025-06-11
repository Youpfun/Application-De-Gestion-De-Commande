using Npgsql;
using System;
using System.Collections.Generic;

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

            // Mets ici ta propre chaîne de connexion
            string connectionString = "Host=localhost;Port=5432;Database=SAE201;Username=postgres;Password=superuser";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT numsouscategorie, nomsouscategorie, numcategorie FROM souscategorie";

                using (var cmd = new NpgsqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sousCategories.Add(new SousCategorie
                        {
                            IdSousCategorie = reader.GetInt32(0),
                            NomSousCategorie = reader.GetString(1),
                            IdCategorie = reader.GetInt32(2)
                        });
                    }
                }
            }

            return sousCategories;
        }
    }
}
