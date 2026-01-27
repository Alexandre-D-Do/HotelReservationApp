using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.ViewModels
{
    internal class MainWindowViewModel
    {
        public ViewModelBase CurrentViewModel { get; }
        public MainWindowViewModel()
        {
            CurrentViewModel = new MakeReservationViewModel();
        }
    }
}
