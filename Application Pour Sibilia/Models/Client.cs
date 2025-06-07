using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Pour_Sibilia.Models
{
    public class Client
    {
        private string nomClient;
        private string prenomClient;
        private string telClient;
        private string adresseRueClient;
        private string adresseCPClient;
        private string adresseVilleClient;
        public Client()
        {
        }

        public Client( string nomClient, string prenomClient, string telClient, string adresseRueClient, string adresseCPClient, string adresseVilleClient)
        {
            
            this.NomClient = nomClient;
            this.PrenomClient = prenomClient;
            this.TelClient = telClient;
            this.AdresseRueClient = adresseRueClient;
            this.AdresseCPClient = adresseCPClient;
            this.AdresseVilleClient = adresseVilleClient;
        }

        public string NomClient
        {
            get
            {
                return this.nomClient;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le nom doit être renseigné"); }
                if (value.Length > 50) { throw new ArgumentException("doit avoir moins de 50 caractères"); }
                this.nomClient = value;
            }
        }

        public string PrenomClient
        {
            get
            {
                return this.prenomClient;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le nom doit être renseigné"); }
                if (value.Length > 50) { throw new ArgumentException("doit avoir moins de 50 caractères"); }
                this.prenomClient = value;
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
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le nom doit être renseigné"); }
                if (value.Length > 10) { throw new ArgumentException("doit avoir moins de 50 caractères"); }
                this.telClient = value;
            }
        }

        public string AdresseRueClient
        {
            get
            {
                return this.adresseRueClient;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le nom doit être renseigné"); }
                if (value.Length > 100) { throw new ArgumentException("doit avoir moins de 50 caractères"); }
                this.adresseRueClient = value;
            }
        }

        public string AdresseCPClient
        {
            get
            {
                return this.adresseCPClient;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le nom doit être renseigné"); }
                if (value.Length > 5) { throw new ArgumentException("doit avoir moins de 50 caractères"); }
                this.adresseCPClient = value;
            }
        }

        public string AdresseVilleClient
        {
            get
            {
                return this.adresseVilleClient;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le nom doit être renseigné"); }
                if (value.Length > 50) { throw new ArgumentException("doit avoir moins de 50 caractères"); }
                this.adresseVilleClient = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public List<Client> FindAll()
        {
            List<Client> lesClients = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from client;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesClients.Add(new Client((String)dr["nomclient"], (String)dr["prenomclient"],
                   (String)dr["tel"], (String)dr["adresserue"], (String)dr["adressecp"], (String)dr["adresseville"]));
            }
            return lesClients;
        }


    }
}
