using CommunityToolkit.Mvvm.ComponentModel;
using HotelReservationApp.Models;
using HotelReservationApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly NavigationStore _navigationStore;

        public IPageViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
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
