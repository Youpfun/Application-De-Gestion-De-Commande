using Application_Pour_Sibilia.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class DataPage : INavigableView<DataViewModel>
    {
        public DataViewModel ViewModel { get; }

        public DataPage(DataViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
