using System.Windows.Media;
using Application_Pour_Sibilia.Models;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class GestionClientViewModel : ObservableObject
    {
        private string nomClient;
        private string prenomClient;
        private string telClient;
        private string adresseRueClient;
        private string adresseCPClient;
        private string adresseVilleClient;

        public string NomClient
        {
            get
            {
                return this.nomClient;
            }

            set
            {
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
                this.adresseVilleClient = value;
            }
        }
    }
}
