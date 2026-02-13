using HotelReservationApp.Models;
using HotelReservationApp.Stores;
using HotelReservationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HotelReservationApp.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly HotelStore _hotelStore;
        private readonly ReservationListingViewModel _reservationListingViewModel;

        public LoadReservationsCommand(ReservationListingViewModel viewModel, HotelStore hotelStore)
        {
            _hotelStore = hotelStore;
            _reservationListingViewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _reservationListingViewModel.ErrorMessage = string.Empty;
            _reservationListingViewModel.IsLoading = true;
            try
            {
                await _hotelStore.Initialize();
                _reservationListingViewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                _reservationListingViewModel.ErrorMessage= "Failed to load reservations.";
            }
            _reservationListingViewModel.IsLoading = false;
        }
    }
}
