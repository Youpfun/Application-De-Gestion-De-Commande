using Application_Pour_Sibilia.Models;
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
using System.Windows.Shapes;

namespace Application_Pour_Sibilia.Views.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowDetailsCommande.xaml
    /// </summary>
    public partial class WindowDetailsCommande : Window
    {
        public WindowDetailsCommande(int numCommande)
        {
            InitializeComponent();
            ChargerDetailsCommande(numCommande);
            
        }
        // Permet de charger le contenue d'une commande spécifique dans une liste
        private void ChargerDetailsCommande(int numCommande)
        {
            List<PlatCommande> details = PlatCommande.DetailsCommandes(numCommande);
            detailsCommande.ItemsSource = details;
        }
    }
}
