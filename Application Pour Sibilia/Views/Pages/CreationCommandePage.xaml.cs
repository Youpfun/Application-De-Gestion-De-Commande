using Application_Pour_Sibilia.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class CreationCommandePage : INavigableView<CreationCommandeViewModel>
    {
        public CreationCommandeViewModel ViewModel { get; }

        public CreationCommandePage(CreationCommandeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            // Explicitly specify the namespace to resolve ambiguity
            this.InitializeComponent();
        }
    }
}
