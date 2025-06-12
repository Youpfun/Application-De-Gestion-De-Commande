using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Application_Pour_Sibilia.Models
{
    public class Magasin
    {
        private string nom;
        private ObservableCollection<Client> lesClients;
        private ObservableCollection<Plat> lesPlats;
        private ObservableCollection<GestionCommande> lesGestionCommandes;
        private ObservableCollection<GestionCommande> lesCommandesDuJour;

        public Magasin(string nom)
        {
            this.Nom = nom;
            this.LesClients = new ObservableCollection<Client>(new Client().FindAll());
            this.LesPlats = new ObservableCollection<Plat>(new Plat().FindAll());
            this.LesGestionCommandes = new ObservableCollection<GestionCommande>(new GestionCommande().FindAll());
            this.LesCommandesDuJour = new ObservableCollection<GestionCommande>(new GestionCommande().FindAllCommandeAujourdhui());

        }
        public Magasin():this("")
        {
            
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public ObservableCollection<Client> LesClients
        {
            get
            {
                return this.lesClients;
            }

            set
            {
                this.lesClients = value;
            }
        }
        public ObservableCollection<Plat> LesPlats
        {
            get
            {
                return this.lesPlats;
            }

            set
            {
                this.lesPlats = value;
            }
        }

        public ObservableCollection<GestionCommande> LesGestionCommandes
        {
            get
            {
                return this.lesGestionCommandes;
            }

            set
            {
                this.lesGestionCommandes = value;
            }
        }

        public ObservableCollection<GestionCommande> LesCommandesDuJour
        {
            get
            {
                return this.lesCommandesDuJour;
            }

            set
            {
                this.lesCommandesDuJour = value;
            }
        }
    }

    
}
