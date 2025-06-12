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
        private string _login;
        private string _prenom;
        private string _nom;
        private int _numEmploye;
        private int _roleEmploye; // Ajout du rôle de l'employé

        public string Login
        {
            get => _login;
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        public string Nom
        {
            get => _nom;
            set
            {
                if (_nom != value)
                {
                    _nom = value;
                    OnPropertyChanged(nameof(Nom));
                }
            }
        }

        public string Prenom
        {
            get => _prenom;
            set
            {
                if (_prenom != value)
                {
                    _prenom = value;
                    OnPropertyChanged(nameof(Prenom));
                }
            }
        }

        public int NumEmploye
        {
            get => _numEmploye;
            set
            {
                if (_numEmploye != value)
                {
                    _numEmploye = value;
                    OnPropertyChanged(nameof(NumEmploye));
                }
            }
        }

        public int RoleEmploye
        {
            get => _roleEmploye;
            set
            {
                if (_roleEmploye != value)
                {
                    _roleEmploye = value;
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
