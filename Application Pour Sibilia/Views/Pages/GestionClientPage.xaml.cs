using Application_Pour_Sibilia.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class GestionClientPage : INavigableView<GestionClientViewModel>
    {
        public GestionClientViewModel ViewModel { get; }

        public GestionClientPage(GestionClientViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
