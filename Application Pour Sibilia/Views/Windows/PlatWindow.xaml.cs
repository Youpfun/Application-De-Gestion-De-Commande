using Application_Pour_Sibilia.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour PlatWindow.xaml
    /// </summary>
    public partial class PlatWindow : Window
    {
        public PlatWindow()
        {
            InitializeComponent();
            ChargerDonneesReferentielles();
        }

        public PlatWindow(Plat unPlat, typeAction uneAction)
        {
            InitializeComponent();
            ChargerDonneesReferentielles();
            this.DataContext = unPlat;
            this.butValiderPlat.Content = uneAction.ToString();
        }

        private void ChargerDonneesReferentielles()
        {
            try
            {
                // Charger les sous-catégories
                ChargerSousCategories();

                // Charger les périodes
                ChargerPeriodes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChargerSousCategories()
        {
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(@"
                SELECT sc.numsouscategorie, sc.nomsouscategorie, c.nomcategorie 
                FROM souscategorie sc 
                JOIN categorie c ON sc.numcategorie = c.numcategorie 
                ORDER BY c.nomcategorie, sc.nomsouscategorie"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);

                var sousCategories = new List<dynamic>();
                foreach (DataRow dr in dt.Rows)
                {
                    sousCategories.Add(new
                    {
                        Value = Convert.ToInt32(dr["numsouscategorie"]),
                        Text = $"{dr["nomcategorie"]} - {dr["nomsouscategorie"]}"
                    });
                }

                comboSousCategorie.ItemsSource = sousCategories;
            }
        }

        private void ChargerPeriodes()
        {
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT numperiode, libelleperiode FROM periode ORDER BY numperiode"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);

                var periodes = new List<dynamic>();
                foreach (DataRow dr in dt.Rows)
                {
                    periodes.Add(new
                    {
                        Value = Convert.ToInt32(dr["numperiode"]),
                        Text = dr["libelleperiode"].ToString()
                    });
                }

                comboPeriode.ItemsSource = periodes;
            }
        }

        private void butValiderPlat_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;

            // Valider les TextBox
            foreach (UIElement uie in FormClient.Children)
            {
                if (uie is TextBox)
                {
                    TextBox txt = (TextBox)uie;
                    txt.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                }
                else if (uie is ComboBox)
                {
                    ComboBox combo = (ComboBox)uie;
                    combo.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();
                }

                if (Validation.GetHasError(uie))
                    ok = false;
            }

            // Vérifier que les ComboBox ont des valeurs sélectionnées
            if (comboSousCategorie.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner une sous-catégorie.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                ok = false;
            }

            if (comboPeriode.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner une période.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                ok = false;
            }

            if (ok)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Erreur de saisie", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}