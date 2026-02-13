using HotelReservationApp.Models;
using HotelReservationApp.Stores;
using HotelReservationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HotelReservationApp.Commands
{
    public class DeleteReservationCommand : AsyncCommandBase
    {
        private readonly ReservationListingViewModel _reservationListingViewModel;
        private readonly HotelStore _hotelStore;

        public DeleteReservationCommand(ReservationListingViewModel reservationListingViewModel, HotelStore hotelStore)
        {
            _reservationListingViewModel = reservationListingViewModel;
            _hotelStore = hotelStore;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                ReservationViewModel reservationViewModel = _reservationListingViewModel.SelectedReservation;
                Reservation reservation = new Reservation(
                    new RoomID(reservationViewModel.FloorNumber, reservationViewModel.RoomNumber),
                    reservationViewModel.Username, reservationViewModel.StartDateData, reservationViewModel.EndDateData);
                await _hotelStore.DeleteReservation(reservation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }


    }
}
