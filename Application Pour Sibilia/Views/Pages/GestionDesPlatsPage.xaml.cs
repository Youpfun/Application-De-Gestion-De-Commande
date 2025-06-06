using Application_Pour_Sibilia.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class GestionDesPlatsPage : INavigableView<GestionDesPlatsViewModel>
    {
        public GestionDesPlatsViewModel ViewModel { get; }


        public GestionDesPlatsPage(GestionDesPlatsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            DataContext = new GestionDesPlatsViewModel();
            InitializeComponent();
        }
    }
}
