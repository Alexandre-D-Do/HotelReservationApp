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
        private readonly ReservationListingViewModel _viewModel;

        public LoadReservationsCommand(ReservationListingViewModel viewModel, HotelStore hotelStore)
        {
            _hotelStore = hotelStore;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;
            try
            {
                await _hotelStore.Load();
                _viewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load reservations.",
                   "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _viewModel.IsLoading = false;
        }
    }
}
