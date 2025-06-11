using Npgsql;
using System;
using System.Collections.Generic;

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

            // Mets ici ta propre chaîne de connexion
            string connectionString = "Host=localhost;Port=5432;Database=SAE201;Username=postgres;Password=superuser";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT numcategorie, nomcategorie FROM categorie";

                using (var cmd = new NpgsqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Categorie
                        {
                            IdCategorie = reader.GetInt32(0),   
                            NomCategorie = reader.GetString(1)
                        });
                    }
                }
            }

            return categories;
        }
    }
}
