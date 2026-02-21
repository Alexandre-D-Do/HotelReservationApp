using CommunityToolkit.Mvvm.ComponentModel;
using HotelReservationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        private IPageViewModel _currentViewModel;

        public IPageViewModel CurrentViewModel
        {
            get => _currentViewModel;
            
            set
            {
                //Set IsActive to false for the current view model before changing it.
                if (_currentViewModel != null)
                {
                    _currentViewModel.IsActive = false;
                }

                _currentViewModel = value;

                //Set IsActive to true for the new view model after changing it.
                if (_currentViewModel != null)
                {
                    _currentViewModel.IsActive = true;
                }
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
