using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Pour_Sibilia.Models
{
    public class Magasin
    {
        private string nom;
        private ObservableCollection<Client> lesClients;

        public Magasin(string nom)
        {
            this.Nom = nom;
            this.LesClients = new ObservableCollection<Client>(new Client().FindAll());
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
    }

    
}
