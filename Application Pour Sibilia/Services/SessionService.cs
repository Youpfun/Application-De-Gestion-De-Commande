using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Application_Pour_Sibilia.Services
{
    public class SessionService : INotifyPropertyChanged
    {
        private string login;
        private string prenom;
        private string nom;
        private int numEmploye;
        private int roleEmploye; // Ajout du rôle de l'employé

        public string Login
        {
            get => login;
            set
            {
                if (login != value)
                {
                    login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        public string Nom
        {
            get => nom;
            set
            {
                if (nom != value)
                {
                    nom = value;
                    OnPropertyChanged(nameof(Nom));
                }
            }
        }

        public string Prenom
        {
            get => prenom;
            set
            {
                if (prenom != value)
                {
                    prenom = value;
                    OnPropertyChanged(nameof(Prenom));
                }
            }
        }

        public int NumEmploye
        {
            get => numEmploye;
            set
            {
                if (numEmploye != value)
                {
                    numEmploye = value;
                    OnPropertyChanged(nameof(NumEmploye));
                }
            }
        }

        public int RoleEmploye
        {
            get => roleEmploye;
            set
            {
                if (roleEmploye != value)
                {
                    roleEmploye = value;
                    OnPropertyChanged(nameof(RoleEmploye));
                }
            }
        }

        // Méthode pratique pour vérifier si l'employé est un vendeur (rôle 1)
        public bool EstVendeur => RoleEmploye == 1;

        // Méthode pratique pour vérifier si l'employé peut créer des plats
        public bool PeutCreerPlats => RoleEmploye != 1;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
