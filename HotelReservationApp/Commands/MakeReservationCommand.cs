using HotelReservationApp.Exceptions;
using HotelReservationApp.Models;
using HotelReservationApp.Services;
using HotelReservationApp.Stores;
using HotelReservationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservationApp.Commands
{
    internal class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly HotelStore _hotelStore;
        private readonly NavigationService _reservationListingNavigationService;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, HotelStore hotelStore, 
            NavigationService reservationListingNavigationService)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotelStore = hotelStore;
            _reservationListingNavigationService = reservationListingNavigationService;

            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        

        public override bool CanExecute(object parameter)
        {
            // Check if floor number is parsable
            if (!int.TryParse(_makeReservationViewModel.FloorNumber, out int floorNumberValue))
            {
                return false;
            }

            // Check if room number is parsable
            if (!int.TryParse(_makeReservationViewModel.RoomNumber, out int roomNumberValue))
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(_makeReservationViewModel.Username) &&
                floorNumberValue > 0 &&
                roomNumberValue > 0 &&
                DateTime.Compare(_makeReservationViewModel.StartDate, _makeReservationViewModel.EndDate) < 0 &&
                base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_makeReservationViewModel.Username) || 
                e.PropertyName == nameof(_makeReservationViewModel.FloorNumber) || 
                e.PropertyName == nameof(_makeReservationViewModel.RoomNumber) ||
                e.PropertyName == nameof(_makeReservationViewModel.StartDate) ||
                e.PropertyName == nameof(_makeReservationViewModel.EndDate))
            {
                OnExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _makeReservationViewModel.IsLoading = true;
            Reservation reservation = new Reservation(
                new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.Username, 
                _makeReservationViewModel.StartDate, 
                _makeReservationViewModel.EndDate
                );

            try
            {
                await _hotelStore.MakeReservation(reservation);
                MessageBox.Show("Successfully reserved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _reservationListingNavigationService.Navigate();

            }
            catch(ReservationConflictException)
            {
                MessageBox.Show("This room is already taken within the selected timeframe.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to make reservation.",
                   "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _makeReservationViewModel.IsLoading = false;
        }
    }
}
