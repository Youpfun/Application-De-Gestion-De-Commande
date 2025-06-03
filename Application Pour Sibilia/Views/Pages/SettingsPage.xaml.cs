using Application_Pour_Sibilia.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
