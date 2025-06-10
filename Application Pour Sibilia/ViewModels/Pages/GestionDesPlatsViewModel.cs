using Application_Pour_Sibilia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Application_Pour_Sibilia.ViewModels.Pages
{
    public partial class GestionDesPlatsViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<Plat> lesPlats;
        [ObservableProperty]
        private string motClefPlat;
        public GestionDesPlatsViewModel()
        {
            ChargerPlats();

        }

        private void ChargerPlats()
        {
            Plat repo = new Plat();
            var plats = repo.FindAll();
            LesPlats = new ObservableCollection<Plat>(plats);
        }
    }
}
