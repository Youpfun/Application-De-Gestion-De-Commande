
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
using Application_Pour_Sibilia.Models;

namespace Application_Pour_Sibilia.Views.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowClient.xaml
    /// </summary>
    public enum typeAction { Modifier, Créer };

    public partial class WindowClient : Window
    {
        public WindowClient()
        {
            InitializeComponent();
        }
        public WindowClient(Client unclient, typeAction uneAction)
        {
            InitializeComponent();
            this.DataContext = unclient;
            this.butValider.Content = uneAction.ToString();
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in FormClient.Children)
            {
                if (uie is TextBox)
                {
                    TextBox txt = (TextBox)uie;
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }

                if (Validation.GetHasError(uie))
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
