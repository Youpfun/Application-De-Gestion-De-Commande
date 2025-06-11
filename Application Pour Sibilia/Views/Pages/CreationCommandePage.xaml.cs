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
            DataContext = new CreationCommandeViewModel();
        }

    }
}