using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Application_Pour_Sibilia.Models
{
    public class Client
    {
        private int idClient;
        private string nomClient;
        private string prenomClient;
        private string telClient;
        private string adresseRueClient;
        private string adresseCPClient;
        private string adresseVilleClient;

        public event PropertyChangedEventHandler? PropertyChanged;


        public Client(int idClient) 
        {
            this.IdClient = idClient;
        }
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

        public Client(string nomClient, string prenomClient, string telClient, string adresseRueClient, string adresseCPClient, string adresseVilleClient, int idClient) : this(nomClient, prenomClient, telClient, adresseRueClient, adresseCPClient, adresseVilleClient)
        {
            this.IdClient = idClient;
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
                this.nomClient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()); ;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomClient))); 
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
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le prenom doit être renseigné"); }
                if (value.Length > 50) { throw new ArgumentException("doit avoir moins de 50 caractères"); }
                this.prenomClient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()); ;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrenomClient)));

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
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le numero de téléphone doit être renseigné"); }
                if (value.Length != 10) { throw new ArgumentException("doit avoir 10 caractères"); }
                this.telClient = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TelClient)));

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
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("L'adresse du rue doit être renseigné"); }
                if (value.Length > 100) { throw new ArgumentException("doit avoir moins de 100 caractères"); }
                this.adresseRueClient = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdresseRueClient)));

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
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Le code postal doit être renseigné"); }
                if (value.Length > 5) { throw new ArgumentException("doit avoir 5 caractères"); }
                this.adresseCPClient = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdresseCPClient)));

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
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentException("La ville doit être renseigné"); }
                if (value.Length > 50) { throw new ArgumentException("doit avoir moins de 50 caractères"); }
                this.adresseVilleClient = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdresseVilleClient)));

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
        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into client (nomclient,prenomclient,tel,adresserue,adressecp,adresseville ) values (@nomclient,@prenomclient,@tel,@adresserue,@adressecp,@adresseville) RETURNING numclient"))
            {
                cmdInsert.Parameters.AddWithValue("nomclient", this.NomClient);
                cmdInsert.Parameters.AddWithValue("prenomclient", this.PrenomClient);
                cmdInsert.Parameters.AddWithValue("tel", this.TelClient);
                cmdInsert.Parameters.AddWithValue("adresserue", this.AdresseRueClient);
                cmdInsert.Parameters.AddWithValue("adressecp", this.AdresseCPClient);
                cmdInsert.Parameters.AddWithValue("adresseville", this.AdresseVilleClient);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.IdClient = nb;
            return nb;
        }
        public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("select * from  client  where numclient =@numclient;"))
            {
                cmdSelect.Parameters.AddWithValue("numclient", this.IdClient);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                this.NomClient = (String)dt.Rows[0]["nomclient"];
                this.PrenomClient = (String)dt.Rows[0]["prenomclient"];
                this.TelClient = (String)dt.Rows[0]["tel"];
                this.AdresseRueClient = (String)dt.Rows[0]["adresserue"];
                this.AdresseCPClient = (String)dt.Rows[0]["adressecp"];
                this.AdresseVilleClient = (String)dt.Rows[0]["adresseville"];

            }

        }


        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update client set nomclient =@nomclient ,  prenomclient = @prenomclient,  tel = @tel , adresserue =@adresserue ,  adressecp = @adressecp,  adresseville = @adresseville  where numclient =@IdClient;"))
            {
                cmdUpdate.Parameters.AddWithValue("nomclient", this.NomClient);
                cmdUpdate.Parameters.AddWithValue("prenomclient", this.PrenomClient);
                cmdUpdate.Parameters.AddWithValue("tel", this.TelClient);
                cmdUpdate.Parameters.AddWithValue("adresserue", this.AdresseRueClient);
                cmdUpdate.Parameters.AddWithValue("adressecp", this.AdresseCPClient);
                cmdUpdate.Parameters.AddWithValue("adresseville", this.AdresseVilleClient);
                cmdUpdate.Parameters.AddWithValue("numclient", this.IdClient);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
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
