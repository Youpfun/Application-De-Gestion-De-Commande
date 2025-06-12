using Application_Pour_Sibilia.ViewModels.Pages;
using System.Windows.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class CreationCommandePage : Page
    {
        public CreationCommandeViewModel ViewModel { get; }

        public CreationCommandePage()
        {
            InitializeComponent();
            ViewModel = new CreationCommandeViewModel();
            DataContext = ViewModel;

            ViewModel.LignesCommande.CollectionChanged += LignesCommande_CollectionChanged;
        }
        private void LignesCommande_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ScrollCommande?.ScrollToEnd();
        }
    }
}