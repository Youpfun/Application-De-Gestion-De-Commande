using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Application_Pour_Sibilia.Views.Pages
{
    /// <summary>
    /// Logique d'interaction pour GestionDesPlats.xaml
    /// </summary>
    /// 
        public class Plat
    {
        public string Nom { get; set; }
        public string Categorie { get; set; }
        public string SousCategorie { get; set; }
        public string Disponibilite { get; set; }
    }
    public partial class GestionDesPlats : Page
    {

        public GestionDesPlats()
        {
            InitializeComponent();
            var plats = new List<Plat>
            {
                new Plat { Nom = "Tranche de jambon", Categorie = "dessert", SousCategorie = "fromage", Disponibilite = "Toute saison" },
                new Plat { Nom = "Escragot", Categorie = "Entrée", SousCategorie = "Jambon", Disponibilite = "Toute saison" },
                new Plat { Nom = "Bouliste", Categorie = "plat", SousCategorie = "fddd", Disponibilite = "Toute saison" }
            };
            PlatsDataGrid.ItemsSource = plats;
        }
        private void CreePlat_Click(object sender, RoutedEventArgs e)
        {

        }

    }

}
