using System.Collections.ObjectModel;
using System.Windows.Media;
using Application_Pour_Sibilia.Models;
using Wpf.Ui.Abstractions.Controls;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class GestionClientViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Client> lesClients = new()
        {
            new Client("Étienne","Roland","0799284957","rue de Hamon","20094","Marie" )

        };

    }

    
}
