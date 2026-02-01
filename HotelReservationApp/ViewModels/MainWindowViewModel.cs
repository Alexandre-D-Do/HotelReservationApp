using HotelReservationApp.Models;
using HotelReservationApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += CurrentViewModelChangedHandler;
        }

        private void CurrentViewModelChangedHandler()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
